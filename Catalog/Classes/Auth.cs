using Catalog.Wndows;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Catalog.Classes
{
    public static class Auth
    {
        static string connectionString = "Server=.;Database=CatalogDB;Encrypt=False;Trusted_Connection=True;";
        public static User? currentUser;

        public static void TrySignIn(string nickname, string password)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Подключаемся к БД
                string sqlExpression = $"SELECT * FROM Users WHERE Password = '{password}' AND Login = '{nickname}'";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        if (reader.Read()) // построчно считываем данные
                        {
                            object ID = reader["UserID"];
                            object IsAdmin = reader["IsAdmin"];
                            object Login = reader["Login"];
                            object Password = reader["Password"];
                            object Name = reader["Name"];
                            object Surname = reader["Surname"];
                            object Patronymic = reader["Patronymic"];
                            object PhoneNumber = reader["PhoneNumber"];
                            object Address = reader["Address"];
                            MessageBox.Show($"{ID} {Login} {Name} {Surname} {Patronymic} {Password} \n{PhoneNumber} {Address} {IsAdmin}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Проверьте логин и пароль!");
                        return;
                    }
                }
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            RegisterWnd.regWnd.Close();
        }

        public static void TryRegister()
        {

        }
    }
}
