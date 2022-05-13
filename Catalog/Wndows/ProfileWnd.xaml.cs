using Catalog.Classes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Catalog.Wndows
{
    /// <summary>
    /// Логика взаимодействия для ProfileWnd.xaml
    /// </summary>
    public partial class ProfileWnd : Window
    {
        //string connectionString = "Server=.;Database=CatalogDB;Encrypt=False;Trusted_Connection=True";
        //string savedUserLogin = "";
        public ProfileWnd()
        {
            InitializeComponent();

            //savedUserLogin = Auth.currentUser.Login;
            profileLogin.Text = Auth.currentUser.Login;
            profilePassword.Text = "********";
            profileName.Text = Auth.currentUser.Name;
            profileSurname.Text = Auth.currentUser.Surname;
            profilePatronymic.Text = Auth.currentUser.Patronymic;
            profilePhone.Text = Auth.currentUser.PhoneNumber;
            profileAddress.Text = Auth.currentUser.Address;
        }

        private void RedactAccount(object sender, RoutedEventArgs e)
        {
            DataBase dataBase = new DataBase();

            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    string checkUserSql = $"SELECT * FROM Users WHERE Login = '{profileLogin.Text}'";
            //    SqlCommand commandCheck = new SqlCommand(checkUserSql, connection);
            //    SqlDataReader checkReader = commandCheck.ExecuteReader();
            //    string loginFromDB = "";
            //    if (checkReader.HasRows) // Проверяем есть ли такой User в БД
            //    {
            //        while (checkReader.Read()) // построчно считываем данные
            //        {
            //            loginFromDB = checkReader["Login"].ToString();
            //        }

            //        if(loginFromDB == )
            //        checkReader.Close();
            //        MessageBox.Show($"Такой логин занят!");
            //        connection.Close();
            //        return;
            //    }
            //    connection.Close();
            //}

            try
            {
                if (profilePassword.Text != "********")
                {
                    Auth.currentUser.Password = profilePassword.Text;
                }
                Auth.currentUser.Login = profileLogin.Text;
                Auth.currentUser.Name = profileName.Text;
                Auth.currentUser.Surname = profileSurname.Text;
                Auth.currentUser.Patronymic = profilePatronymic.Text;
                Auth.currentUser.PhoneNumber = profilePhone.Text;
                Auth.currentUser.Address = profileAddress.Text;

                dataBase.RedactUser(Auth.currentUser);
                dataBase.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteAccount(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы действительно хотите удалить аккаунт?", "Удаление профиля", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                RegisterWnd registerWnd = new RegisterWnd();
                registerWnd.Show();
                this.Close();

                Auth.DeleteUser(Auth.currentUser.ID);
                Auth.currentUser = null;
                MessageBox.Show("Аккаунт успешно удален!");
            }
        }

        private void ProfileExit(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            this.Close();
        }
    }
}
