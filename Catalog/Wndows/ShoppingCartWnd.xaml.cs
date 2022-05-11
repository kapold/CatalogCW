﻿using Catalog.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Catalog.Wndows
{
    /// <summary>
    /// Логика взаимодействия для ShoppingCartWnd.xaml
    /// </summary>
    public partial class ShoppingCartWnd : Window
    {
        public double totalPrice = 0;
        public NavigationService navigationService;
        public CartPage cartPage = new CartPage();
        public DeliveryPage deliveryPage = new DeliveryPage();
        public ShoppingCartWnd()
        {
            InitializeComponent();
            navigationService = mainCartFrame.NavigationService;
            navigationService.Navigate(cartPage);

            pickCartBtn.Background = Brushes.LightGray;
            pickCartBtn.BorderBrush = Brushes.LightGray;

            DataBase db = new DataBase();
            List<Order> orders = db.GetOrders();
            foreach (Order ord in orders)
            {
                totalPrice += Convert.ToDouble(ord.Good.Price);
            }
            totalPriceTB.Text = totalPrice.ToString();

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
            pickCartBtn.BorderBrush = Brushes.LightGray;
            pickDeliveryBtn.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            navigationService.Navigate(cartPage);
        }

        private void PickDelivery(object sender, RoutedEventArgs e)
        {
            pickCartBtn.Background = new SolidColorBrush(Color.FromRgb(255, 192, 203));
            pickDeliveryBtn.Background = Brushes.LightGray;
            pickDeliveryBtn.BorderBrush = Brushes.LightGray;
            navigationService.Navigate(deliveryPage);
        }
    }
}
