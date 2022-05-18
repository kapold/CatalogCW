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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Catalog.Pages
{
    /// <summary>
    /// Логика взаимодействия для AllGoodsPage.xaml
    /// </summary>
    public partial class AllGoodsPage : Page
    {
        public static Page goodsPage;
        public List<Good> allGoods = new List<Good>();
        
        public AllGoodsPage()
        {
            InitializeComponent();
            goodsPage = this;

            DataBase dataBase = new DataBase();
            allGoods = dataBase.GetGoods();
            dataBase.Dispose();
            allGoodsList.ItemsSource = allGoods;
        }

        private void AddToCart(object sender, RoutedEventArgs e)
        {
            try
            {
                Good good = new Good();
                good = ((sender as Button).DataContext) as Good;
                //MessageBox.Show(good.ToString());
                bool ifThereIs = false;

                DataBase db = new DataBase();
                List<Order> ordersInCart = db.GetOrders();
                db.Dispose();

                if(ordersInCart != null)
                {
                    foreach (Order item in ordersInCart)
                    {
                        //MessageBox.Show(item.Good.ToString());
                        if (item.Good.ID == good.ID)
                        {
                            ifThereIs = true;
                        }
                    }
                }

                if (ifThereIs)
                {
                    MessageBox.Show("Такой товар уже находится в корзине!");
                }
                else
                {
                    DataBase dataBase = new DataBase();
                    dataBase.AddOrder(good, Auth.currentUser);
                    dataBase.Dispose();
                    MessageBox.Show("Товар успешно добавлен в корзину!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                MessageBox.Show("Ошибка: " + ex.Source);
            }
        }

        private void ShowDescription(object sender, RoutedEventArgs e)
        {
            try
            {
                Good good = new Good();
                good = ((sender as Button).DataContext) as Good;

                GoodDescriptionPage goodDescriptionPage = new GoodDescriptionPage(good);
                MainWindow.mainWindow.navigationService.Navigate(goodDescriptionPage);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}
