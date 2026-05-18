using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using PR16_Abazov.Core;
using PR16_Abazov.Models;

namespace PR16_Abazov.Views
{
    public partial class GamePage : Page
    {
        GameEngine engine = new GameEngine();
        EnemyTemplates currentEnemy;
        Items pendingItem;

        public GamePage()
        {
            InitializeComponent();
            UpdateUI();
            NextTurn();
        }

        private void UpdateUI()
        {
            var p = Core.Core.CurrentPlayer;
            HpBar.Maximum = p.MaxHP;
            HpBar.Value = p.CurrentHP;
            TxtHp.Text = $"{Math.Round(p.CurrentHP, 1)} / {p.MaxHP}";
            TxtFloor.Text = $"ЭТАЖ: {p.CurrentFloor}";

            TxtWeapon.Text = $"Оружие: {p.CurrentWeapon?.Name ?? "Кулаки"}";
            TxtArmor.Text = $"Доспех: {p.CurrentArmor?.Name ?? "Рубаха"}";
        }

        private void Log(string msg)
        {
            LstLog.Items.Insert(0, $"[{DateTime.Now:HH:mm}] {msg}");
        }

        private void NextTurn()
        {
            var ev = engine.GenerateEvent(Core.Core.CurrentPlayer.CurrentFloor);
            string fileName = "placeholder.png";

            if (ev is EnemyTemplates template)
            {
                currentEnemy = new EnemyTemplates
                {
                    TypeName = template.TypeName,
                    BaseHP = template.BaseHP,
                    BaseAttack = template.BaseAttack,
                    BaseDefense = template.BaseDefense,
                    IsBoss = template.IsBoss
                };

                TxtRoomTitle.Text = $"Враг: {currentEnemy.TypeName} (HP: {currentEnemy.BaseHP})";
                PanelActions.Visibility = Visibility.Visible;
                PanelChest.Visibility = Visibility.Collapsed;

                string type = currentEnemy.TypeName.ToLower();
                if (type.Contains("гоблин")) fileName = "goblin.png";
                else if (type.Contains("скелет")) fileName = "skeleton.png";
                else if (type.Contains("маг")) fileName = "mage.png";
            }
            else
            {
                pendingItem = engine.GenerateLoot();
                TxtRoomTitle.Text = $"Сундук! Внутри: {pendingItem.Name}";
                PanelActions.Visibility = Visibility.Collapsed;
                PanelChest.Visibility = Visibility.Visible;
                fileName = "chest.png";
            }

            try
            {
                ImgEntity.Source = new BitmapImage(new Uri($"/Resources/Images/{fileName}", UriKind.Relative));
            }
            catch { }
        }

        private async void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            PanelActions.IsEnabled = false;

            Log(CombatSystem.PlayerAttack(Core.Core.CurrentPlayer, currentEnemy));
            UpdateUI();

            if (currentEnemy.BaseHP <= 0)
            {
                await Task.Delay(500);
                FinishRoom();
            }
            else
            {
                await Task.Delay(800);
                Log(CombatSystem.EnemyAttack(currentEnemy, Core.Core.CurrentPlayer, false));
                UpdateUI();
                CheckDeath();
            }

            PanelActions.IsEnabled = true;
        }

        private void FinishRoom()
        {
            if (currentEnemy != null)
                Log($"Победа над {currentEnemy.TypeName}!");

            Core.Core.CurrentPlayer.CurrentFloor++;
            NextTurn();
            UpdateUI();
        }

        private void BtnGuard_Click(object sender, RoutedEventArgs e)
        {
            Log("Вы защищаетесь...");
            Log(CombatSystem.EnemyAttack(currentEnemy, Core.Core.CurrentPlayer, true));
            UpdateUI();
            CheckDeath();
        }

        private void CheckDeath()
        {
            if (Core.Core.CurrentPlayer.CurrentHP <= 0)
            {
                // ИСПРАВЛЕНИЕ: Убираем ??, так как CurrentFloor теперь не nullable
                NavigationService.Navigate(new GameOverPage(Core.Core.CurrentPlayer.CurrentFloor));
            }
        }

        private void BtnTake_Click(object sender, RoutedEventArgs e)
        {
            if (pendingItem.Type == "Weapon")
            {
                Core.Core.CurrentPlayer.CurrentWeapon = pendingItem;
                Log($"Вы взяли оружие: {pendingItem.Name}");
            }
            else if (pendingItem.Type == "Armor")
            {
                Core.Core.CurrentPlayer.CurrentArmor = pendingItem;
                Log($"Вы надели броню: {pendingItem.Name}");
            }
            else if (pendingItem.IsHeal)
            {
                Core.Core.CurrentPlayer.CurrentHP = Core.Core.CurrentPlayer.MaxHP;
                Log("Вы выпили зелье и восстановили HP!");
            }
            FinishRoom();
        }

        private void BtnSkip_Click(object sender, RoutedEventArgs e)
        {
            Core.Core.CurrentPlayer.CurrentFloor++;
            NextTurn();
            UpdateUI();
        }
    }
}