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
using System.Windows.Shapes;

namespace Catalog.Wndows
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCartWnd.xaml
    /// </summary>
    public partial class ShoppingCartWnd : Window
    {
        public ShoppingCartWnd()
        {
            InitializeComponent();

            pickCartBtn.Background = Brushes.LightGray;
        }

        private void ReturnToGoods(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }

        private void PickCart(object sender, RoutedEventArgs e)
        {
            pickCartBtn.Background = Brushes.LightGray;
            pickDeliveryBtn.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
        }

        private void PickDelivery(object sender, RoutedEventArgs e)
        {
            pickCartBtn.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            pickDeliveryBtn.Background = Brushes.LightGray;
        }
    }
}
