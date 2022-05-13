using Catalog.Classes;
using Catalog.Pages;
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
    /// Логика взаимодействия для DeliveryRegistration.xaml
    /// </summary>
    public partial class DeliveryRegistration : Window
    {
        public int savedMaxCount;
        public Good currentGood = new Good();
        public Order currentOrder = new Order();
        public DeliveryRegistration(Good good)
        {
            InitializeComponent();

            DataBase db = new DataBase();
            FIOBox.Text = $"{Auth.currentUser.Surname} {Auth.currentUser.Name} {Auth.currentUser.Patronymic}";
            countBox.Text = "1";
            goodNameBox.Text = good.Name;
            deliveryDateBox.DisplayDateStart = DateTime.Now.AddDays(1);
            deliveryDateBox.DisplayDateEnd = DateTime.Now.AddDays(7);
            addressBox.Text = Auth.currentUser.Address;
            List<Good> goodForCount = db.GetGoods();
            Good goodForPrice = goodForCount.FirstOrDefault();
            maxCountBox.Text = goodForPrice.Count.ToString();
            savedMaxCount = goodForPrice.Count;
            currentGood = good;

            currentOrder = db.GetOrders().Where(ord => ord.GoodID == good.ID).FirstOrDefault();
            

            db.Dispose();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CartPage.ifDeliveryOpened = false;
        }

        private void AddToDeliveries(object sender, RoutedEventArgs e)
        {
            Delivery delivery = new Delivery();
            delivery.DeliveryCount = 1;
            delivery.Order = currentOrder;
            delivery.OrderID = currentOrder.OrderNo;

            if (savedMaxCount < Convert.ToInt32(countBox.Text))
            {
                MessageBox.Show("Выберите количество товара, меньше чем на складе");
                return;
            }
            else
            {
                delivery.DeliveryCount = Convert.ToInt32(countBox.Text);
            }

            delivery.DeliveryDate = Convert.ToDateTime(deliveryDateBox.Text);
            ComboBoxItem paymentCBI = paymentTypeBox.SelectedItem as ComboBoxItem;
            if (paymentTypeBox.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите способ оплаты!");
            }
            else if (paymentCBI.Content.ToString() == "Наличными")
            {
                delivery.PaymentType = "Наличными";
            }
            else if (paymentCBI.Content.ToString() == "Банковской картой")
            {
                delivery.PaymentType = "Банковской картой";
            }
            else
            {
                MessageBox.Show("Ошибка в способе оплаты!");
            }

            if ((bool) checkForAddressBox.IsChecked)
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
            DataBase db = new DataBase();
            db.AddDelivery(delivery);
            db.Dispose();

            MessageBox.Show("Доставка оформлена!");
            this.Close();
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
