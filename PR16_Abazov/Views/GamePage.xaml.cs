using PR16_Abazov.Core;
using PR16_Abazov.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PR16_Abazov.Views
{
    public partial class GamePage : Page
    {
        GameEngine engine = new GameEngine();
        List<EnemyTemplates> floorEnemies = new List<EnemyTemplates>();
        Items pendingItem;
        bool isPlayerFrozen = false;

        public GamePage()
        {
            InitializeComponent();
            NextTurn();
        }

        private void UpdateUI()
        {
            var p = Core.Core.CurrentPlayer;
            if (p == null) return;

            HpBar.Maximum = p.MaxHP;
            HpBar.Value = p.CurrentHP;
            TxtHp.Text = $"{Math.Round(p.CurrentHP, 1)} / {p.MaxHP}";
            TxtFloor.Text = $"ЭТАЖ: {p.CurrentFloor}";

            EnemyListBox.ItemsSource = null;
            EnemyListBox.ItemsSource = floorEnemies;
            
            if (floorEnemies.Count > 0 && EnemyListBox.SelectedIndex == -1)
                EnemyListBox.SelectedIndex = 0;
        }

        private void NextTurn()
        {
            var p = Core.Core.CurrentPlayer;
            floorEnemies.Clear();
            
            var ev = engine.GenerateEvent(p.CurrentFloor);

            if (ev is EnemyTemplates)
            {
                Random rng = new Random();
                int count = rng.Next(1, 4); 
                
                for (int i = 0; i < count; i++)
                {
                    floorEnemies.Add(engine.GenerateEnemy(p.CurrentFloor));
                }

                TxtRoomTitle.Text = $"Врагов на этаже: {floorEnemies.Count}";
                PanelActions.Visibility = Visibility.Visible;
                PanelChest.Visibility = Visibility.Collapsed;
                EnemyListBox.SelectedIndex = -1;
                if (floorEnemies.Count > 0) EnemyListBox.SelectedIndex = 0;
            }
            else
            {
                pendingItem = engine.GenerateLoot();
                TxtRoomTitle.Text = $"Сундук: {pendingItem.Name}";
                PanelActions.Visibility = Visibility.Collapsed;
                PanelChest.Visibility = Visibility.Visible;
                ImgEntity.Source = new BitmapImage(new Uri("/Resources/Images/chest.png", UriKind.Relative));
                EnemyListBox.ItemsSource = null;
            }
            UpdateUI();
        }
        private void Log(string message)
        {
            LstLog.Items.Insert(0, $"[{DateTime.Now:HH:mm:ss}] {message}");
        }
        private void EnemyListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EnemyListBox.SelectedItem == null) return;

            var selected = EnemyListBox.SelectedItem as EnemyTemplates;
            if (selected != null)
            {
                string type = selected.TypeName.ToLower();
                string fileName = "placeholder.png";

                if (type.Contains("гоблин")) fileName = "goblin.png";
                else if (type.Contains("скелет")) fileName = "skeleton.png";
                else if (type.Contains("маг")) fileName = "mage.png";
                else if (type.Contains("ввг")) fileName = "goblin.png";
                else if (type.Contains("архимаг")) fileName = "mage.png";
                else if (type.Contains("пестов")) fileName = "skeleton.png";
                else if (type.Contains("ковальский")) fileName = "skeleton.png";
                
                ImgEntity.Source = new BitmapImage(new Uri("chest.png", UriKind.Relative));
                
                try
                {
                    ImgEntity.Source = new BitmapImage(new Uri($"/Resources/Images/{fileName}", UriKind.Relative));
                }
                catch
                {
                }
            }
        }
        private async void BtnAttack_Click(object sender, RoutedEventArgs e)
        {
            var selected = EnemyListBox.SelectedItem as EnemyTemplates;

            if (selected == null)
            {
                Log("Сначала нажми на врага в списке справа!");
                return;
            }

            string result = CombatSystem.PlayerAttack(Core.Core.CurrentPlayer, selected);
            Log(result);

            if (selected.BaseHP <= 0)
            {
                Log($"{selected.TypeName} убит!");
                floorEnemies.Remove(selected);
            }

            UpdateUI();

            if (floorEnemies.Count == 0)
            {
                FinishRoom();
            }
            else
            {
                await AllEnemiesAttack(false);
            }
        }

        private async void BtnGuard_Click(object sender, RoutedEventArgs e)
        {
            LstLog.Items.Insert(0, "Вы защищаетесь...");
            await AllEnemiesAttack(true); 
        }

        private async Task AllEnemiesAttack(bool isGuarding)
        {
            PanelActions.IsEnabled = false;
            

            var currentEnemies = floorEnemies.ToList();

            foreach (var enemy in currentEnemies)
            {
                await Task.Delay(600);
                string result = CombatSystem.EnemyAttack(enemy, Core.Core.CurrentPlayer, isGuarding);
                LstLog.Items.Insert(0, result);

                if (result.Contains("ЗАМОРОЖЕНЫ")) isPlayerFrozen = true;
                
                UpdateUI();
                if (Core.Core.CurrentPlayer.CurrentHP <= 0)
                {
                    NavigationService.Navigate(new GameOverPage(Core.Core.CurrentPlayer.CurrentFloor));
                    return;
                }
            }
            PanelActions.IsEnabled = true;
        }

        private void FinishRoom()
        {
            Core.Core.CurrentPlayer.CurrentFloor++;
            NextTurn();
        }

        private void BtnTake_Click(object sender, RoutedEventArgs e)
        {
            var p = Core.Core.CurrentPlayer;
            if (pendingItem.Type == "Weapon") p.CurrentWeapon = pendingItem;
            else if (pendingItem.Type == "Armor") p.CurrentArmor = pendingItem;
            else if (pendingItem.IsHeal) p.CurrentHP = p.MaxHP;
            FinishRoom();
        }

        private void BtnSkip_Click(object sender, RoutedEventArgs e) => FinishRoom();
    }
}