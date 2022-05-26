using Catalog.Classes;
using Catalog.Pages;
using Catalog.Patterns;
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
        public string forPrice = "";
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            navigationService = goodsFrame.NavigationService;
            navigationService.Navigate(allGoodPage);
            DataBase dataBase = new DataBase();
            DataContext = new ApplicationsViewModel();

            typeFilter.ItemsSource = dataBase.GetGoodTypesALL();
            displayTypeFilter.ItemsSource = dataBase.GetDisplayTypesALL();
            resolutionFilter.ItemsSource = dataBase.GetResolutionsALL();
            hertzFilter.ItemsSource = dataBase.GetHertzALL();
            ramFilter.ItemsSource= dataBase.GetRAMALL();
            romFilter.ItemsSource = dataBase.GetROMALL();
            colorFilter.ItemsSource = dataBase.GetColorsALL();
            firmFilter.ItemsSource = dataBase.GetFirmsALL();
            osFilter.ItemsSource = dataBase.GetOSALL();

            typeFilter.SelectedIndex = 0;
            firmFilter.SelectedIndex = 0;
            displayTypeFilter.SelectedIndex = 0;
            resolutionFilter.SelectedIndex = 0;
            ramFilter.SelectedIndex = 0;
            romFilter.SelectedIndex = 0;
            hertzFilter.SelectedIndex = 0;
            colorFilter.SelectedIndex = 0;
            osFilter.SelectedIndex = 0;
            fromPriceFilter.Text = "0";

            // Цена в филтре из БД
            try
            {
                goodsMainWnd = dataBase.GetGoods();
                dataBase.Dispose();
                List<Good> forPriceFilter = goodsMainWnd.OrderByDescending(x => x.Price).ToList();
                toPriceFilter.Text = Convert.ToString(forPriceFilter[0].Price);
                forPrice = Convert.ToString(forPriceFilter[0].Price);
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Фильтр цены не установлен(нет товаров)");
            }

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
            RegisterWnd registerWnd = new RegisterWnd(true);
            registerWnd.Show();
            // Обнуляем пользователя
            Auth.currentUser = null;
            XmlSerializator.SerializeUser(Auth.currentUser);
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
            DataBase dataBase = new DataBase();
            goodsMainWnd = dataBase.GetGoods();
            dataBase.Dispose();
            allGoodPage.allGoodsList.ItemsSource = goodsMainWnd;
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
            try
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
            catch { }
        }

        // Функция общей фильтрации
        private void FilterChanged(object sender, RoutedEventArgs e)
        {
            DataBase dataBase = new DataBase();
            filterGoods = dataBase.GetGoods();

            try
            {
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
                // Фильтр-дисплей(тип)
                ComboBoxItem displayTypeCBI = displayTypeFilter.SelectedItem as ComboBoxItem;
                if (displayTypeFilter.SelectedIndex < 0 || displayTypeFilter.SelectedIndex == 0)
                {
                    filterGoods = filterGoods;
                }
                else if (displayTypeFilter.SelectedIndex > 0)
                {
                    filterGoods = filterGoods.Where(g => g.DisplayType == displayTypeCBI.Content.ToString()).ToList();
                }
                // Фильтр-разрешение
                ComboBoxItem resolutionCBI = resolutionFilter.SelectedItem as ComboBoxItem;
                if (resolutionFilter.SelectedIndex < 0 || resolutionFilter.SelectedIndex == 0)
                {
                    filterGoods = filterGoods;
                }
                else if (resolutionFilter.SelectedIndex > 0)
                {
                    filterGoods = filterGoods.Where(g => g.Resolution == resolutionCBI.Content.ToString()).ToList();
                }
                // Фильтр-герцовка
                ComboBoxItem hertzCBI = hertzFilter.SelectedItem as ComboBoxItem;
                if (hertzFilter.SelectedIndex < 0 || hertzFilter.SelectedIndex == 0)
                {
                    filterGoods = filterGoods;
                }
                else if (hertzFilter.SelectedIndex > 0)
                {
                    filterGoods = filterGoods.Where(g => g.Hertz == Convert.ToInt32(hertzCBI.Content.ToString())).ToList();
                }
                // Фильтр-операционка
                ComboBoxItem osCBI = osFilter.SelectedItem as ComboBoxItem;
                if (osFilter.SelectedIndex < 0 || osFilter.SelectedIndex == 0)
                {
                    filterGoods = filterGoods;
                }
                else if (osFilter.SelectedIndex > 0)
                {
                    filterGoods = filterGoods.Where(g => g.OS == osCBI.Content.ToString()).ToList();
                }
                // Фильтр-процессор
                if (!String.IsNullOrEmpty(cpuFilter.Text))
                {
                    Regex regex = new Regex(cpuFilter.Text.ToUpper());
                    for (int i = filterGoods.Count - 1; i >= 0; i--)
                    {
                        if (!regex.IsMatch(filterGoods[i].CPU.ToUpper()))
                        {
                            filterGoods.Remove(filterGoods[i]);
                        }
                    }
                }
                // Фильтр-оперативка
                ComboBoxItem ramCBI = ramFilter.SelectedItem as ComboBoxItem;
                if (ramFilter.SelectedIndex < 0 || ramFilter.SelectedIndex == 0)
                {
                    filterGoods = filterGoods;
                }
                else if (ramFilter.SelectedIndex > 0)
                {
                    filterGoods = filterGoods.Where(g => g.RAM == Convert.ToInt32(ramCBI.Content.ToString())).ToList();
                }
                // Фильтр-емкость хранилища
                ComboBoxItem romCBI = romFilter.SelectedItem as ComboBoxItem;
                if (romFilter.SelectedIndex < 0 || romFilter.SelectedIndex == 0)
                {
                    filterGoods = filterGoods;
                }
                else if (romFilter.SelectedIndex > 0)
                {
                    filterGoods = filterGoods.Where(g => g.ROM == Convert.ToInt32(romCBI.Content.ToString())).ToList();
                }
                // Фильтр-цвет
                ComboBoxItem colorCBI = colorFilter.SelectedItem as ComboBoxItem;
                if (colorFilter.SelectedIndex < 0 || colorFilter.SelectedIndex == 0)
                {
                    filterGoods = filterGoods;
                }
                else if (colorFilter.SelectedIndex > 0)
                {
                    filterGoods = filterGoods.Where(g => g.Color == colorCBI.Content.ToString()).ToList();
                }
                // Фильтр-батарея
                if (!String.IsNullOrEmpty(fromBatteryFilter.Text) && !String.IsNullOrEmpty(toBatteryFilter.Text))
                {
                    filterGoods = filterGoods.Where(g => g.Battery >= Convert.ToDouble(fromBatteryFilter.Text) && g.Battery <= Convert.ToDouble(toBatteryFilter.Text)).ToList();
                }
                // Фильтр-камера
                if (!String.IsNullOrEmpty(cameraFilter.Text))
                {
                    filterGoods = filterGoods.Where(g => g.Camera == Convert.ToDouble(cameraFilter.Text)).ToList();
                }
                // Фильтр-NFC
                if ((bool)nfcOK.IsChecked)
                {
                    filterGoods = filterGoods.Where(g => g.NFC == true).ToList();
                }
                else if ((bool)nfcNone.IsChecked)
                {
                    filterGoods = filterGoods.Where(g => g.NFC == false).ToList();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Фильтр задан неверно!");
                OutputGoods(sender, e);
            }

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
            displayTypeFilter.SelectedIndex = 0;
            resolutionFilter.SelectedIndex = 0;
            hertzFilter.SelectedIndex = 0;
            ramFilter.SelectedIndex = 0;
            romFilter.SelectedIndex = 0;
            colorFilter.SelectedIndex = 0;
            nfcOK.IsChecked = false;
            nfcNone.IsChecked = false;

            fromPriceFilter.Text = "0";
            toPriceFilter.Text = forPrice;
            displayFilter.Text = "";

            OutputGoods(sender, e);
        }
    }
}
