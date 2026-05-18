using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR16_Abazov.Views
{
    public partial class GameOverPage : Page
    {
        public GameOverPage(int floor)
        {
            InitializeComponent();
            TxtFinalFloor.Text = $"Вы дошли до {floor} этажа";
        }

        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {

            var p = Core.Core.CurrentPlayer;
            p.CurrentHP = p.MaxHP;
            p.CurrentFloor = 1;
            Core.Core.Save();

            NavigationService.Navigate(new StartMenuPage());
        }
    }
}
