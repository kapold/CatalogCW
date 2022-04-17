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

        // TODO: DataBase use
        //public void AddGood(Good g)
        //{
        //    string sql = $"";
        //    try
        //    {
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public List<Good> GetGoods()
        //{
        //    string sql = $"";

        //}

        //public void Update(int id, Good g)
        //{
        //    string sql = $"";
        //    try
        //    {
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public void Delete(Good g)
        //{
        //    string sql = $"";

        //    try
        //    {
        //        SqlCommand command = new SqlCommand(sql, connection);
        //        command.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
}
}
