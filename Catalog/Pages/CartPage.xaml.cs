using Catalog.Classes;
using Catalog.Wndows;
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

namespace Catalog.Pages
{
    /// <summary>
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public static bool ifDeliveryOpened = false;
        public CartPage()
        {
            InitializeComponent();

            GetCartGoods();
        }

        public void GetCartGoods()
        {
            List<Order> orders = new List<Order>();
            DataBase db = new DataBase();
            orders = db.GetOrders();
            db.Dispose();
            cartList.ItemsSource = orders;
        }

        private void OpenDeliveryForm(object sender, RoutedEventArgs e)
        {
            Good good = new Good();
            good = ((sender as Button).DataContext) as Good;
            //MessageBox.Show(good.ToString());

            if (!ifDeliveryOpened)
            {
                DeliveryRegistration deliveryRegistration = new DeliveryRegistration(good);
                deliveryRegistration.Show();
                ifDeliveryOpened = true;
            }
        }

        private void DeleteFromOrders(object sender, RoutedEventArgs e)
        {
            Order order = new Order();
            order = ((sender as Button).DataContext) as Order;

            DataBase db = new DataBase();
            db.DeleteOrder(order);
            db.Dispose();

            MessageBox.Show("Товар удален из корзины!");
            GetCartGoods();
            ShoppingCartWnd.mainCartWnd.GetCurrentTotalPrice();
        }
    }
}
