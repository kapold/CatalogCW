using Catalog.Classes;
using Catalog.Pages;
using Catalog.Wndows;
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
        public MainWindow()
        {
            InitializeComponent();
            mainWindow = this;
            navigationService = goodsFrame.NavigationService;
            navigationService.Navigate(allGoodPage);

            // Скрываем админ-панель, если пользователь не админ
            if (!Auth.currentUser.IsAdmin)
            {
                adminPanelItem.IsEnabled = false;
                adminPanelItem.Visibility = Visibility.Hidden;
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
    }
}
