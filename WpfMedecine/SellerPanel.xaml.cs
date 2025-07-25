﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using WpfMedecine.Data;

namespace WpfMedecine
{
    /// <summary>
    /// Interaction logic for SellerPanel.xaml
    /// </summary>
    public partial class SellerPanel : Window
    {
        public SellerPanel()
        {
            InitializeComponent();
        }


        int flag = 1;
        public bool ValidateInput()
        {

            return true;
        }


        /// /////////////////////////////////////////////////////////////////

        int flag1 = 1;
        public bool Validate1Input()
        {

            return true;
        }


        public void RefreshGridCustomer()
        {
            CustomerDB customerDB = new CustomerDB();
            ObservableCollection<Customer> customersList;
            customersList = customerDB.SelectCustomer();
            CustomersListGrid.ItemsSource = customersList;
        }

        public void RefreshGridMedecine()
        {
            MedecineDB medecineDB = new MedecineDB();
            ObservableCollection<Medicine> medecinesList;
            medecinesList = medecineDB.SelectMedecine();
            MedecinesListGrid.ItemsSource = medecinesList;
        }


        private void LoginWindow_Click(object sender, RoutedEventArgs e)
        {
            var backtoLogin = new MainWindow();
            backtoLogin.Show();
            this.Close();
        }

        private void CustomersManagement_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Visibility = Visibility.Collapsed;
            AddCustomersPanel.Visibility = Visibility.Collapsed;
            MedicationsManagment.Visibility = Visibility.Collapsed;
            CustomersManagementPanel.Visibility = Visibility.Visible;

            CustomerDB customerDB = new CustomerDB();
            CustomersListGrid.ItemsSource = customerDB.SelectCustomer();
        }

        private void MedicationsManagement_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Visibility = Visibility.Collapsed;
            CustomersManagementPanel.Visibility= Visibility.Collapsed;
            MedicationsManagment.Visibility = Visibility.Visible;

            MedecineDB medecineDB = new MedecineDB();
            MedecinesListGrid.ItemsSource = medecineDB.SelectMedecine();
        }

        private void SellingMedicine_Click(object sender, RoutedEventArgs e)
        {

        }

        private void medicineRequest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PersonalSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        ////////////////////////////////////////////////// صفحه مدیریت مشتری ها 

        private void txtSearchCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtSearchCustomer.Text.Trim();
            CustomerDB customerDB = new CustomerDB();
            ObservableCollection<Customer> searchResults;

            if (string.IsNullOrEmpty(filter)) // بررسی میکنیم که تکست باکس خالی نباشه
            {
                RefreshGridCustomer(); // اگر خالی بود، کل گرید رو دوباره پر میکنیم
            }
            else
            {
                searchResults = customerDB.Search(filter); // جستجو بر اساس فیلتر
                CustomersListGrid.ItemsSource = searchResults; // نمایش نتایج جستجو در گرید
            }
        }


        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Visibility = Visibility.Collapsed;
            CustomersManagementPanel.Visibility = Visibility.Collapsed;
            AddCustomersPanel.Visibility = Visibility.Visible;
        }


        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersListGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            var selectedCustomer = CustomersListGrid.SelectedItem as Customer;

            if (selectedCustomer == null)
            {
                MessageBox.Show("The selected item is not valid.");
                return;
            }
            // مقداردهی TextBoxها
            txtFirstNameEdit.Text = selectedCustomer.CustomerFirstName;
            txtLastNameEdit.Text = selectedCustomer.CustomerLastName;
            txtNationalCodeEdit.Text = selectedCustomer.CustomerId;
            txtPhoneNumberEdit.Text = selectedCustomer.CustomerPhoneNumber;
            // نمایش پنل ویرایش
            CustomersManagementPanel.Visibility = Visibility.Collapsed;
            EditCustomersPanel.Visibility = Visibility.Visible;

            RefreshGridCustomer();
        }


        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (CustomersListGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {
                // cast کردن SelectedItem به Customer
                Customer selectedCustomer = (Customer)CustomersListGrid.SelectedItem;

                // استفاده از ویژگی‌های Customer
                string id = selectedCustomer.CustomerId;
                string firstName = selectedCustomer.CustomerFirstName;
                string lastName = selectedCustomer.CustomerLastName;

                if (MessageBox.Show($"Are you sure you want to delete? {firstName + ' ' + lastName}", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        CustomerDB customerDB = new CustomerDB();
                        // دریافت ObservableCollection از ItemsSource گرید
                        ObservableCollection<Customer> customersList = (ObservableCollection<Customer>)CustomersListGrid.ItemsSource;

                        if (customerDB.DeleteCustomer(id))
                        {
                            MessageBox.Show($"{firstName + ' ' + lastName} has been deleted!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete customer.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting customer: {ex.Message}");
                    }
                }
                RefreshGridCustomer();
            }
        }


        private void RefreshCustomerList()
        {
            CustomersListGrid.ItemsSource = new CustomerDB().SelectCustomer();
            RefreshGridCustomer();
        }



        private void AddCustomerBack_Click(object sender, RoutedEventArgs e)
        {
            AddCustomersPanel.Visibility = Visibility.Collapsed;
            CustomersManagementPanel.Visibility = Visibility.Visible;
        }



        private void btnOkAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDB customerDB = new CustomerDB();
            Customer customer = new Customer();

            if (ValidateInput())
            {
                ///مقدار دهی پارمتر هایی که کاربر وارو می کند
                customer.CustomerFirstName = txtFirstName.Text.ToString();
                customer.CustomerLastName = txtLastName.Text.ToString();
                customer.CustomerId = txtNationalCode.Text.ToString();
                customer.CustomerPhoneNumber = txtPhoneNumber.Text.ToString();

                //بررسی می کند که ایا ثبت نام وفقیت امیز بوده یا نه
                if (customerDB.AddCustomer(customer))
                {
                    MessageBox.Show("✅ Adding was successful !", "Adding ", MessageBoxButton.OK);
                    RefreshGridCustomer();
                }
                else
                {
                    MessageBox.Show("Adding was unsuccessful !", "Adding ", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        private void btnOkEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer updatedCustomer = new Customer()
            {
                CustomerFirstName = txtFirstNameEdit.Text,
                CustomerLastName = txtLastNameEdit.Text,
                CustomerId = txtNationalCodeEdit.Text,
                CustomerPhoneNumber = txtPhoneNumberEdit.Text
            };

            CustomerDB customerDB = new CustomerDB();

            if (customerDB.UpdateCustomer(updatedCustomer))
            {
                MessageBox.Show("Information updated successfully.");
            }
            else
            {
                MessageBox.Show("The update operation failed.");
            }
            RefreshGridCustomer();
            EditCustomersPanel.Visibility = Visibility.Collapsed;
            CustomersManagementPanel.Visibility = Visibility.Visible;
        }

        private void EditCustomerBack_Click(object sender, RoutedEventArgs e)
        {
            EditCustomersPanel.Visibility = Visibility.Collapsed;
            CustomersManagementPanel.Visibility = Visibility.Visible;
        }



        private void txtSearchMedecine_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtSearchMedecine.Text.Trim();
            MedecineDB medecineDB = new MedecineDB();
            ObservableCollection<Medicine> searchResults;

            if (string.IsNullOrEmpty(filter)) // بررسی میکنیم که تکست باکس خالی نباشه
            {
                RefreshGridMedecine(); // اگر خالی بود، کل گرید رو دوباره پر میکنیم
            }
            else
            {
                searchResults = medecineDB.Search(filter); // جستجو بر اساس فیلتر
                MedecinesListGrid.ItemsSource = searchResults; // نمایش نتایج جستجو در گرید
            }
        }

        private void btnAddMedecine_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Visibility = Visibility.Collapsed;
            MedicationsManagment.Visibility = Visibility.Collapsed;
            AddMedecinesPanel.Visibility = Visibility.Visible;
        }

        private void btnEditMedecine_Click(object sender, RoutedEventArgs e)
        {
            if (MedecinesListGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }

            var selectedMedecine = MedecinesListGrid.SelectedItem as Medicine;

            if (selectedMedecine == null)
            {
                MessageBox.Show("The selected item is not valid.");
                return;
            }
            // مقداردهی TextBoxها
            txtIdEdit.Text = selectedMedecine.Id;
            txtNameEdit.Text = selectedMedecine.NameMedecines;
            dpDateBuyEdit.Text = selectedMedecine.DateBuy.ToString();
            txtPriceBuyEdit.Text = selectedMedecine.PriceBuy.ToString();
            txtPriceSellEdit.Text = selectedMedecine.PriceSell.ToString();
            txtQuantityEdit.Text = selectedMedecine.Quantity.ToString();
            txtUnitEdit.Text = selectedMedecine.Unit;

            // نمایش پنل ویرایش
            MainWindow.Visibility = Visibility.Collapsed;
            MedicationsManagment.Visibility = Visibility.Collapsed;
            EditMedecinePanel.Visibility = Visibility.Visible;

            RefreshGridMedecine();


        }

        private void btnDeleteMedecine_Click(object sender, RoutedEventArgs e)
        {
            if (MedecinesListGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {
                Medicine selectedMedecine = (Medicine)MedecinesListGrid.SelectedItem;

                // استفاده از ویژگی‌های Customer
                string Id = selectedMedecine.Id;
                string NameMedecines = selectedMedecine.NameMedecines;
                string DateBuy = selectedMedecine.DateBuy.ToString();
                string PriceBuy = selectedMedecine.PriceBuy.ToString();
                string PriceSell = selectedMedecine.PriceSell.ToString();
                string Quantity = selectedMedecine.Quantity.ToString();
                string Unit = selectedMedecine.Unit;

                if (MessageBox.Show($"Are you sure you want to delete? {NameMedecines + " with id " + Id}", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        MedecineDB medecineDB = new MedecineDB();
                        // دریافت ObservableCollection از ItemsSource گرید
                        ObservableCollection<Medicine> medecinesList = (ObservableCollection<Medicine>)MedecinesListGrid.ItemsSource;

                        if (medecineDB.DeleteMedecine(Id))
                        {
                            MessageBox.Show($"{NameMedecines + ' ' + Id} has been deleted!");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete medecine.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting medecine: {ex.Message}");
                    }
                }
                RefreshGridMedecine();
            }
        }

        private void AddMedecineBack_Click(object sender, RoutedEventArgs e)
        {
            AddMedecinesPanel.Visibility = Visibility.Collapsed;
            MedicationsManagment.Visibility = Visibility.Visible;
        }

        private void dpBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? selectedDate = dpBirthDate.SelectedDate;
            if (selectedDate.HasValue)
            {
                DateTime birthDate = selectedDate.Value;
                int age = DateTime.Now.Year - birthDate.Year;
                if (birthDate > DateTime.Now.AddYears(-age))
                {
                    age--;
                }
  
            }
        }

        private void btnOkAddMecedine_Click(object sender, RoutedEventArgs e)
        {
            MedecineDB medecineDB = new MedecineDB();
            Medicine medicine = new Medicine();

            if (ValidateInput())
            {
                ///مقدار دهی پارمتر هایی که کاربر وارد می کند
                medicine.Id = txtId.Text;
                medicine.NameMedecines = txtName.Text;
                medicine.DateBuy = dpBirthDate.SelectedDate.Value;
                medicine.PriceBuy = float.Parse(txtPriceBuy.Text);
                medicine.PriceSell = float.Parse(txtPriceSell.Text);
                medicine.Quantity = float.Parse(txtQuantity.Text);
                medicine.Unit = txtUnit.Text;

                //بررسی می کند که ایا ثبت نام وفقیت امیز بوده یا نه
                if (medecineDB.AddMedecine(medicine))
                {
                    MessageBox.Show("✅ Adding was successful !", "Adding ", MessageBoxButton.OK);
                    RefreshGridMedecine();
                }
            }
            else
            {
                MessageBox.Show("Adding was unsuccessful !", "Adding ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditMedecinBack_Click(object sender, RoutedEventArgs e)
        {
            EditMedecinePanel.Visibility = Visibility.Collapsed;
            MedicationsManagment.Visibility = Visibility.Visible; 
        }

        private void btnOkEditMedecine_Click(object sender, RoutedEventArgs e)
        {
            Medicine updatedMedecine = new Medicine()
            {
                Id = txtIdEdit.Text,
                NameMedecines = txtNameEdit.Text,
                DateBuy = DateTime.Parse(dpDateBuyEdit.Text),
                PriceBuy = float.Parse(txtPriceBuyEdit.Text), 
                PriceSell = float.Parse(txtPriceSellEdit.Text), 
                Quantity = float.Parse(txtQuantityEdit.Text),
                Unit = txtUnitEdit.Text
            };

            MedecineDB medecineDB = new MedecineDB();

            if (medecineDB.UpdateMedecine(updatedMedecine))
            {
                MessageBox.Show("Information updated successfully.");
            }
            else
            {
                MessageBox.Show("The update operation failed.");
            }
            RefreshGridMedecine();
            EditMedecinePanel.Visibility = Visibility.Collapsed;
            MedicationsManagment.Visibility = Visibility.Visible;
        }

        private void SellingMedicineBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }

}
