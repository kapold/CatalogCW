﻿using Catalog.Classes;
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
    /// Логика взаимодействия для DeliveryPage.xaml
    /// </summary>
    public partial class DeliveryPage : Page
    {
        public DeliveryPage()
        {
            InitializeComponent();

            GetCartDeliveries();
        }

        public void GetCartDeliveries()
        {
            List<Delivery> deliveries = new List<Delivery>();
            DataBase db = new DataBase();
            deliveries = db.GetDeliveries();
            db.Dispose();
            deliveryList.ItemsSource = deliveries;
        }
    }
}
