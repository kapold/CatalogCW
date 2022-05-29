using Catalog.Classes;
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
    /// Логика взаимодействия для RegisterAllDeliveries.xaml
    /// </summary>
    public partial class RegisterAllDeliveries : Window
    {
        public RegisterAllDeliveries()
        {
            InitializeComponent();

            FIOBox.Text = $"{Auth.currentUser.Surname} {Auth.currentUser.Name} {Auth.currentUser.Patronymic}";
            deliveryDateBox.DisplayDateStart = DateTime.Now.AddDays(1);
            deliveryDateBox.DisplayDateEnd = DateTime.Now.AddDays(7);
            addressBox.Text = Auth.currentUser.Address;

        }

        private void AddToDeliveries(object sender, RoutedEventArgs e)
        {
            try
            {
                Delivery delivery = new Delivery();
                DataBase db = new DataBase();
                delivery.DeliveryCount = 1;

                if (db.GetOrders() is null)
                {
                    MessageBox.Show("Корзина пуста, нечего заказывать!");
                    return;
                }
                
                delivery.DeliveryDate = DateOnly.FromDateTime(Convert.ToDateTime(deliveryDateBox.Text));
                ComboBoxItem paymentCBI = paymentTypeBox.SelectedItem as ComboBoxItem;
                if (paymentTypeBox.SelectedIndex < 0)
                {
                    MessageBox.Show("Выберите способ оплаты!");
                }
                else if (paymentCBI.Content.ToString() == "Наличными курьеру")
                {
                    delivery.PaymentType = "Наличными курьеру";
                }
                else if (paymentCBI.Content.ToString() == "Банковской картой курьеру")
                {
                    delivery.PaymentType = "Банковской картой курьеру";
                }
                else
                {
                    MessageBox.Show("Ошибка в способе оплаты!");
                }

                if ((bool)checkForAddressBox.IsChecked)
                {
                    delivery.DeliveryAddress = Auth.currentUser.Address;
                }
                else if (!String.IsNullOrEmpty(addressBox.Text))
                {
                    delivery.DeliveryAddress = addressBox.Text;
                }
                else
                {
                    MessageBox.Show("Ошибка в выборе адреса!");
                }

                //MessageBox.Show(delivery.ToString());
                //MessageBox.Show($"Current GOOD: {currentGood.ToString()}");
                //MessageBox.Show($"Current Order: {currentOrder.OrderNo.ToString()}");
                List<Order> allCartGoods = db.GetOrders();
                foreach (Order order in allCartGoods)
                {
                    delivery.Order = order;
                    delivery.OrderID = order.OrderNo;
                    db.AddDelivery(delivery);
                    db.CorrectCount(1, delivery.Order.Good.Name);
                }

                db.Dispose();
                MessageBox.Show("Доставка оформлена!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Заполните все поля");
            }

            ShoppingCartWnd.mainCartWnd.GetCurrentTotalPrice();
            ShoppingCartWnd.mainCartWnd.cartPage.GetCartGoods();
            ShoppingCartWnd.mainCartWnd.deliveryPage.GetCartDeliveries();
        }

        private void BoxChecked(object sender, RoutedEventArgs e)
        {
            addressBox.Foreground = new SolidColorBrush(Colors.Gray);
            addressBox.IsEnabled = false;
        }

        private void BoxUnchecked(object sender, RoutedEventArgs e)
        {
            addressBox.Foreground = new SolidColorBrush(Colors.Black);
            addressBox.IsEnabled = true;
        }
    }
}
