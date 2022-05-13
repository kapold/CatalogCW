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
        public DeliveryRegistration(Good good)
        {
            InitializeComponent();

            FIOBox.Text = $"{Auth.currentUser.Surname} {Auth.currentUser.Name} {Auth.currentUser.Patronymic}";
            countBox.Text = "1";
            goodNameBox.Text = good.Name;
            deliveryDateBox.DisplayDateStart = DateTime.Now.AddDays(1);
            deliveryDateBox.DisplayDateEnd = DateTime.Now.AddDays(7);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CartPage.ifDeliveryOpened = false;
        }

        private void AddToDeliveries(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("WORK");
        }
    }
}
