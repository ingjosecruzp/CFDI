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
using CFDI.Views;
using CFDI.ViewModel;

namespace CFDI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DockPanel_Loaded(object sender, RoutedEventArgs e)
        {
            /*MenuViewModel menuViewModel = new MenuViewModel();
            MenuLtl.DataContext = menuViewModel;
            MenuLtl.Content = new MenuView();*/
        }
    }
}
