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
        public static User currentUser;
        public static bool isSucessfullQuery = false;

        public static void TrySignIn(string nickname, string password)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); // Подключаемся к БД
                password = Crypto.GetHash(password); // sha256
                string sqlExpression = $"SELECT * FROM Users WHERE Password = '{password}' AND Login = '{nickname}'";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        if (reader.Read()) // построчно считываем данные
                        {
                            currentUser = new User();
                            object ID = reader["UserID"];
                            object IsAdmin = reader["IsAdmin"];
                            object Login = reader["Login"];
                            object Password = reader["Password"];
                            object Name = reader["Name"];
                            object Surname = reader["Surname"];
                            object Patronymic = reader["Patronymic"];
                            object PhoneNumber = reader["PhoneNumber"];
                            object Address = reader["Address"];

                            currentUser.ID = Convert.ToInt32(ID);
                            currentUser.IsAdmin = Convert.ToBoolean(IsAdmin);
                            currentUser.Login = Convert.ToString(Login);
                            currentUser.Password = Convert.ToString(Password);
                            currentUser.Name = Convert.ToString(Name);
                            currentUser.Surname = Convert.ToString(Surname);
                            currentUser.Patronymic = Convert.ToString(Patronymic);
                            currentUser.PhoneNumber = Convert.ToString(PhoneNumber);
                            currentUser.Address = Convert.ToString(Address);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Проверьте логин и пароль!");
                        connection.Close();
                        return;
                    }
                }
                connection.Close();
            }
            // MessageBox.Show(currentUser.ToString()); // User info

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            XmlSerializator.SerializeUser(currentUser);

            RegisterWnd.regWnd.Close();
        }

        public static void TryRegister(string login, string password, string name, string surname, string patronymic, int isAdmin, string address, string phoneNumber)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                password = Crypto.GetHash(password); // sha256
                string sqlExpression = $"INSERT INTO Users(isAdmin, login, password, name, surname, patronymic, phoneNumber, Address)" +
                                       $"   VALUES(0, '{login}', '{password}', '{name}', '{surname}', '{patronymic}', '{phoneNumber}', '{address}')";
                string checkUserSql = $"SELECT * FROM Users WHERE Login = '{login}'";

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlCommand commandForCheck = new SqlCommand(checkUserSql, connection);
                //command.Parameters.Add("@login", login);

                SqlDataReader checkReader = commandForCheck.ExecuteReader();
                if (!checkReader.HasRows) // Проверяем есть ли такой User в БД
                {
                    checkReader.Close();
                    command.ExecuteNonQuery();
                    MessageBox.Show($"Регистрация прошла успешно!");
                    isSucessfullQuery = true;
                }
                else
                {
                    MessageBox.Show("Такой пользователь зарегистрирован!");
                    isSucessfullQuery = false;
                    connection.Close();
                    return;
                }

                connection.Close();
            }
        }

        public static void DeleteUser(int id)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sqlDelete = $"DELETE FROM Users WHERE UserID = {id}";

                try
                {
                    SqlCommand commandDelete = new SqlCommand(sqlDelete, connection);
                    commandDelete.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                connection.Close();
            }
        }
    }
}
