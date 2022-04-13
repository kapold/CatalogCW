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
using System.Windows.Navigation;
using System.Windows.Resources;
using System.Windows.Shapes;

namespace Catalog.Wndows
{
    /// <summary>
    /// Логика взаимодействия для RegisterWnd.xaml
    /// </summary>
    public partial class RegisterWnd : Window
    {
        // Подключение к БД
        static string connectionString = "Server=.;Database=CatalogDB;Encrypt=False;Trusted_Connection=True";
        // Для закрытия формы регистрации
        public static RegisterWnd regWnd;
        // Кадры
        public NavigationService navigationService;
        public Authorization authorizationPage = new Authorization();
        public Registration registrationPage = new Registration();

        public RegisterWnd()
        {
            InitializeComponent();
            regWnd = this;
            // Для навигации по фреймам
            navigationService = mainFrame.NavigationService;
            // Новый курсор
            StreamResourceInfo sri = Application.GetResourceStream(new Uri("Images/CursorBlack.cur", UriKind.Relative));
            Cursor customCursor = new Cursor(sri.Stream);
            this.Cursor = customCursor;
        }

        public void AuthorizationPageSelect(object sender, RoutedEventArgs e)
        {
            navigationService.Navigate(authorizationPage);
        }

        private void RegistrationPageSelect(object sender, RoutedEventArgs e)
        {
            navigationService.Navigate(registrationPage);
        }
    }
}
