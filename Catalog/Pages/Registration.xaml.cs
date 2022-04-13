using Catalog.Classes;
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

namespace Catalog.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void RegisterBtn(object sender, RoutedEventArgs e)
        {
            int isAdministractor = 0;
            if(isAdminBoxRegistr.Text == "Да" || isAdminBoxRegistr.Text == "Yes" || isAdminBoxRegistr.Text == "1" || isAdminBoxRegistr.Text == "True")
            {
                isAdministractor = 1;
            }
            Auth.TryRegister(loginBoxRegistr.Text, passwordBoxRegistr.Password.ToString(), nameBoxRegistr.Text, surnameBoxRegistr.Text, patronymicBoxRegistr.Text, isAdministractor, addressBoxRegistr.Text, phoneNumberBoxRegistr.Text);

            // Чищу поля и возвращаюсь к Входу
            if(Auth.isSucessfullQuery)
            {
                loginBoxRegistr.Text = null;
                passwordBoxRegistr.Password = null;
                nameBoxRegistr.Text = null;
                surnameBoxRegistr.Text = null;
                patronymicBoxRegistr.Text = null;
                addressBoxRegistr.Text = null;
                phoneNumberBoxRegistr.Text = null;
                isAdminBoxRegistr.Text = null;
                RegisterWnd.regWnd.AuthorizationPageSelect(sender, e);
            }
        }
    }
}
