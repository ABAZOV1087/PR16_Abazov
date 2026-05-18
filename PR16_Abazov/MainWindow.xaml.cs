using PR16_Abazov.Core;
using PR16_Abazov.Views;
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

namespace PR16_Abazov
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PR16_Abazov.Core.Core.InitData();
            MainFrame.Navigate(new StartMenuPage());
        }
    }
}