using Catalog.Classes;
using Catalog.Pages;
using Catalog.Wndows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace Catalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Кадры
        public NavigationService navigationService;
        public AllGoodsPage allGoodPage = new AllGoodsPage();
        public static MainWindow mainWindow;

        public static bool ifAdminPanelOpened = false;
        public static List<Good> filterGoods = new List<Good>();
        public static List<Good> goodsMainWnd = new List<Good>();
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            navigationService = goodsFrame.NavigationService;
            navigationService.Navigate(allGoodPage);

            typeFilter.SelectedIndex = 0;
            firmFilter.SelectedIndex = 0;
            fromPriceFilter.Text = "0";

            // Цена в филтре из БД
            //DataBase dataBase = new DataBase();
            //goodsMainWnd = dataBase.GetGoods();
            //dataBase.Dispose();
            //List<Good> forPriceFilter = goodsMainWnd.OrderBy(x => x.Price).ToList();
            //toPriceFilter.Text = Convert.ToString(forPriceFilter[0].Price);
            //forPrice = Convert.ToString(forPriceFilter[0].Price);

            // Скрываем админ-панель, если пользователь не админ
            if (!Auth.currentUser.IsAdmin)
            {
                adminPanelItem.IsEnabled = false;
                adminPanelItem.Visibility = Visibility.Hidden;
                adminPanelItem.Height = 0;
            }
        }

        private void OpenAdminPanel(object sender, RoutedEventArgs e)
        {
            if(!ifAdminPanelOpened)
            {
                AdminPanelWnd adminPanelWnd = new AdminPanelWnd();
                adminPanelWnd.Show();
                ifAdminPanelOpened = true;
            }
        }

        private void OpenAuthForm(object sender, RoutedEventArgs e)
        {
            RegisterWnd registerWnd = new RegisterWnd();
            registerWnd.Show();
            // Обнуляем пользователя
            Auth.currentUser = null;
            this.Close();
        }

        private void OpenProfile(object sender, RoutedEventArgs e)
        {
            ProfileWnd profileWnd = new ProfileWnd();
            profileWnd.Show();

            this.Close();
        }

        private void OutputGoods(object sender, RoutedEventArgs e)
        {

        }

        private void LeftArrowNavigate(object sender, RoutedEventArgs e)
        {
            try
            {
                navigationService.GoBack();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void RightArrowNavigate(object sender, RoutedEventArgs e)
        {
            try
            {
                navigationService.GoForward();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SearchBoxChanged(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(mainSearchBox.Text);

            DataBase dataBase = new DataBase();
            List<Good> goods = dataBase.GetGoods();
            List<Good> searchedGoods = new List<Good>();

            foreach (Good g in goods)
            {
                if (regex.IsMatch(g.Name))
                {
                    searchedGoods.Add(g);
                }
            }

            allGoodPage.allGoodsList.ItemsSource = searchedGoods;
            dataBase.Dispose();
        }

        // Функция общей фильтрации
        private void FilterChanged(object sender, RoutedEventArgs e)
        {
            DataBase dataBase = new DataBase();
            filterGoods = dataBase.GetGoods();

            // Фильтр-тип
            ComboBoxItem typeCBI = typeFilter.SelectedItem as ComboBoxItem;
            if (typeFilter.SelectedIndex < 0 || typeFilter.SelectedIndex == 0)
            {
                filterGoods = filterGoods;
            }
            else if (typeFilter.SelectedIndex > 0)
            {
                filterGoods = filterGoods.Where(g => g.Type == typeCBI.Content.ToString()).ToList();
            }
            // Фильтр-цена
            if (!String.IsNullOrEmpty(fromPriceFilter.Text) && !String.IsNullOrEmpty(toPriceFilter.Text))
            {
                filterGoods = filterGoods.Where(g => g.Price >= Convert.ToDouble(fromPriceFilter.Text) && g.Price <= Convert.ToDouble(toPriceFilter.Text)).ToList();
            }
            // Фильтр-фирма
            ComboBoxItem firmCBI = firmFilter.SelectedItem as ComboBoxItem;
            if (firmFilter.SelectedIndex < 0 || firmFilter.SelectedIndex == 0)
            {
                filterGoods = filterGoods;
            }
            else if (firmFilter.SelectedIndex > 0)
            {
                filterGoods = filterGoods.Where(g => g.Firm == firmCBI.Content.ToString()).ToList();
            }
            // Фильтр-дисплей(размер)
            if (!String.IsNullOrEmpty(displayFilter.Text))
            {
                filterGoods = filterGoods.Where(g => g.Display == Convert.ToDouble(displayFilter.Text)).ToList();
            }
            //


            allGoodPage.allGoodsList.ItemsSource = filterGoods;
            dataBase.Dispose();
        }

        private void OutputGoods(object sender, MouseButtonEventArgs e)
        {
            DataBase dataBase = new DataBase();
            List<Good> goods = dataBase.GetGoods();
            allGoodPage.allGoodsList.ItemsSource = goods;
            dataBase.Dispose();
        }

        private void OpenShoppingCart(object sender, MouseButtonEventArgs e)
        {
            ShoppingCartWnd scWnd = new ShoppingCartWnd();
            scWnd.Show();

            this.Close();
        }

        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            typeFilter.SelectedIndex = 0;
            firmFilter.SelectedIndex = 0;

            fromPriceFilter.Text = "0";
            toPriceFilter.Text = "";

            OutputGoods(sender, e);
        }
    }
}
