using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Catalog.Classes
{
    public class DataBase : IDisposable
    {
        string connectionString;
        private SqlConnection connection;

        public DataBase()
        {
            connectionString = "Server=.;Database=CatalogDB;Encrypt=False;Trusted_Connection=True";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            if (connection != null)
                connection.Close();
        }

        public void AddGood(Good g)
        {
            //SqlTransaction transaction = connection.BeginTransaction();
            //SqlCommand commandTrans = connection.CreateCommand();
            //commandTrans.Transaction = transaction;
            // Редактирую приближенные значения(в c# и sql разные знаки)
            string redactedPrice = g.Price.ToString().Replace(',', '.');
            string redactedDisplay = g.Display.ToString().Replace(',', '.');

            string sqlGoods = $"INSERT INTO Goods(Name, Price, GoodCount, Firm, Description, ImageSrc, GoodType) " +
                $"VALUES ('{g.Name}', {redactedPrice}, {g.Count}, '{g.Firm}', '{g.Description}', '{g.ImageSrc}', '{g.Type}');";
            string sqlCopyID = $"SELECT TOP(1) GoodID FROM Goods ORDER BY Goods.GoodID DESC; ";


            int id = 0;
            try
            {
                SqlCommand commandGoods = new SqlCommand(sqlGoods, connection);
                commandGoods.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            SqlCommand commandCopy = new SqlCommand(sqlCopyID, connection);
            using (SqlDataReader reader = commandCopy.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    if (reader.Read()) // построчно считываем данные
                    {
                        id = Convert.ToInt32(reader["GoodID"]);
                    }
                }
            }


            string sqlParams = $"INSERT INTO Params(Good_ID, Display, DisplayType, Resolution, Hertz, CPU, RAM, ROM, Color, OS, Battery, Camera, NFC) " +
                $"VALUES ({id}, {redactedDisplay}, '{g.DisplayType}', '{g.Resolution}', {g.Hertz}, '{g.CPU}', {g.RAM}, {g.ROM}, '{g.Color}', '{g.OS}', {g.Battery}, {g.Camera}, {Convert.ToInt32(g.NFC)});";

            try
            {
                SqlCommand commandParams = new SqlCommand(sqlParams, connection);
                commandParams.ExecuteNonQuery();
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //transaction.Rollback();
            }
        }

        public List<Good> GetGoods()
        {
            Good good;
            List<Good> goods = new List<Good>();
            string sql = $"SELECT * FROM Goods as g INNER JOIN Params as p ON g.GoodID = p.Good_ID ORDER BY g.GoodID";
            SqlCommand commandGoods = new SqlCommand(sql, connection);
            using(SqlDataReader reader = commandGoods.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while(reader.Read()) // построчно считываем данные
                    {
                        good = new Good();
                        // Основные
                        good.ID = Convert.ToInt32(reader["GoodID"]);
                        good.Name = reader["Name"].ToString();
                        good.Price = Convert.ToDouble(reader["Price"]);
                        good.Count = Convert.ToInt32(reader["GoodCount"]);
                        good.Firm = reader["Firm"].ToString();
                        good.Description = reader["Description"].ToString();
                        good.ImageSrc = reader["ImageSrc"].ToString();
                        good.Type = reader["GoodType"].ToString();
                        // Параметры
                        good.Display = Convert.ToDouble(reader["Display"]);
                        good.DisplayType = reader["DisplayType"].ToString();
                        good.Resolution = reader["Resolution"].ToString();
                        good.Hertz = Convert.ToInt32(reader["Hertz"]);
                        good.CPU = reader["CPU"].ToString();
                        good.RAM = Convert.ToInt32(reader["RAM"]);
                        good.ROM = Convert.ToInt32(reader["ROM"]);
                        good.Color = reader["Color"].ToString();
                        good.OS = reader["OS"].ToString();
                        good.Battery = Convert.ToInt32(reader["Battery"]);
                        good.Camera = Convert.ToInt32(reader["Camera"]);
                        good.NFC = Convert.ToBoolean(reader["NFC"]);

                        good.GoodLink = good;

                        goods.Add(good);
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка в получении коллекции товаров!");
                    connection.Close();
                    return null;
                }
            }

            return goods;
        }

        public void UpdateGood(int id, Good g)
        {
            // Редактирую приближенные значения(в c# и sql разные знаки)
            string redactedPrice = g.Price.ToString().Replace(',', '.');
            string redactedDisplay = g.Display.ToString().Replace(',', '.');

            string sqlUpdateGoods = $"UPDATE Goods SET Name = '{g.Name}', Price = {redactedPrice}, GoodCount = {g.Count}, Firm = '{g.Firm}', Description = '{g.Description}', ImageSrc = '{g.ImageSrc}', GoodType = '{g.Type}' WHERE GoodID = {id}";
            string sqlUpdateParams = $"UPDATE Params SET Display = {redactedDisplay}, DisplayType = '{g.DisplayType}', Resolution = '{g.Resolution}', Hertz = {g.Hertz}, CPU = '{g.CPU}', RAM = {g.RAM}, ROM = {g.ROM}, Color = '{g.Color}', OS = '{g.OS}', Battery = {g.Battery}, Camera = {g.Camera}, NFC = {Convert.ToInt32(g.NFC)} WHERE Good_ID = {id}";

            try
            {
                SqlCommand commandGoods = new SqlCommand(sqlUpdateGoods, connection);
                commandGoods.ExecuteNonQuery();

                SqlCommand commandParams = new SqlCommand(sqlUpdateParams, connection);
                commandParams.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteGood(int id)
        {
            string sqlGoods = $"DELETE FROM Goods WHERE GoodID = {id}";
            string sqlParams = $"DELETE FROM Params WHERE Good_ID = {id}";

            try
            {
                SqlCommand commandParams = new SqlCommand(sqlParams, connection);
                commandParams.ExecuteNonQuery();

                SqlCommand commandGoods = new SqlCommand(sqlGoods, connection);
                commandGoods.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RedactUser(User user)
        {
            User redactedUser = new User();

            string sqlRedact = $"UPDATE Users SET Password = '{user.Password}', Name = '{user.Name}', Surname = '{user.Surname}', Patronymic = '{user.Patronymic}', PhoneNumber = '{user.PhoneNumber}', Address = '{user.Address}' WHERE UserID = {user.ID}";

            try
            {
                SqlCommand commandRedact = new SqlCommand(sqlRedact, connection);
                commandRedact.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddOrder(Good g, User u)
        {
            string sqlOrder = $"INSERT INTO Orders(GoodID, OrderedCount, IsOrdered, Customer) " +
                $"VALUES ({g.ID}, 1, 0, {u.ID})";

            try
            {
                SqlCommand commandOrders = new SqlCommand(sqlOrder, connection);
                commandOrders.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteOrder(Order order)
        {
            string sqlOrder = $"DELETE FROM Orders WHERE OrderNo = {order.OrderNo}";

            try
            {
                SqlCommand commandOrders = new SqlCommand(sqlOrder, connection);
                commandOrders.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<Order> GetOrders()
        {
            Order order;
            List<Order> orders = new List<Order>();
            string sql = $"SELECT * FROM Orders as o INNER JOIN Goods as g on o.GoodID = g.GoodID INNER JOIN Users as u on o.Customer = u.UserID INNER JOIN Params as p on p.Good_ID = g.GoodID WHERE UserID = {Auth.currentUser.ID} and IsOrdered = 0";
            SqlCommand commandOrders = new SqlCommand(sql, connection);

            using (SqlDataReader reader = commandOrders.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        order = new Order();
                        order.OrderNo = Convert.ToInt32(reader["OrderNo"]);
                        order.GoodID = Convert.ToInt32(reader["GoodID"]);
                        order.GoodCount = 1;
                        order.IsOrdered = Convert.ToBoolean(reader["IsOrdered"]);
                        order.Customer = Convert.ToInt32(reader["Customer"]);

                        // Good in Order
                        order.Good.ID = Convert.ToInt32(reader["GoodID"]);
                        order.Good.Name = reader["Name"].ToString();
                        order.Good.Price = Convert.ToDouble(reader["Price"]);
                        order.Good.Count = Convert.ToInt32(reader["GoodCount"]);
                        order.Good.Firm = reader["Firm"].ToString();
                        order.Good.Description = reader["Description"].ToString();
                        order.Good.ImageSrc = reader["ImageSrc"].ToString();
                        order.Good.Type = reader["GoodType"].ToString();
                        // Params
                        order.Good.Display = Convert.ToDouble(reader["Display"]);
                        order.Good.DisplayType = reader["DisplayType"].ToString();
                        order.Good.Resolution = reader["Resolution"].ToString();
                        order.Good.Hertz = Convert.ToInt32(reader["Hertz"]);
                        order.Good.CPU = reader["CPU"].ToString();
                        order.Good.RAM = Convert.ToInt32(reader["RAM"]);
                        order.Good.ROM = Convert.ToInt32(reader["ROM"]);
                        order.Good.Color = reader["Color"].ToString();
                        order.Good.OS = reader["OS"].ToString();
                        order.Good.Battery = Convert.ToInt32(reader["Battery"]);
                        order.Good.Camera = Convert.ToInt32(reader["Camera"]);
                        order.Good.NFC = Convert.ToBoolean(reader["NFC"]);

                        order.Good.GoodLink = order.Good;
                        order.OrderLink = order;

                        MessageBox.Show(order.Good.GoodLink.ToString());

                        orders.Add(order);
                    }
                }
                else
                {
                    //MessageBox.Show("Ошибка в получении коллекции заказов!");
                    connection.Close();
                    return null;
                }
            }

            return orders;
        }

        public List<Delivery> GetDeliveries()
        {
            Delivery delivery;
            List<Delivery> deliveries = new List<Delivery>();
            string sql = $"SELECT * FROM Deliveries as d INNER JOIN Orders as o ON d.OrderID = o.OrderNo INNER JOIN Goods as g ON o.GoodID = g.GoodID INNER JOIN Users as u ON u.UserID = o.Customer WHERE UserID = {Auth.currentUser.ID} and IsOrdered = 1";
            SqlCommand commandDeliveries = new SqlCommand(sql, connection);

            using (SqlDataReader reader = commandDeliveries.ExecuteReader())
            {
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        delivery = new Delivery();
                        delivery.DeliveryID = Convert.ToInt32(reader["DeliveryID"]);
                        delivery.OrderID = Convert.ToInt32(reader["OrderID"]);
                        delivery.DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]);
                        delivery.DeliveryAddress = reader["DeliveryAddress"].ToString();

                        delivery.Order.OrderNo = Convert.ToInt32(reader["OrderNo"]);
                        delivery.Order.GoodCount = Convert.ToInt32(reader["OrderedCount"]);

                        delivery.Order.Good.ImageSrc = reader["ImageSrc"].ToString();
                        delivery.Order.Good.Name = reader["Name"].ToString();
                        delivery.Order.Good.Price = Convert.ToDouble(reader["Price"]);

                        delivery.DeliveryLink = delivery;

                        deliveries.Add(delivery);
                    }
                }
                else
                {
                    //MessageBox.Show("Ошибка в получении коллекции доставок!");
                    connection.Close();
                    return null;
                }
            }

            return deliveries;
        }
    }
}
