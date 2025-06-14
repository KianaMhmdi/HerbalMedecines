using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using System.Data;
using System.Xml;




namespace WpfMedecine.Data
{
    internal class HerbalDB
    {

        //آدرس دیتابیس
        private string connectionString = "Data Source=.;Initial Catalog=HerbalProjectDB;Integrated Security=true";

        ///اضافه کردن فرد به دیتابیس
        public bool AddPerson(Person person)
        {
            string hashedPassword = PasswordHash.HashPassword(person.PasswordHash);
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "INSERT INTO Person_DB_Table(NationalCode, FirstName, LastName,PhoneNumber, Gender, PasswordHash, Address,BirthDate, Role, Email,FavoriteNumber) " +
               "VALUES (@nationalCode, @firstName, @lastName, @phoneNumber, @gender, @passwordHash, @address, @birthDate, @role, @email,@favoriteNumber)";


                SqlCommand command = new SqlCommand(query, connection);

                //مقدار دهی پارامتر ها

                command.Parameters.AddWithValue("@nationalCode", person.Id);
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@phoneNumber", person.PhoneNumber);
                command.Parameters.AddWithValue("@gender", person.Gender);
                command.Parameters.AddWithValue("@passwordHash", hashedPassword);

                command.Parameters.AddWithValue("@address", person.Address);
                command.Parameters.AddWithValue("@birthDate", person.BirthDate);
                command.Parameters.AddWithValue("@role", person.Role);
                command.Parameters.AddWithValue("@email", person.Email);
                command.Parameters.AddWithValue("@favoriteNumber", person.FavoriteNumber);

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

        public bool EditPassword(Person person)
        {
            string hashedNewPassword = PasswordHash.HashPassword(person.PasswordHash);
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "update Person_DB_Table set PasswordHash=@NewPassword where NationalCode=@Id and PhoneNumber=@phoneNumber and FavoriteNumber=@favoriteNumber";
                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@NewPassword", hashedNewPassword);
                command.Parameters.AddWithValue("@Id", person.Id);
                command.Parameters.AddWithValue("@phoneNumber", person.PhoneNumber);
                command.Parameters.AddWithValue("@favoriteNumber", person.FavoriteNumber);


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

        ///select owner,seles,customer
        public DataTable SelectPerson(string RoleType)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            DataTable data = new DataTable();
            try
            {
                connection.Open();
                string query = $"select NationalCode,FirstName, LastName,PhoneNumber,Address from Person_DB_Table where Role='{RoleType}'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(data);
                connection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return data;

        }

        //search seller and owner 
        public DataTable SearchPerson(string filter, string nameTable, string RoleType)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            DataTable data = new DataTable();
            try
            {
                connection.Open();
                string query = $" select NationalCode,FirstName,LastName,PhoneNumber from {nameTable} where Role='{RoleType}' and ( FirstName like '{filter}%' or LastName like '{filter}%') ";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(data);
                connection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return data;

        }
        //deletePerson
        public bool DeletePeson(string NationalCode, string NameTable)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();
                string query = $"delete {NameTable} where NationalCode='{NationalCode}'";
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
        //updatePerson
        public bool UpdatePerson(Person PersonEdite)
        {
            string hashedPassword = PasswordHash.HashPassword(PersonEdite.PasswordHash);
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "update Person_DB_Table set " +
                    "NationalCode=@nationalCode, FirstName=@firstName" +
                    ", LastName=@lastName,PhoneNumber=@phoneNumber, Gender=@gender, PasswordHash=@passwordHash" +
                    ", Address=@address,BirthDate=@birthDate, Role=@role, Email=@email,FavoriteNumber=@favoriteNumber " +
                    "where NationalCode=@nationalCode";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("nationalCode", PersonEdite.Id);
                command.Parameters.AddWithValue("@firstName", PersonEdite.FirstName);
                command.Parameters.AddWithValue("@lastName", PersonEdite.LastName);
                command.Parameters.AddWithValue("@phoneNumber", PersonEdite.PhoneNumber);
                command.Parameters.AddWithValue("@gender", PersonEdite.Gender);
                command.Parameters.AddWithValue("@passwordHash", hashedPassword);
                command.Parameters.AddWithValue("@address", PersonEdite.Address);
                command.Parameters.AddWithValue("@birthDate", PersonEdite.BirthDate);
                command.Parameters.AddWithValue("@role", PersonEdite.Role);
                command.Parameters.AddWithValue("@email", PersonEdite.Email);
                command.Parameters.AddWithValue("@favoriteNumber", PersonEdite.FavoriteNumber);
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

        //add Medicine
        public bool AddMedicine(Medicine medicine)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "insert into Medecines_DB_Table(Id,NameMedecines,DateBuy,PriceBuy,Quantity,Unit,PriceSell)" +
                    "values  (@id,@nameMedicine,@dateBuy,@priceBuy,@quantity,@unit,@priceSell)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", medicine.Id);
                command.Parameters.AddWithValue("@nameMedicine", medicine.NameMedecines);
                command.Parameters.AddWithValue("dateBuy", medicine.DateBuy);
                command.Parameters.AddWithValue("@priceBuy", medicine.PriceBuy);
                command.Parameters.AddWithValue("@quantity", medicine.Quantity);
                command.Parameters.AddWithValue("@unit", medicine.Unit);
                command.Parameters.AddWithValue("@priceSell", medicine.PriceSell);
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
        //select Medicine
        public DataTable SelectMedicine()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            DataTable data = new DataTable();
            try
            {
                connection.Open();
                string query = "select Id,NameMedecines,DateBuy,PriceBuy,Quantity,Unit,PriceSell from Medecines_DB_Table";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(data);
                connection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return data;

        }
        ///search Medicine
        public DataTable SearchMedicine(string filter)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            DataTable data = new DataTable();
            try
            {
                connection.Open();
                string query = $"select Id,NameMedecines,DateBuy,PriceBuy,Quantity,Unit,PriceSell from Medecines_DB_Table where  NameMedecines like '{filter}%' or Id like '{filter}%'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(data);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return data;
        }
        //delete Medicine
        public bool DeleteMedicine(string Id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();
                string query = $"delete Medecines_DB_Table where Id='{Id}'";
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
        //update Medicine
        public bool UpdateMedicine(Medicine medicine)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "update Medecines_DB_Table set" +
                    " Id=@id,NameMedecines=@nameMedecine,DateBuy=@dateBuy," +
                    "PriceBuy=@priceBuy,PriceSell=@priceSell,Quantity=@quantity,Unit=@unit where Id=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", medicine.Id);
                command.Parameters.AddWithValue("@nameMedecine", medicine.NameMedecines);
                command.Parameters.AddWithValue("dateBuy", medicine.DateBuy);
                command.Parameters.AddWithValue("@priceBuy", medicine.PriceBuy);
                command.Parameters.AddWithValue("@quantity", medicine.Quantity);
                command.Parameters.AddWithValue("@unit", medicine.Unit);
                command.Parameters.AddWithValue("@priceSell", medicine.PriceSell);
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



        //add Customer
        public bool AddCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "insert into  Customer(CustomerId,CustomerFirstName,CustomerLastName,PhoneNumber)" +
                    "values (@id,@fistName,@lastName,@phoneNumber)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", customer.CustomerId);
                command.Parameters.AddWithValue("@fistName", customer.CustomerFirstName);
                command.Parameters.AddWithValue("@lastName", customer.CustomerLastName);
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

        ///select customer
        public DataTable SelectCustomer()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            DataTable data = new DataTable();
            try
            {
                connection.Open();
                string query = "select CustomerId,CustomerFirstName,CustomerLastName,PhoneNumber from Customer";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(data);
                connection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return data;

        }
        ///Search Customer
        public DataTable SearchCustomer(string filter)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            DataTable data = new DataTable();
            try
            {
                connection.Open();
                string query =$"select CustomerId,CustomerFirstName,CustomerLastName,PhoneNumber from Customer where CustomerFirstName like '{filter}%' or CustomerLastName like '{filter}%'";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                adapter.Fill(data);
                connection.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return data;

        }
        //Update Customer
        public bool UpdateCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "update  Customer set CustomerId=@id,CustomerFirstName=@fistName,CustomerLastName=@lastName,PhoneNumber=@phoneNumber  where  CustomerId=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", customer.CustomerId);
                command.Parameters.AddWithValue("@fistName", customer.CustomerFirstName);
                command.Parameters.AddWithValue("@lastName", customer.CustomerLastName);
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

        //delete Customer
        public bool DeleteCustomer(string Id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {

                connection.Open();
                string query = $"delete Customer where CustomerId='{Id}'";
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

        //add Order
        public bool AddOrder(Order order)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = "insert into [Order](CustomerFullName,MedincineName,SellPrice,Quntity,TotalAmunt,orderTime)" +
                    "values (@customerFullName,@medincineName,@sellPrice,@quntity,@totalAmunt,Getdate())";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@customerFullName", order.CustomerFullName);
               command.Parameters.AddWithValue("@medincineName", order.MedincineName);
                command.Parameters.AddWithValue("@sellPrice", order.SellPrice);
                command.Parameters.AddWithValue("@quntity", order.Quntity);
                command.Parameters.AddWithValue("@totalAmunt", order.TotalAmunt);
               

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
        //update Sell Quantity
        public bool UpdateSellQuantity(float quntity, string Id)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                string query = $"update Medecines_DB_Table set Quantity=Quantity-{quntity} where Id='{Id}'";
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
