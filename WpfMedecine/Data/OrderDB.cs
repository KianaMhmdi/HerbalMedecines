using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMedecine.Data
{
    internal class OrderDB
    {
        private string connectionString = "Data Source=.;Initial Catalog=HerbalProjectDB;Integrated Security=true";


        public ObservableCollection<Orders> SelectOrder()
        {
            ObservableCollection<Orders> orders = new ObservableCollection<Orders>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, NameMedecines FROM Orders";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    orders.Add(new Orders
                    {
                        Id = reader["Id"].ToString(),
                        NameMedecines = reader["NameMedecines"].ToString(),

                    });
                }
            }

            return orders;
        }
    }
}
