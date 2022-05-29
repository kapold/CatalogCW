using Catalog.Classes;
using Catalog.Patterns;
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
        public static AdminPanelWnd adminWnd;
        public AdminPanelWnd()
        {
            InitializeComponent();
            adminWnd = this;
            DataContext = new ApplicationsViewModel();

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

        public void OpenAddGoodWnd()
        {
            AddGood addGood = new AddGood();
            addGood.Show();
            this.Close();
        }

        public void OpenAddTypeWnd()
        {
            AddNewParams addNewParams = new AddNewParams();
            addNewParams.Show();
        }

        private void ChangeGoodBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tableGoods.SelectedIndex < 0)
                {
                    MessageBox.Show("Выберите товар!");
                    return;
                }

                Good good = (Good)tableGoods.SelectedItem;
                int GoodID = Convert.ToInt32(good.ID);

                DataBase dataBase = new DataBase();
                dataBase.UpdateGood(GoodID, good);
                tableGoods.ItemsSource = dataBase.GetGoods();
                dataBase.Dispose();

                MessageBox.Show("Данные обновлены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteGoodBtn(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchBox_Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                Regex regex = new Regex(searchBox.Text.ToUpper());

                DataBase dataBase = new DataBase();
                List<Good> goods = dataBase.GetGoods();
                List<Good> searchedGoods = new List<Good>();

                foreach (Good g in goods)
                {
                    if (regex.IsMatch(g.Name.ToUpper()))
                    {
                        searchedGoods.Add(g);
                    }
                }

                tableGoods.ItemsSource = searchedGoods;
                dataBase.Dispose();
            }
            catch { }
        }

        private void SearchOrders_Changed(object sender, TextChangedEventArgs e)
        {
            try
            {
                Regex regex = new Regex(searchOrdersBox.Text);

                DataBase dataBase = new DataBase();
                List<Delivery> deliveries = dataBase.GetAllDeliveries();
                List<Delivery> searchedDeliveries = new List<Delivery>();

                if (deliveries != null)
                {
                    foreach (Delivery d in deliveries)
                    {
                        if (regex.IsMatch(d.Customer.Surname.ToString()))
                        {
                            searchedDeliveries.Add(d);
                        }
                    }
                }

                tableOrders.ItemsSource = searchedDeliveries;
                dataBase.Dispose();
            }
            catch { }
        }
    }
}
