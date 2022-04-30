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
            Good good = e.Source as Good;
            MessageBox.Show(good.ToString());
            //DataBase dataBase = new DataBase();
            //dataBase.AddOrder(null, Auth.currentUser);
            //dataBase.Dispose();
        }
    }
}
