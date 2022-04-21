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
            
            if(String.IsNullOrEmpty(loginBoxRegistr.Text) || String.IsNullOrEmpty(passwordBoxRegistr.Password) ||
               String.IsNullOrEmpty(nameBoxRegistr.Text) || String.IsNullOrEmpty(repeatPasswordBoxRegistr.Password) ||
               String.IsNullOrEmpty(surnameBoxRegistr.Text) || String.IsNullOrEmpty(addressBoxRegistr.Text) ||
               String.IsNullOrEmpty(patronymicBoxRegistr.Text) || String.IsNullOrEmpty(phoneNumberBoxRegistr.Text))
            {

                MessageBox.Show("Заполните все поля!");
                return;
            }
            if(passwordBoxRegistr.Password != repeatPasswordBoxRegistr.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
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
                repeatPasswordBoxRegistr.Password = null;
                RegisterWnd.regWnd.AuthorizationPageSelect(sender, e);
            }
        }
    }
}
