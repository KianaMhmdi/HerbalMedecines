using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMedecine.Data
{
    internal class MedecineDB
    {
        //آدرس دیتابیس
        private string connectionString = "Data Source=.;Initial Catalog=HerbalProjectDB;Integrated Security=true";

        public ObservableCollection<Medicine> SelectMedecine()


        {
            ObservableCollection<Medicine> medecines = new ObservableCollection<Medicine>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, NameMedecines, DateBuy, PriceBuy, priceSell, Quantity, Unit FROM Medecines_DB_Table";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    medecines.Add(new Medicine
                    {
                        Id = reader["Id"].ToString(),
                        NameMedecines = reader["NameMedecines"].ToString(),
                        DateBuy = DateTime.Parse(reader["DateBuy"].ToString()),
                        PriceBuy = float.Parse(reader["PriceBuy"].ToString()),
                        PriceSell = float.Parse(reader["priceSell"].ToString()),
                        Quantity = float.Parse(reader["Quantity"].ToString()),
                        Unit = reader["Unit"].ToString()
                    });
                }
            }

            return medecines;
        }


        public ObservableCollection<Medicine> Search(string filter)
        {
            ObservableCollection<Medicine> searchResults = new ObservableCollection<Medicine>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, NameMedecines, DateBuy, PriceBuy, priceSell, Quantity, Unit FROM Medecines_DB_Table WHERE Id LIKE @Filter OR NameMedecines LIKE @Filter";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Filter", "%" + filter + "%"); // اضافه کردن پارامتر برای جلوگیری از SQL Injection
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    searchResults.Add(new Medicine
                    {
                        Id = reader["Id"].ToString(),
                        NameMedecines = reader["NameMedecines"].ToString(),
                        DateBuy = DateTime.Parse(reader["DateBuy"].ToString()),
                        PriceBuy = float.Parse(reader["PriceBuy"].ToString()),
                        PriceSell = float.Parse(reader["priceSell"].ToString()),
                        Quantity = float.Parse(reader["Quantity"].ToString()),
                        Unit = reader["Unit"].ToString()
                    });
                }
            }
            return searchResults;
        }

        public bool AddMedecine(Medicine medicine)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "INSERT INTO Medecines_DB_Table(Id, NameMedecines, DateBuy, PriceBuy, priceSell, Quantity, Unit) " +
               "VALUES (@id, @nameMedecines, @dateBuy, @priceBuy, @priceSell, @quantity, @unit)";

                SqlCommand command = new SqlCommand(query, connection);

                //مقدار دهی پارامتر ها 
                command.Parameters.AddWithValue("@id", medicine.Id);
                command.Parameters.AddWithValue("@nameMedecines", medicine.NameMedecines);
                command.Parameters.AddWithValue("@dateBuy", medicine.DateBuy);
                command.Parameters.AddWithValue("@priceBuy", medicine.PriceBuy);
                command.Parameters.AddWithValue("@priceSell", medicine.PriceSell);
                command.Parameters.AddWithValue("@quantity", medicine.Quantity);
                command.Parameters.AddWithValue("@unit", medicine.Unit);

                command.ExecuteNonQuery();
                return true;
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
