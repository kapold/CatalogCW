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

            if (String.IsNullOrEmpty(loginBoxRegistr.Text) || String.IsNullOrEmpty(passwordBoxRegistr.Password) ||
               String.IsNullOrEmpty(nameBoxRegistr.Text) || String.IsNullOrEmpty(repeatPasswordBoxRegistr.Password) ||
               String.IsNullOrEmpty(surnameBoxRegistr.Text) || String.IsNullOrEmpty(addressBoxRegistr.Text) ||
               String.IsNullOrEmpty(patronymicBoxRegistr.Text) || String.IsNullOrEmpty(phoneNumberBoxRegistr.Text))
            {

                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (passwordBoxRegistr.Password != repeatPasswordBoxRegistr.Password)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            if (passwordBoxRegistr.Password.Length < 8 || passwordBoxRegistr.Password.Length > 16)
            {
                MessageBox.Show("Пароль должен быть длиной от 8 до 16 символов!");
                return;
            }
            if (phoneNumberBoxRegistr.Text.Length != 12)
            {
                MessageBox.Show("Телефон должен быть 12 символов!");
                return;
            }
            if (nameBoxRegistr.Text.Contains(' ') || surnameBoxRegistr.Text.Contains(' ') || patronymicBoxRegistr.Text.Contains(' '))
            {
                MessageBox.Show("Проверьте чтобы в полях ФИО было по 1 слову");
                return;
            }
            if (loginBoxRegistr.Text.Length < 8 || loginBoxRegistr.Text.Length > 16)
            {
                MessageBox.Show("Логин должен быть длиной от 8 до 16 символов!");
                return;
            }

            try
            {
                Convert.ToInt64(phoneNumberBoxRegistr.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте номер телефона!\n" +
                    "Шаблон: 375298689745 (12 цифр)");
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

        // Подсказки для полей регистрации
        private void NumberSelection(object sender, RoutedEventArgs e)
        {
            RegisterWnd.regWnd.notificationBox.Text = "* Номер телефона должен быть без пробелов, знака + и длиной 12 символов.\n" +
                "Шаблон: 375XXXXXXXXX";
        }

        private void LoginSelection(object sender, RoutedEventArgs e)
        {
            RegisterWnd.regWnd.notificationBox.Text = "* Логин должен быть длиной от 8 до 16 знаков.";
        }

        private void FIOSelection(object sender, RoutedEventArgs e)
        {
            RegisterWnd.regWnd.notificationBox.Text = "* Поля Имя, Фамилия и Отечество должны содержать по одному слову.";
        }

        private void AddressSelection(object sender, RoutedEventArgs e)
        {
            RegisterWnd.regWnd.notificationBox.Text = "* Адрес не должен быть больше 64 символов.";
        }

        private void PasswordSelection(object sender, RoutedEventArgs e)
        {
            RegisterWnd.regWnd.notificationBox.Text = "* Пароль должен быть длиной от 8 до 16 символов.";
        }
    }
}
