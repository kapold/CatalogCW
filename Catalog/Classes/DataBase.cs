using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
            //commandTrans.Transaction = transaction;d
            // Редактирую приближенные значения(в c# и sql разные знаки)
            string redactedPrice = g.Price.ToString().Replace(',', '.');
            string redactedDisplay = g.Display.ToString().Replace(',', '.');

            string sqlGoods = $"INSERT INTO Goods(Name, Price, GoodCount, Firm, Description, ImageSrc, GoodType) " +
                $"VALUES (@gName, @redactedPrice, @gCount, @gFirm, @gDescription, @gImageSrc, @gType);";
            string sqlCopyID = $"SELECT TOP(1) GoodID FROM Goods ORDER BY Goods.GoodID DESC; ";


            int id = 0;
            try
            {
                SqlCommand commandGoods = new SqlCommand(sqlGoods, connection);
                commandGoods.Parameters.Add(new SqlParameter("@gName", g.Name));
                commandGoods.Parameters.Add(new SqlParameter("@redactedPrice", redactedPrice));
                commandGoods.Parameters.Add(new SqlParameter("@gCount", g.Count));
                commandGoods.Parameters.Add(new SqlParameter("@gFirm", g.Firm));
                commandGoods.Parameters.Add(new SqlParameter("@gDescription", g.Description));
                commandGoods.Parameters.Add(new SqlParameter("@gImageSrc", g.ImageSrc));
                commandGoods.Parameters.Add(new SqlParameter("@gType", g.Type));

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
                $"VALUES (@id, @redactedDisplay, @gDisplayType, @gResolution, @gHertz, @gCPU, @gRAM, @gROM, @gColor, @gOS, @gBattery, @gCamera, @gNFC);";

            try
            {
                SqlCommand commandParams = new SqlCommand(sqlParams, connection);
                commandParams.Parameters.Add(new SqlParameter("@id", id));
                commandParams.Parameters.Add(new SqlParameter("@redactedDisplay", redactedDisplay));
                commandParams.Parameters.Add(new SqlParameter("@gDisplayType", g.DisplayType));
                commandParams.Parameters.Add(new SqlParameter("@gResolution", g.Resolution));
                commandParams.Parameters.Add(new SqlParameter("@gHertz", g.Hertz));
                commandParams.Parameters.Add(new SqlParameter("@gCPU", g.CPU));
                commandParams.Parameters.Add(new SqlParameter("@gRAM", g.RAM));
                commandParams.Parameters.Add(new SqlParameter("@gROM", g.ROM));
                commandParams.Parameters.Add(new SqlParameter("@gColor", g.Color));
                commandParams.Parameters.Add(new SqlParameter("@gOS", g.OS));
                commandParams.Parameters.Add(new SqlParameter("@gBattery", g.Battery));
                commandParams.Parameters.Add(new SqlParameter("@gCamera", g.Camera));
                commandParams.Parameters.Add(new SqlParameter("@gNFC", Convert.ToInt32(g.NFC)));

                commandParams.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public List<Good> GetGoods()
        {
            Good good;
            List<Good> goods = new List<Good>();
            string sql = $"SELECT * FROM Goods as g INNER JOIN Params as p ON g.GoodID = p.Good_ID WHERE GoodCount > 0 ORDER BY g.GoodID";
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
                    //MessageBox.Show("Ошибка в получении коллекции товаров!");
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

            string sqlUpdateGoods = $"UPDATE Goods SET Name = @gName, Price = @redactedPrice, GoodCount = @gCount, Firm = @gFirm, Description = @gDescription, ImageSrc = @gImageSrc, GoodType = @gType WHERE GoodID = @id";
            string sqlUpdateParams = $"UPDATE Params SET Display = @redactedDisplay, DisplayType = @gDisplayType, Resolution = @gResolution, Hertz = @gHertz, CPU = @gCPU, RAM = @gRAM, ROM = @gROM, Color = @gColor, OS = @gOS, Battery = @gBattery, Camera = @gCamera, NFC = @gNFC WHERE Good_ID = @id";

            try
            {
                SqlCommand commandGoods = new SqlCommand(sqlUpdateGoods, connection);
                commandGoods.Parameters.Add(new SqlParameter("@gName", g.Name));
                commandGoods.Parameters.Add(new SqlParameter("@redactedPrice", redactedPrice));
                commandGoods.Parameters.Add(new SqlParameter("@gCount", g.Count));
                commandGoods.Parameters.Add(new SqlParameter("@gFirm", g.Firm));
                commandGoods.Parameters.Add(new SqlParameter("@gDescription", g.Description));
                commandGoods.Parameters.Add(new SqlParameter("@gImageSrc", g.ImageSrc));
                commandGoods.Parameters.Add(new SqlParameter("@gType", g.Type));
                commandGoods.Parameters.Add(new SqlParameter("@id", id));
                commandGoods.ExecuteNonQuery();

                SqlCommand commandParams = new SqlCommand(sqlUpdateParams, connection);
                commandParams.Parameters.Add(new SqlParameter("@id", id));
                commandParams.Parameters.Add(new SqlParameter("@redactedDisplay", redactedDisplay));
                commandParams.Parameters.Add(new SqlParameter("@gDisplayType", g.DisplayType));
                commandParams.Parameters.Add(new SqlParameter("@gResolution", g.Resolution));
                commandParams.Parameters.Add(new SqlParameter("@gHertz", g.Hertz));
                commandParams.Parameters.Add(new SqlParameter("@gCPU", g.CPU));
                commandParams.Parameters.Add(new SqlParameter("@gRAM", g.RAM));
                commandParams.Parameters.Add(new SqlParameter("@gROM", g.ROM));
                commandParams.Parameters.Add(new SqlParameter("@gColor", g.Color));
                commandParams.Parameters.Add(new SqlParameter("@gOS", g.OS));
                commandParams.Parameters.Add(new SqlParameter("@gBattery", g.Battery));
                commandParams.Parameters.Add(new SqlParameter("@gCamera", g.Camera));
                commandParams.Parameters.Add(new SqlParameter("@gNFC", Convert.ToInt32(g.NFC)));
                commandParams.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteGood(int id)
        {
            string sqlGoods = $"DELETE FROM Goods WHERE GoodID = @id";
            string sqlParams = $"DELETE FROM Params WHERE Good_ID = @id";

            try
            {
                SqlCommand commandParams = new SqlCommand(sqlParams, connection);
                commandParams.Parameters.Add(new SqlParameter("@id", id));
                commandParams.ExecuteNonQuery();

                SqlCommand commandGoods = new SqlCommand(sqlGoods, connection);
                commandGoods.Parameters.Add(new SqlParameter("@id", id));
                commandGoods.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RedactUser(User user)
        {
            user.Password = Crypto.GetHash(user.Password);
            string sqlRedact = $"UPDATE Users SET Login = @userLogin, Password = @userPassword, Name = @userName, Surname = @userSurname, Patronymic = @userPatronymic, PhoneNumber = @userPhoneNumber, Address = @userAddress WHERE UserID = @userID";

            try
            {
                SqlCommand commandRedact = new SqlCommand(sqlRedact, connection);
                commandRedact.Parameters.Add(new SqlParameter("@userLogin", user.Login));
                commandRedact.Parameters.Add(new SqlParameter("@userPassword", user.Password));
                commandRedact.Parameters.Add(new SqlParameter("@userName", user.Name));
                commandRedact.Parameters.Add(new SqlParameter("@userSurname", user.Surname));
                commandRedact.Parameters.Add(new SqlParameter("@userPatronymic", user.Patronymic));
                commandRedact.Parameters.Add(new SqlParameter("@userPhoneNumber", user.PhoneNumber));
                commandRedact.Parameters.Add(new SqlParameter("@userAddress", user.Address));
                commandRedact.Parameters.Add(new SqlParameter("@userID", user.ID));
                commandRedact.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Такой логин занят!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AddOrder(Good g, User u)
        {
            string sqlOrder = $"INSERT INTO Orders(GoodID, OrderedCount, IsOrdered, Customer) " +
                $"VALUES (@gID, 1, 0, @uID)";

            try
            {
                SqlCommand commandOrders = new SqlCommand(sqlOrder, connection);
                commandOrders.Parameters.Add(new SqlParameter("@gID", g.ID));
                commandOrders.Parameters.Add(new SqlParameter("@uID", u.ID));
                commandOrders.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteOrder(Order order)
        {
            string sqlOrder = $"DELETE FROM Orders WHERE OrderNo = @orderNo";

            try
            {
                SqlCommand commandOrders = new SqlCommand(sqlOrder, connection);
                commandOrders.Parameters.Add(new SqlParameter("@orderNo", order.OrderNo));
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

                        //MessageBox.Show(order.Good.GoodLink.ToString());

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

        public void AddDelivery(Delivery delivery)
        {
            string sqlDelivery = $"INSERT INTO Deliveries(OrderID, DeliveryDate, DeliveryAddress, PaymentType, DeliveryCount) " +
                $"VALUES (@deliveryOrderID, '{delivery.DeliveryDate}', @deliveryDeliveryAddress, @deliveryPaymentType, @deliveryDeliveryCount)";
            string sqlOrder = $"UPDATE Orders SET IsOrdered = 1 WHERE OrderNo = @deliveryOrderID";

            try
            {
                SqlCommand commandDeliveries = new SqlCommand(sqlDelivery, connection);
                commandDeliveries.Parameters.Add(new SqlParameter("@deliveryOrderID", delivery.OrderID));
                commandDeliveries.Parameters.Add(new SqlParameter("@deliveryDeliveryAddress", delivery.DeliveryAddress));
                commandDeliveries.Parameters.Add(new SqlParameter("@deliveryPaymentType", delivery.PaymentType));
                commandDeliveries.Parameters.Add(new SqlParameter("@deliveryDeliveryCount", delivery.DeliveryCount));
                commandDeliveries.ExecuteNonQuery();

                SqlCommand commandOrders = new SqlCommand(sqlOrder, connection);
                commandOrders.Parameters.Add(new SqlParameter("@deliveryOrderID", delivery.OrderID));
                commandOrders.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                        delivery.DeliveryDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["DeliveryDate"]));
                        delivery.DeliveryAddress = reader["DeliveryAddress"].ToString();
                        delivery.DeliveryCount = Convert.ToInt32(reader["DeliveryCount"]);
                        delivery.PaymentType = reader["PaymentType"].ToString();

                        delivery.Order.OrderNo = Convert.ToInt32(reader["OrderNo"]);
                        delivery.Order.GoodID = Convert.ToInt32(reader["GoodID"]);
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

        public List<Delivery> GetAllDeliveries()
        {
            Delivery delivery;
            List<Delivery> deliveries = new List<Delivery>();
            string sql = $"SELECT * FROM Deliveries as d INNER JOIN Orders as o ON d.OrderID = o.OrderNo INNER JOIN Goods as g ON o.GoodID = g.GoodID INNER JOIN Users as u ON u.UserID = o.Customer WHERE IsOrdered = 1";
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
                        delivery.DeliveryDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["DeliveryDate"]));
                        delivery.DeliveryAddress = reader["DeliveryAddress"].ToString();
                        delivery.DeliveryCount = Convert.ToInt32(reader["DeliveryCount"]);
                        delivery.PaymentType = reader["PaymentType"].ToString();

                        delivery.Order.OrderNo = Convert.ToInt32(reader["OrderNo"]);
                        delivery.Order.GoodCount = Convert.ToInt32(reader["OrderedCount"]);

                        delivery.Order.Good.ImageSrc = reader["ImageSrc"].ToString();
                        delivery.Order.Good.Name = reader["Name"].ToString();
                        delivery.Order.Good.Price = Convert.ToDouble(reader["Price"]);
                        delivery.Customer.Surname = reader["Surname"].ToString();

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

        public void DeleteDateProcedure()
        {
            string sqlProcedure = "DeleteExpiredDate";
            SqlCommand commandProcedure = new SqlCommand(sqlProcedure, connection);
            commandProcedure.CommandType = CommandType.StoredProcedure;

            try
            {
                commandProcedure.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CorrectCount(int count, string name)
        {
            string sqlCount = $"UPDATE Goods SET GoodCount = GoodCount - {count} WHERE Name = '{name}'";

            try
            {
                SqlCommand commandGoods = new SqlCommand(sqlCount, connection);
                commandGoods.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeleteDelivery(Delivery delivery)
        {
            string sqlDelivery = $"DELETE FROM Deliveries WHERE DeliveryID = {delivery.DeliveryID}";
            string sqlOrder = $"UPDATE Orders SET IsOrdered = 0 WHERE OrderNo = {delivery.Order.OrderNo}";
            string sqlGoodCount = $"UPDATE Goods SET GoodCount = GoodCount + {delivery.DeliveryCount} WHERE GoodID = {delivery.Order.GoodID}";

            try
            {
                SqlCommand commandDelivery = new SqlCommand(sqlDelivery, connection);
                commandDelivery.ExecuteNonQuery();

                SqlCommand commandOrder = new SqlCommand(sqlOrder, connection);
                commandOrder.ExecuteNonQuery();

                SqlCommand commandGoodCount = new SqlCommand(sqlGoodCount, connection);
                commandGoodCount.ExecuteNonQuery();

                MessageBox.Show("Отмена доставки!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Добавление параметров
        public void AddGoodType(string newType)
        {
            string sqlType = $"INSERT INTO GoodTypes(TypeName) " +
                $"VALUES (@newType)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@newType", newType));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("Тип товара успешно добавлен!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такой тип уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddDisplayType(string type)
        {
            string sqlType = $"INSERT INTO DisplayTypes(TypeName) " +
                $"VALUES (@type)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@type", type));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("Тип дисплея успешно добавлен!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такой тип уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddResolution(string resolution)
        {
            string sqlType = $"INSERT INTO Resolutions(ResolutionName) " +
                $"VALUES (@resolution)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@resolution", resolution));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("Разрешение успешно добавлено!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такое разрешение уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddHertz(string hertz)
        {
            string sqlType = $"INSERT INTO Hertzs(HertzName) " +
                $"VALUES (@hertz)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@hertz", hertz));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("Гц успешно добавлено!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такое Гц уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddRAM(string ram)
        {
            string sqlType = $"INSERT INTO RAMs(RAMName) " +
                $"VALUES (@ram)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@ram", ram));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("RAM успешно добавлено!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такое RAM уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddROM(string rom)
        {
            string sqlType = $"INSERT INTO ROMs(ROMName) " +
                $"VALUES (@rom)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@rom", rom));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("ROM успешно добавлено!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такое ROM уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddOS(string os)
        {
            string sqlType = $"INSERT INTO OS(OSName) " +
                $"VALUES (@os)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@os", os));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("OS успешно добавлено!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такое OS уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddColor(string color)
        {
            string sqlType = $"INSERT INTO Colors(ColorName) " +
                $"VALUES (@color)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@color", color));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("Color успешно добавлено!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такое Color уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void AddFirm(string firm)
        {
            string sqlType = $"INSERT INTO Firms(FirmName) " +
                $"VALUES (@firm)";

            try
            {
                SqlCommand commandTypes = new SqlCommand(sqlType, connection);
                commandTypes.Parameters.Add(new SqlParameter("@firm", firm));
                commandTypes.ExecuteNonQuery();

                MessageBox.Show("Firm успешно добавлено!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Такое Firm уже есть в БД или он не валидный!");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        // Получение параметров
        public List<ComboBoxItem> GetGoodTypes()
        {
            string sqlExpression = "SELECT * FROM GoodTypes";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            string name = reader["TypeName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetDisplayTypes()
        {
            string sqlExpression = "SELECT * FROM DisplayTypes";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["TypeName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetResolutions()
        {
            string sqlExpression = "SELECT * FROM Resolutions";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["ResolutionName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetHertz()
        {
            string sqlExpression = "SELECT * FROM Hertzs";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["HertzName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetRAM()
        {
            string sqlExpression = "SELECT * FROM RAMs";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["RAMName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetROM()
        {
            string sqlExpression = "SELECT * FROM ROMs";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["ROMName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetOS()
        {
            string sqlExpression = "SELECT * FROM OS";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["OSName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetColors()
        {
            string sqlExpression = "SELECT * FROM Colors";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["ColorName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetFirms()
        {
            string sqlExpression = "SELECT * FROM Firms";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["FirmName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        // ALL
        public List<ComboBoxItem> GetGoodTypesALL()
        {
            string sqlExpression = "SELECT * FROM GoodTypes";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все товары";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["TypeName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetDisplayTypesALL()
        {
            string sqlExpression = "SELECT * FROM DisplayTypes";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все типы";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["TypeName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetResolutionsALL()
        {
            string sqlExpression = "SELECT * FROM Resolutions";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все разрешения";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["ResolutionName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetHertzALL()
        {
            string sqlExpression = "SELECT * FROM Hertzs";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все герцовки";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["HertzName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetRAMALL()
        {
            string sqlExpression = "SELECT * FROM RAMs";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["RAMName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetROMALL()
        {
            string sqlExpression = "SELECT * FROM ROMs";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["ROMName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetOSALL()
        {
            string sqlExpression = "SELECT * FROM OS";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["OSName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetColorsALL()
        {
            string sqlExpression = "SELECT * FROM Colors";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["ColorName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }

        public List<ComboBoxItem> GetFirmsALL()
        {
            string sqlExpression = "SELECT * FROM Firms";
            List<ComboBoxItem> collection = new List<ComboBoxItem>();
            ComboBoxItem allItem = new ComboBoxItem();
            allItem.Content = "Все";
            collection.Add(allItem);

            try
            {
                SqlCommand sqlCommand = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string name = reader["FirmName"].ToString();
                            ComboBoxItem item = new ComboBoxItem();
                            item.Content = name;
                            collection.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return collection;
        }
    }
}
