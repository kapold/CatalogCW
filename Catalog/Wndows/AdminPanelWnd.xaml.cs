using Catalog.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AdminPanelWnd.xaml
    /// </summary>
    public partial class AdminPanelWnd : Window
    {
        public AdminPanelWnd()
        {
            InitializeComponent();

            DataBase dataBase = new DataBase();
            List<Good> goods = dataBase.GetGoods();
            tableGoods.ItemsSource = goods;

            List<Delivery> deliveries = dataBase.GetAllDeliveries();
            tableOrders.ItemsSource = deliveries;

            dataBase.Dispose();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow.ifAdminPanelOpened = false;
        }

        private void OpenAddGoodWnd(object sender, RoutedEventArgs e)
        {
            AddGood addGood = new AddGood();
            addGood.Show();
            this.Close();
        }

        private void OpenAddTypeWnd(object sender, RoutedEventArgs e)
        {
            AddNewParams addNewParams = new AddNewParams();
            addNewParams.Show();
        }

        private void ChangeGoodBtn(object sender, RoutedEventArgs e)
        {
            if (tableGoods.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите товар!");
                return;
            }

            Good good = (Good) tableGoods.SelectedItem;
            int GoodID = Convert.ToInt32(good.ID);

            DataBase dataBase = new DataBase();
            dataBase.UpdateGood(GoodID, good);
            tableGoods.ItemsSource = dataBase.GetGoods();
            dataBase.Dispose();

            MessageBox.Show("Данные обновлены!");
        }

        private void DeleteGoodBtn(object sender, RoutedEventArgs e)
        {
            if (tableGoods.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите товар!");
                return;
            }

            Good good = (Good)tableGoods.SelectedItem;
            int GoodID = Convert.ToInt32(good.ID);

            DataBase dataBase = new DataBase();
            dataBase.DeleteGood(GoodID);
            tableGoods.ItemsSource = dataBase.GetGoods();
            dataBase.Dispose();

            MessageBox.Show("Товар удален!");
        }

        private void SearchBox_Changed(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex(searchBox.Text.ToUpper());

            DataBase dataBase = new DataBase();
            List<Good> goods = dataBase.GetGoods();
            List<Good> searchedGoods = new List<Good>();

            foreach (Good g in goods)
            {
                if(regex.IsMatch(g.Name.ToUpper()))
                {
                    searchedGoods.Add(g);
                }
            }

            tableGoods.ItemsSource = searchedGoods;
            dataBase.Dispose();
        }

        private void SearchOrders_Changed(object sender, TextChangedEventArgs e)
        {
            Regex regex = new Regex(searchOrdersBox.Text);

            DataBase dataBase = new DataBase();
            List<Delivery> deliveries = dataBase.GetAllDeliveries();
            List<Delivery> searchedDeliveries = new List<Delivery>();

            foreach (Delivery d in deliveries)
            {
                if (regex.IsMatch(d.DeliveryID.ToString()))
                {
                    searchedDeliveries.Add(d);
                }
            }

            tableOrders.ItemsSource = searchedDeliveries;
            dataBase.Dispose();
        }
    }
}
