using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Collections.ObjectModel;

namespace WpfMedecine.Data
{
    internal class CustomerDB
    {
        //آدرس دیتابیس
        private string connectionString = "Data Source=.;Initial Catalog=HerbalProjectDB;Integrated Security=true";

        public ObservableCollection<Customer> SelectCustomer()


        {
            ObservableCollection<Customer> customers = new ObservableCollection<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CustomerId, CustomerFirstName, CustomerLastName,  PhoneNumber FROM Customer";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerId = reader["CustomerId"].ToString(),
                        CustomerFirstName = reader["CustomerFirstName"].ToString(),
                        CustomerLastName = reader["CustomerLastName"].ToString(),
                        CustomerPhoneNumber = reader["PhoneNumber"].ToString(),
                    });
                }
            }

            return customers;
        }

        public ObservableCollection<Customer> Search(string filter)
        {
            ObservableCollection<Customer> searchResults = new ObservableCollection<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CustomerId, CustomerFirstName, CustomerLastName,  PhoneNumber FROM Customer WHERE CustomerFirstName LIKE @Filter OR CustomerLastName LIKE @Filter"; // جستجو بر اساس اسم
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Filter", "%" + filter + "%"); // اضافه کردن پارامتر برای جلوگیری از SQL Injection
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    searchResults.Add(new Customer
                    {
                        CustomerId = reader["CustomerId"].ToString(),
                        CustomerFirstName = reader["CustomerFirstName"].ToString(),
                        CustomerLastName = reader["CustomerLastName"].ToString(),
                        CustomerPhoneNumber = reader["PhoneNumber"].ToString(),
                    });
                }
            }

            return searchResults;
        }

        public bool AddCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "INSERT INTO Customer(CustomerId, CustomerFirstName, CustomerLastName,  PhoneNumber) " +
               "VALUES (@customerId, @customerFirstName, @customerLastName, @phoneNumber)";

                SqlCommand command = new SqlCommand(query, connection);

                //مقدار دهی پارامتر ها 
                command.Parameters.AddWithValue("@customerId", customer.CustomerId);
                command.Parameters.AddWithValue("@customerFirstName", customer.CustomerFirstName);
                command.Parameters.AddWithValue("@customerLastName", customer.CustomerLastName);
                command.Parameters.AddWithValue("@phoneNumber", customer.CustomerPhoneNumber);

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

        public bool UpdateCustomer(Customer CustomerEdite)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "update Customer set " +
                    " CustomerId=@nationalCode , CustomerFirstName=@firstName, CustomerLastName=@lastName, PhoneNumber=@phoneNumber" +
                    " where CustomerId=@nationalCode";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@firstName", CustomerEdite.CustomerFirstName);
                command.Parameters.AddWithValue("@lastName", CustomerEdite.CustomerLastName);
                command.Parameters.AddWithValue("nationalCode", CustomerEdite.CustomerId);
                command.Parameters.AddWithValue("@phoneNumber", CustomerEdite.CustomerPhoneNumber);
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


        //deleteCustomer
        public bool DeleteCustomer(string NationalCode)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();
                string query = $"delete Customer where CustomerId='{NationalCode}'";
                SqlCommand command = new SqlCommand(query, connection);
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