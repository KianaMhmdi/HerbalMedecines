using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows.Navigation;
using WpfMedecine.Data;
using System.Data;
using System.Text.RegularExpressions;
using System.Diagnostics;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;




namespace WpfMedecine
{
    /// <summary>
    /// Interaction logic for OwnerPanel.xaml
    /// </summary>
    public partial class OwnerPanel : Window
    {

        public OwnerPanel()
        {
            InitializeComponent();
        }




        private void LoginWindow_Click(object sender, RoutedEventArgs e)
        {
            var backtoLogin = new MainWindow();
            backtoLogin.Show();
            this.Close();
        }

        /// دکمه های مالک و امکانت آن
        private void btnOwnersList_Click(object sender, RoutedEventArgs e)
        {
            OwnersListPanel.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
            HerbalDB herbalDB = new HerbalDB();
            DataTable originalTable;
            string filterText = txtSearchOwners.Text.Trim();
            originalTable = herbalDB.SelectPerson("Owner");
            OwnersListGrid.ItemsSource = originalTable.DefaultView;
        }

        private void OwnersListBack_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            OwnersListPanel.Visibility = Visibility.Collapsed;
        }

        private void btnAddOwner_Click(object sender, RoutedEventArgs e)
        {
            var registeration = new Registeration();
            registeration.ShowDialog();
            RefreshGridOwner();
        }

        private void btnEditOwner_Click(object sender, RoutedEventArgs e)
        {
            if (OwnersListGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {
                var edite = new Registeration();
                HerbalDB herbal = new HerbalDB();
                edite.RegistertionOrEdite.Text = "Edite";
                edite.btnRegister.Content = "Save Change";
                var selectedRow = (DataRowView)OwnersListGrid.SelectedItem;
                edite.txtNationalCode.Text = selectedRow["NationalCode"].ToString();
                edite.txtFirstName.Text = selectedRow["FirstName"].ToString();
                edite.txtLastName.Text = selectedRow["LastName"].ToString();
                edite.txtPhoneNumber.Text = selectedRow["PhoneNumber"].ToString();
                edite.txtaddress.Text = selectedRow["Address"].ToString();
                edite.ShowDialog();

                RefreshGridOwner();
            }
        }

        private void btnDeleteOwner_Click(object sender, RoutedEventArgs e)
        {
            if (OwnersListGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {
                var selectedRow = (DataRowView)OwnersListGrid.SelectedItem;
                string id = selectedRow["NationalCode"].ToString();
                string firstName = selectedRow["FirstName"].ToString();
                string lastName = selectedRow["LastName"].ToString();
                if (MessageBox.Show($"Are you sure you want to delete?{firstName + ' ' + lastName}", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HerbalDB herbal = new HerbalDB();
                    herbal.DeletePeson(id, "Person_DB_Table");
                    MessageBox.Show($"{firstName + ' ' + lastName} has been delete!");
                    herbal.SelectPerson("Owner");
                    RefreshGridOwner();
                }
            }
        }

        //سرچ مالک ها
        private void txtSearchOwners_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtSearchOwners.Text.Trim();
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            if (txtSearchOwners.Text == "")
            {
                RefreshGridOwner();
            }
            else
            {
                originalTable = herbal.SearchPerson(filter, "Person_DB_Table", "Owner");
                OwnersListGrid.ItemsSource = originalTable.DefaultView;
            }
        }
        public void RefreshGridOwner()
        {
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            originalTable = herbal.SelectPerson("Owner");
            OwnersListGrid.ItemsSource = originalTable.DefaultView;
        }



        //صفحه مربوط به seller

        private void btnSellersList_Click(object sender, RoutedEventArgs e)
        {
            SellersListPanel.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
            HerbalDB herbalDB = new HerbalDB();
            DataTable originalTable;
            string filterText = txtSearchSeller.Text.Trim();
            originalTable = herbalDB.SelectPerson("Seller");
            SellersListGrid.ItemsSource = originalTable.DefaultView;
        }

        private void SellersListBack_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            SellersListPanel.Visibility = Visibility.Collapsed;
        }


        private void txtSearchSeller_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtSearchSeller.Text.Trim();
            HerbalDB herbalDB = new HerbalDB();
            DataTable originalTable;
            if (txtSearchSeller.Text == "")
            {
                RefreshGridSeller();
            }
            else
            {
                originalTable = herbalDB.SearchPerson(filter, "Person_DB_Table", "Seller");
                SellersListGrid.ItemsSource = originalTable.DefaultView;
            }
        }

        public void RefreshGridSeller()
        {
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            originalTable = herbal.SelectPerson("Seller");
            SellersListGrid.ItemsSource = originalTable.DefaultView;
        }


        private void btnAddSeller_Click(object sender, RoutedEventArgs e)
        {
            var registeration = new Registeration();
            registeration.ShowDialog();
            RefreshGridSeller();
        }

        private void btnEditSeller_Click(object sender, RoutedEventArgs e)
        {
            if (SellersListGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {
                var edite = new Registeration();
                HerbalDB herbal = new HerbalDB();
                edite.RegistertionOrEdite.Text = "Edite";
                edite.btnRegister.Content = "Save Change";
                var selectedRow = (DataRowView)SellersListGrid.SelectedItem;
                edite.txtNationalCode.Text = selectedRow["NationalCode"].ToString();
                edite.txtFirstName.Text = selectedRow["FirstName"].ToString();
                edite.txtLastName.Text = selectedRow["LastName"].ToString();
                edite.txtPhoneNumber.Text = selectedRow["PhoneNumber"].ToString();
                edite.txtaddress.Text = selectedRow["Address"].ToString();
                edite.ShowDialog();

                RefreshGridSeller();


            }

        }

        private void btnDeleteSeller_Click(object sender, RoutedEventArgs e)
        {
            if (SellersListGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {
                var selectedRow = (DataRowView)SellersListGrid.SelectedItem;
                string id = selectedRow["NationalCode"].ToString();
                string firstName = selectedRow["FirstName"].ToString();
                string lastName = selectedRow["LastName"].ToString();
                if (MessageBox.Show($"Are you sure you want to delete?{firstName + ' ' + lastName}", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HerbalDB herbal = new HerbalDB();
                    herbal.DeletePeson(id, "Person_DB_Table");
                    MessageBox.Show($"{firstName + ' ' + lastName} has been delete!");
                    herbal.SelectPerson("Seller");
                    RefreshGridSeller();
                }
            }
        }
        //صفحه مربوط به دارو ها

        private void btnInventoryMedecines_Click(object sender, RoutedEventArgs e)
        {
            InventoryMedecinesPanel.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
            HerbalDB herbalDB = new HerbalDB();
            DataTable originalTable;
            string filterText = txtSearchInventoryMedecinesPanel.Text.Trim();
            originalTable = herbalDB.SelectMedicine();
            InventoryMedecinesPanelGrid.ItemsSource = originalTable.DefaultView;


        }
        private void txtSearchInventoryMedecinesPanel_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtSearchInventoryMedecinesPanel.Text.Trim();
            HerbalDB herbalDB = new HerbalDB();
            DataTable originalTable;
            if (txtSearchInventoryMedecinesPanel.Text == "")
            {
                RefershInventoryMedicineGrid();
            }
            else
            {
                originalTable = herbalDB.SearchMedicine(filter);
                InventoryMedecinesPanelGrid.ItemsSource = originalTable.DefaultView;
            }
        }

        private void InventoryMedecinesPanelBack_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            InventoryMedecinesPanel.Visibility = Visibility.Collapsed;
        }
        private void btnDeleteInventoryMedecinesPanel_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryMedecinesPanelGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {
                var selectedRow = (DataRowView)InventoryMedecinesPanelGrid.SelectedItem;
                string id = selectedRow["Id"].ToString();
                string NameMedicinee = selectedRow["NameMedecines"].ToString();

                if (MessageBox.Show($"Are you sure you want to delete? Id: {id} and Medicine Name: {NameMedicinee}", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HerbalDB herbal = new HerbalDB();
                    herbal.DeleteMedicine(id);
                    MessageBox.Show($" Id: {id} and Medicine Name: {NameMedicinee} has been delete!");
                    herbal.SelectMedicine();
                    RefershInventoryMedicineGrid();
                }
            }

        }

        private void btnEditInventoryMedecinesPanel_Click(object sender, RoutedEventArgs e)
        {
            if (InventoryMedecinesPanelGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {

                HerbalDB herbal = new HerbalDB();
                Medicine medicine = new Medicine();
                InventoryMedecinesPanel.Visibility = Visibility.Collapsed;
                BuyMedecinesPanel.Visibility = Visibility.Visible;

                BuyOrEditMedicine.Content = "Edit Medicine";

                var selectedRow = (DataRowView)InventoryMedecinesPanelGrid.SelectedItem;

                txtMedicineID.Text = selectedRow["Id"].ToString();
                txtMedicineName.Text = selectedRow["NameMedecines"].ToString();
                dpMedicineBuyDate.Text = selectedRow["DateBuy"].ToString();
                txtMedicineBuyPrice.Text = selectedRow["PriceBuy"].ToString();
                txtMedicineQuantity.Text = selectedRow["Quantity"].ToString();
                txtMedicineSellPrice.Text = selectedRow["PriceSell"].ToString();


            }
        }

        // بروزرسانی گرید موجوی دارو
        public void RefershInventoryMedicineGrid()
        {
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            originalTable = herbal.SelectMedicine();
            InventoryMedecinesPanelGrid.ItemsSource = originalTable.DefaultView;
        }
        private void btnMedecinesBuy_Click(object sender, RoutedEventArgs e)
        {
            BuyMedecinesPanel.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;
            dpMedicineBuyDate.SelectedDate = DateTime.Today;
        }

        private void BuyMedecinesPanelBack_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            BuyMedecinesPanel.Visibility = Visibility.Collapsed;
            RefreshBuyMedicine();
            clearErrorsBuyMedicine();


        }

        private void btnAddMedicine_Click(object sender, RoutedEventArgs e)
        {

            HerbalDB herbalDB = new HerbalDB();
            Medicine medicine = new Medicine();

            medicine.Id = txtMedicineID.Text.ToString();
            medicine.NameMedecines = txtMedicineName.Text.ToString();
            medicine.DateBuy = dpMedicineBuyDate.SelectedDate.Value;
            medicine.PriceBuy = Convert.ToSingle(txtMedicineBuyPrice.Text.ToString());
            medicine.Quantity = Convert.ToSingle(txtMedicineQuantity.Text.ToString());
            medicine.Unit = ((ComboBoxItem)comMedicineUnit.SelectedItem).Content.ToString();
            medicine.PriceSell = Convert.ToSingle(txtMedicineSellPrice.Text.ToString());


            if (ValidateInputMedicineBuy() && BuyOrEditMedicine.Content.ToString() == "Buy Medicines")
            {


                if (herbalDB.AddMedicine(medicine))
                {
                    MessageBox.Show("✅ Add medicine was successful !", "Add ", MessageBoxButton.OK);

                    clearErrorsBuyMedicine();
                    RefreshBuyMedicine();
                }
                else
                {
                    MessageBox.Show("Add medicine was unsuccessful !", "Add ", MessageBoxButton.OK, MessageBoxImage.Error);
                    clearErrorsBuyMedicine();
                    RefreshBuyMedicine();
                }
            }
            if (ValidateInputMedicineBuy() && BuyOrEditMedicine.Content.ToString() == "Edit Medicine")
            {


                if (herbalDB.UpdateMedicine(medicine))
                {
                    MessageBox.Show("✅ Eidit medicine was successful !", "Edite ", MessageBoxButton.OK);

                    clearErrorsBuyMedicine();
                    RefreshBuyMedicine();
                    InventoryMedecinesPanel.Visibility = Visibility.Visible;
                    BuyMedecinesPanel.Visibility = Visibility.Collapsed;
                    RefershInventoryMedicineGrid();

                }
                else
                {
                    MessageBox.Show("Edit medicine was unsuccessful !", "Edite ", MessageBoxButton.OK, MessageBoxImage.Error);
                    clearErrorsBuyMedicine();
                    RefreshBuyMedicine();
                    InventoryMedecinesPanel.Visibility = Visibility.Visible;
                    BuyMedecinesPanel.Visibility = Visibility.Collapsed;
                }

            }


        }
        //پاک کردن ارور ها با کلیک روی فیلد
        private void txtMedicineID_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsBuyMedicine();
        }
        private void txtMedicineName_GotFocus(object sender, RoutedEventArgs e)
        {

            clearErrorsBuyMedicine();
        }

        private void dpMedicineBuyDate_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsBuyMedicine();

        }

        private void txtMedicineBuyPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsBuyMedicine();

        }

        private void txtMedicineQuantity_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsBuyMedicine();

        }
        private void txtMedicineSellPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsBuyMedicine();

        }

        ///پاک کردن اخطار های فرم خرید دارو
        public void clearErrorsBuyMedicine()
        {

            MedicineIDError.Visibility = Visibility.Collapsed;
            MedicineNameError.Visibility = Visibility.Collapsed;
            MedicineBuyDateError.Visibility = Visibility.Collapsed;
            MedicineBuyPriceError.Visibility = Visibility.Collapsed;
            MedicineBuyPriceErrorDigit.Visibility = Visibility.Collapsed;
            MedicineQuantityError.Visibility = Visibility.Collapsed;
            MedicineQuantityErrorDigit.Visibility = Visibility.Collapsed;
            MedicineSellPriceError.Visibility = Visibility.Collapsed;
            MedicineSellPriceErrorDigit.Visibility = Visibility.Collapsed;

        }
        //بروزرسانی فرم خرید دارو 
        public void RefreshBuyMedicine()
        {
            txtMedicineID.Text = "";
            txtMedicineName.Text = "";
            dpMedicineBuyDate.SelectedDate = DateTime.Today;
            txtMedicineBuyPrice.Text = "";
            txtMedicineQuantity.Text = "";
            txtMedicineSellPrice.Text = "";
        }
        private void txtInventoryMedecinesPanel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }





        private void btnSearchMedecines_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRequestedMedecines_Click(object sender, RoutedEventArgs e)
        {

        }



        private void btnDeleteMedecines_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnPersonalSettings_Click(object sender, RoutedEventArgs e)
        {

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }


        //اعتبار سنجی فیلد های ثبت  خرید دارو
        public bool ValidateInputMedicineBuy()
        {
            if (txtMedicineID.Text == "")
            {
                MedicineIDError.Visibility = Visibility.Visible;
                return false;
            }
            if (txtMedicineName.Text == "")
            {
                MedicineNameError.Visibility = Visibility.Visible;
                return false;
            }
            if (dpMedicineBuyDate.SelectedDate == null)
            {
                MedicineBuyDateError.Visibility = Visibility.Visible;
                return false;
            }
            if (txtMedicineBuyPrice.Text == "")
            {
                MedicineBuyPriceError.Visibility = Visibility.Visible;
                return false;

            }
            if (!IsNumberInput(txtMedicineBuyPrice.Text))
            {
                MedicineBuyPriceErrorDigit.Visibility = Visibility.Visible;
                return false;

            }
            if (txtMedicineQuantity.Text == "")
            {
                MedicineQuantityError.Visibility = Visibility.Visible;
                return false;
            }

            if (!IsNumberInput(txtMedicineQuantity.Text))
            {
                MedicineQuantityErrorDigit.Visibility = Visibility.Visible;
                return false;

            }
            if (txtMedicineSellPrice.Text == "")
            {
                MedicineSellPriceError.Visibility = Visibility.Visible;
                return false;
            }
            if (!IsNumberInput(txtMedicineSellPrice.Text))
            {
                MedicineSellPriceErrorDigit.Visibility = Visibility.Visible;
                return false;
            }

            return true;
        }

        ///بررسی رقم بودن فید ها  در خرید دارو  
        public bool IsNumberInput(string input)
        {
            if (double.TryParse(input, out _))
            {
                return true;
            }
            return false;
        }



        public bool ValidateInputAddCustomer()
        {

            if (txtCustomerID.Text == "")
            {
                CustomerIDError.Visibility = Visibility.Visible;
                return false;
            }
            if (!Regex.IsMatch(txtCustomerID.Text, @"^\d+$"))
            {
                CustomerIDErrorDigit.Visibility = Visibility.Visible;
                return false;
            }

            if (txtCustomerID.Text.Length != 10 && txtCustomerID.Text.Length > 0)
            {
                CustomerIDLengthError.Visibility = Visibility.Visible;
                return false;
            }
            if (txtCustomerFirstName.Text == "")
            {
                CustomerFirstNameError.Visibility = Visibility.Visible;
                return false;
            }
            if (txtCustomerLastName.Text == "")
            {
                CustomerLastNameError.Visibility = Visibility.Visible;
                return false;
            }
            if (txtCustomrPhoneNumber.Text == "")
            {
                CustomerPhoneNumberError.Visibility = Visibility.Visible;
                return false;
            }
            if (!Regex.IsMatch(txtCustomrPhoneNumber.Text.Trim(), @"^\d+$") && txtCustomrPhoneNumber.Text.Length > 0)
            {
                CustomerPhoneNumberErrorErrorDigit.Visibility = Visibility.Visible;

                return false;
            }

            if (txtCustomrPhoneNumber.Text.Trim().Length != 11 && txtCustomrPhoneNumber.Text.Length > 0)
            {
                LengthCustomerPhoneNumberError.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }
        //پاک کردن ارور های صفحه اد کردن مشتری

        public void clearErrorsAddCustomer()
        {
            CustomerIDError.Visibility = Visibility.Collapsed;
            CustomerIDErrorDigit.Visibility = Visibility.Collapsed;
            CustomerIDLengthError.Visibility = Visibility.Collapsed;
            CustomerFirstNameError.Visibility = Visibility.Collapsed;
            CustomerLastNameError.Visibility = Visibility.Collapsed;
            CustomerPhoneNumberError.Visibility = Visibility.Collapsed;
            CustomerPhoneNumberErrorErrorDigit.Visibility = Visibility.Collapsed;
            LengthCustomerPhoneNumberError.Visibility = Visibility.Collapsed;
        }
        private void txtCustomerID_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsAddCustomer();

        }
        private void txtCustomerFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsAddCustomer();
        }

        private void txtCustomerLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsAddCustomer();
        }
        private void txtCustomrPhoneNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            clearErrorsAddCustomer();
        }




        private void txtMedicineID_TextChanged(object sender, TextChangedEventArgs e)
        {


        }




        private void MedicineSellListGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            CustomerPanel.Visibility = Visibility.Visible;
            HerbalDB herbalDB = new HerbalDB();
            DataTable originalTable;
            string filterText = txtSearchCustomer.Text.Trim();
            originalTable = herbalDB.SelectCustomer();
            CustomerGrid.ItemsSource = originalTable.DefaultView;



        }

        private void CustomerBack_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            CustomerPanel.Visibility = Visibility.Collapsed;

        }

        private void txtSearchCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {

            string filter = txtSearchCustomer.Text.Trim();
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            if (txtSearchCustomer.Text == "")
            {
                RefreshGridCustomer();
            }
            else
            {
                originalTable = herbal.SearchCustomer(filter);
                CustomerGrid.ItemsSource = originalTable.DefaultView;
            }
        }

        public void RefreshGridCustomer()
        {
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            originalTable = herbal.SelectCustomer();
            CustomerGrid.ItemsSource = originalTable.DefaultView;
        }
        private void btnAddCustomerPanel_Click(object sender, RoutedEventArgs e)
        {
            CustomerPanel.Visibility = Visibility.Collapsed;
            AddCustomer.Visibility = Visibility.Visible;
        }


        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            HerbalDB herbalDB = new HerbalDB();
            Customer customer = new Customer();
            customer.CustomerId = txtCustomerID.Text.ToString();
            customer.CustomerFirstName = txtCustomerFirstName.Text.ToString();
            customer.CustomerLastName = txtCustomerLastName.Text.ToString();
            customer.CustomerPhoneNumber = txtCustomrPhoneNumber.Text.ToString();
            if (ValidateInputAddCustomer() && SellOrEditCustomer.Content.ToString() == "Add Customer")
            {

                if (herbalDB.AddCustomer(customer))
                {

                    MessageBox.Show("✅ Add customer was successful !", "Add ", MessageBoxButton.OK);
                    clearErrorsAddCustomer();
                    RefreshGridCustomer();
                    ClearCustomerFields();
                    AddCustomer.Visibility = Visibility.Collapsed;
                    CustomerPanel.Visibility = Visibility.Visible;
                }
                else
                {

                    MessageBox.Show("Add medicine was unsuccessful !", "Add ", MessageBoxButton.OK, MessageBoxImage.Error);
                    clearErrorsAddCustomer();
                    RefreshGridCustomer();
                    ClearCustomerFields();

                    AddCustomer.Visibility = Visibility.Collapsed;
                    CustomerPanel.Visibility = Visibility.Visible;
                }

            }
            if (ValidateInputAddCustomer() && SellOrEditCustomer.Content.ToString() == "Edit Customer")
            {
                if (herbalDB.UpdateCustomer(customer))
                {


                    MessageBox.Show("✅ Edit customer was successful !", "Edit ", MessageBoxButton.OK);
                    clearErrorsAddCustomer();
                    RefreshGridCustomer();
                    ClearCustomerFields();
                    SellOrEditCustomer.Content = "Add Customer";
                    btnAddCustomer.Content = "Add";
                    CustomerGrid.SelectedItem = null;
                    AddCustomer.Visibility = Visibility.Collapsed;
                    CustomerPanel.Visibility = Visibility.Visible;
                }
                else
                {

                    MessageBox.Show("Edit medicine was unsuccessful !", "Edit ", MessageBoxButton.OK, MessageBoxImage.Error);
                    clearErrorsAddCustomer();
                    RefreshGridCustomer();
                    ClearCustomerFields();


                    SellOrEditCustomer.Content = "Add Customer";
                    btnAddCustomer.Content = "Add";
                    CustomerGrid.SelectedItem = null;
                    AddCustomer.Visibility = Visibility.Collapsed;
                    CustomerPanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnEditCustomerPanel_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {

                HerbalDB herbal = new HerbalDB();
                Customer customer = new Customer();
                CustomerPanel.Visibility = Visibility.Collapsed;
                AddCustomer.Visibility = Visibility.Visible;

                SellOrEditCustomer.Content = "Edit Customer";
                btnAddCustomer.Content = "Edit";

                var selectedRow = (DataRowView)CustomerGrid.SelectedItem;
                txtCustomerID.Text = selectedRow["CustomerId"].ToString();
                txtCustomerFirstName.Text = selectedRow["CustomerFirstName"].ToString();
                txtCustomerLastName.Text = selectedRow["CustomerLastName"].ToString();
                txtCustomrPhoneNumber.Text = selectedRow["PhoneNumber"].ToString();
            }
        }

        private void btnDeleteCustomerPanel_Click(object sender, RoutedEventArgs e)
        {

            if (CustomerGrid.SelectedItem == null)
            {
                MessageBox.Show("Please select an item.");
                return;
            }
            else
            {
                var selectedRow = (DataRowView)CustomerGrid.SelectedItem;
                string id = selectedRow["CustomerId"].ToString();
                string firstName = selectedRow["CustomerFirstName"].ToString();
                string lastName = selectedRow["CustomerLastName"].ToString();
                if (MessageBox.Show($"Are you sure you want to delete?{firstName + ' ' + lastName}", "Delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    HerbalDB herbal = new HerbalDB();
                    herbal.DeleteCustomer(id);
                    MessageBox.Show($"{firstName + ' ' + lastName} has been delete!");
                    herbal.SelectPerson("Seller");
                    RefreshGridCustomer();
                }
            }
        }

        private void AddCustomerBack_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer.Visibility = Visibility.Collapsed;
            CustomerPanel.Visibility = Visibility.Visible;
            CustomerGrid.SelectedItem = null;
            clearErrorsAddCustomer();
            RefreshBuyMedicine();
            ClearCustomerFields();
        }
        public void ClearCustomerFields()
        {
            txtCustomerID.Text = "";
            txtCustomerFirstName.Text = "";
            txtCustomerLastName.Text = "";
            txtCustomrPhoneNumber.Text = "";
        }

        ///صفحه مربوط به فروش دارو

        private void btnMedecinesSell_Click(object sender, RoutedEventArgs e)
        {

            Main.Visibility = Visibility.Collapsed;
            AddOrder.Visibility = Visibility.Visible;
            HerbalDB herbalDB = new HerbalDB();
            DataTable originalTable = new DataTable();
            originalTable = herbalDB.SelectMedicine();
            MedicineSellListGrid.ItemsSource = originalTable.DefaultView;
            originalTable = herbalDB.SelectCustomer();
            CustomerSellListGrid.ItemsSource = originalTable.DefaultView;
            MedicineSellListGrid.IsEnabled = false;


        }
        private void AddOrderBack_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            AddOrder.Visibility = Visibility.Collapsed;
            BorderFactor.Visibility = Visibility.Collapsed;
            Factor.Items.Clear();
            TotalAmountLabel.Content = 0.0;
            MedicineSellListGrid.IsEnabled = false;

        }

        private void CustomerSellListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerSellListGrid.SelectedItem != null)
            {

                BorderFactor.Visibility = Visibility.Visible;
                var selectedRow = (DataRowView)CustomerSellListGrid.SelectedItem;
                NameCustomerSell.Content = selectedRow["CustomerFirstName"].ToString() + ' ' + selectedRow["CustomerLastName"].ToString();
                MedicineSellListGrid.IsEnabled = true;


            }
            else
            {
                // اگر مشتری انتخاب نشد، داروها را غیرفعال کن
                MedicineSellListGrid.IsEnabled = false;
            }

        }


        //رویداد انتخاب دارو و نمایش آن روی فاکتور

        private void MedicineSellListGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medicine medicine = new Medicine();
            HerbalDB herbal = new HerbalDB();

            if (MedicineSellListGrid.SelectedItem != null)
            {
                if (CustomerSellListGrid.SelectedItem != null)
                {

                    Order order = new Order();
                    var selectedRow = (DataRowView)MedicineSellListGrid.SelectedItem;
                    medicine.Id = selectedRow["Id"].ToString();
                    medicine.NameMedecines = selectedRow["NameMedecines"].ToString();
                    medicine.Quantity = 1;
                    medicine.Unit = selectedRow["Unit"].ToString();
                    float price;

                    if (float.TryParse(selectedRow["PriceSell"].ToString(), out price))
                    {
                        medicine.PriceSell = price;

                    }



                    MedicineSellQuantity quantity = new MedicineSellQuantity();
                    quantity.ShowDialog();


                    if (IsMedicineInFactorGrid(medicine.NameMedecines))
                    {
                        MessageBox.Show("This item is already selected.");

                    }

                    else
                    {
                        medicine.Quantity = quantity.Quantiry;
                        Customer customer = new Customer();

                        Order order1 = new Order();
                        var selectedRowCustomer = (DataRowView)CustomerSellListGrid.SelectedItem;
                        order.CustomerFullName = selectedRowCustomer["CustomerFirstName"].ToString() + ' ' + selectedRowCustomer["CustomerLastName"].ToString();
                        order.MedincineName = selectedRow["NameMedecines"].ToString();
                        order.SellPrice = price;

                        order.TotalAmunt = medicine.Price;
                        order.orderTime = DateTime.Today;
                        float q = herbal.SelectInventoryQuntity(medicine.Id);

                        if (quantity.Quantiry <= q)
                        {
                            order.Quntity = quantity.Quantiry;
                            medicine.Quantity = quantity.Quantiry;


                            if (quantity.flag == 1)
                            {

                                Factor.Items.Add(medicine);
                            }

                        }
                        else
                        {
                            MessageBox.Show("بیشتر از موجودی انبار است ");
                        }

                    }


                    UpdateTotalPrice(); // محاسبه جمع کل پس از اضافه‌شدن دارو

                }

                else
                {
                    MedicineSellListGrid.SelectedItem = null; // لغو انتخاب دارو
                    return;
                }

            }
        }

        private void SubmitOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }
        //بررسی می کند ایا ایتم انختاب شده برای فروش در فاکتور است یا نه
        public bool IsMedicineInFactorGrid(string MedicineName)
        {
            foreach (Medicine item in Factor.Items)
            {
                if (item.NameMedecines == MedicineName)
                {
                    return true;
                }

            }
            return false;
        }



        private void UpdateTotalPrice()
        {
            float total = 0;
            foreach (Medicine item in Factor.Items)
            {
                total += item.PriceSell * item.Quantity; // جمع کل = تعداد × قیمت هر واحد
            }
            TotalAmountLabel.Content = total;
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            HerbalDB herbal = new HerbalDB();
            int count = 0;
            Order order = new Order();

            foreach (Medicine item in Factor.Items)
            {



                order.CustomerFullName = NameCustomerSell.Content.ToString();
                order.MedincineName = item.NameMedecines;
                float quntityOrder;
                if (float.TryParse(item.Quantity.ToString(), out quntityOrder))
                {
                    order.Quntity = quntityOrder;

                }
                float price;
                if (float.TryParse(item.PriceSell.ToString(), out price))
                {
                    order.SellPrice = price;

                }

                float total;
                if (float.TryParse(item.Price.ToString(), out total))
                {
                    order.TotalAmunt = total;

                }



                if (herbal.AddOrder(order))
                {
                    if (herbal.UpdateSellQuantity(order.Quntity, order.MedincineName))
                    {
                        DataTable originalTable = new DataTable();
                        originalTable = herbal.SelectMedicine();
                        MedicineSellListGrid.ItemsSource = originalTable.DefaultView;


                    }
                    count++;
                }

            }


            if (count == Factor.Items.Count)
            {
                MessageBox.Show("ثبت سفارش با موفقیت انجام شد!");
            }
            else
            {
                MessageBox.Show("ثبت سفارش با موفقیت انجام نشد!");
            }
            BorderFactor.Visibility = Visibility.Collapsed;
            Factor.Items.Clear();
            TotalAmountLabel.Content = 0.0;
        }

        private void btnDeleteFactor_Click(object sender, RoutedEventArgs e)
        {
            Factor.Items.Clear();
            TotalAmountLabel.Content = 0.0;
            NameCustomerSell.Content = ' ';
            CustomerSellListGrid.SelectedItem = null;
            MedicineSellListGrid.SelectedItem = null;
            MedicineSellListGrid.IsEnabled = false; // غیرفعال کردن DataGrid داروها
        }


        ///صفحه گزارش فروش 

        private void btnSalesReports_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            SelllesReport.Visibility = Visibility.Visible;
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable = new DataTable();
            originalTable = herbal.SelectOrder();
            SelllesReportGrid.ItemsSource = originalTable.DefaultView;
            dpendDate.SelectedDate = DateTime.Today;
        }
        public int SearchBaze = 0;
        private void SelllesReportBack_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            SelllesReport.Visibility = Visibility.Collapsed;
            SearchBaze = 0;
            RefreshGridOrder();
            dpStartDate.SelectedDate = null;
            endAndStartDateError.Visibility = Visibility.Collapsed;

        }

        private void txtSearchSelllesReport_TextChanged(object sender, TextChangedEventArgs e)
        {

            string filter = txtSearchSelllesReport.Text.Trim();
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            if (SearchBaze == 0)
            {


                if (txtSearchSelllesReport.Text == "")
                {
                    RefreshGridOrder();
                }
                else
                {
                    originalTable = herbal.SearchOrder(filter);
                    SelllesReportGrid.ItemsSource = originalTable.DefaultView;
                }
            }

            if (SearchBaze == 1)
            {


                if (txtSearchSelllesReport.Text == "")
                {
                    RefreshGridOrderBaze();
                }
                else
                {
                    originalTable = herbal.SearchGetOrderByDateRange(dpStartDate.SelectedDate.Value, dpendDate.SelectedDate.Value, filter);
                    SelllesReportGrid.ItemsSource = originalTable.DefaultView;
                }
            }

        }
        public void RefreshGridOrder()
        {
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            originalTable = herbal.SelectOrder();
            SelllesReportGrid.ItemsSource = originalTable.DefaultView;
        }
        public void RefreshGridOrderBaze()
        {
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            originalTable = herbal.GetOrderByDateRange(dpStartDate.SelectedDate.Value, dpendDate.SelectedDate.Value);
            SelllesReportGrid.ItemsSource = originalTable.DefaultView;
        }
        //تابعی برای اعتبار سنجی تاریخ 
        public bool validateDates()
        {
            DateTime? startDate = dpStartDate.SelectedDate;
            DateTime? endDate = dpendDate.SelectedDate;


            if (startDate.HasValue && endDate.HasValue)
            {
                if (endDate < startDate)
                {
                    endAndStartDateError.Text = " تاریخ نامعتبر است";
                    endAndStartDateError.Visibility = Visibility.Visible;
                    return false;
                }


            }
            if (startDate.HasValue && !endDate.HasValue)
            {
                endAndStartDateError.Text = " تاریخ پایان تعیین نشده است";
                endAndStartDateError.Visibility = Visibility.Visible;
                return false;
            }
            if (!startDate.HasValue && !endDate.HasValue)
            {
                endAndStartDateError.Text = " تاریخ نامعتبر است";
                endAndStartDateError.Visibility = Visibility.Visible;
                return false;
            }
            endAndStartDateError.Visibility = Visibility.Collapsed;
            return true;
        }

        private void dpStartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {


            validateDates();
        }

        private void dpendDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            validateDates();
        }

        private void btnMohasebeDateReport_Click(object sender, RoutedEventArgs e)
        {

            if (validateDates())
            {
                SearchBaze = 1;
                endAndStartDateError.Visibility = Visibility.Collapsed;
                DateTime startDate = dpStartDate.SelectedDate.Value;
                DateTime endDate = dpendDate.SelectedDate.Value;
                HerbalDB herbal = new HerbalDB();
                DataTable originalTable;
                originalTable = herbal.GetOrderByDateRange(startDate, endDate);
                SelllesReportGrid.ItemsSource = originalTable.DefaultView;
            }
        }


        ///گزار فروش در یک روز خاص
        private void btnDailySalesReports_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Collapsed;
            DailySelllesReport.Visibility = Visibility.Visible;

            dpDate.SelectedDate = DateTime.Today;
            RefreshGridOrderDaily();


        }
        public void RefreshGridOrderDaily()
        {
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            originalTable = herbal.DailySalesReport(dpDate.SelectedDate.Value);
            DailySelllesReportGrid.ItemsSource = originalTable.DefaultView;
        }
        private void DailySelllesReportBack_Click(object sender, RoutedEventArgs e)
        {
            Main.Visibility = Visibility.Visible;
            DailySelllesReport.Visibility = Visibility.Collapsed;
            RefreshGridOrderDaily();
            DateError.Visibility = Visibility.Collapsed;
        }

        private void txtDailySearchSelllesReport_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = txtDailySearchSelllesReport.Text.Trim();
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;

            if (txtDailySearchSelllesReport.Text == "")
            {
                RefreshGridOrderDaily();
            }
            else
            {
                originalTable = herbal.SearchDailySalesReport(dpDate.SelectedDate.Value, filter);
                DailySelllesReportGrid.ItemsSource = originalTable.DefaultView;


            }
        }

        private void btnDateReport_Click(object sender, RoutedEventArgs e)
        {
            if (validateDatesDaily())
            {
                RefreshGridOrderDaily();
            }

        }
        public bool validateDatesDaily()
        {
            if (dpDate.SelectedDate.HasValue)
            {
                DateError.Visibility = Visibility.Collapsed;
                return true;

            }
            else
            {
                DateError.Text = "تاریخ نامعتبر است";
                DateError.Visibility = Visibility.Visible;
                return false;
            }
        }


        private void dpDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            validateDatesDaily();
        }
        //صفحه واردن تاریخ برای رسم نمودار 


        private void btnProductSalesReport_Click(object sender, RoutedEventArgs e)
        {
            DateEntryPageForCahrt.Visibility = Visibility.Visible;
            Main.Visibility = Visibility.Collapsed;


        }
        public bool validateDatesProductSellReport()
        {

            DateTime? startDate = dpStartDateproductSelesReport.SelectedDate;
            DateTime? endDate = dpendDateProductSelesReport.SelectedDate;


            if (startDate.HasValue && endDate.HasValue)
            {
                if (endDate < startDate)
                {
                    endAndStartProductSelesReportDateError.Text = " تاریخ نامعتبر است";
                    endAndStartProductSelesReportDateError.Visibility = Visibility.Visible;
                    return false;
                }


            }
            if (startDate.HasValue && !endDate.HasValue)
            {
                endAndStartProductSelesReportDateError.Text = " تاریخ پایان تعیین نشده است";
                endAndStartProductSelesReportDateError.Visibility = Visibility.Visible;
                return false;
            }
            if (!startDate.HasValue && endDate.HasValue)
            {
                endAndStartProductSelesReportDateError.Text = " تاریخ شروع تعییم نشده است";
                endAndStartProductSelesReportDateError.Visibility = Visibility.Visible;
                return false;
            }
            if (!startDate.HasValue && !endDate.HasValue)
            {
                endAndStartProductSelesReportDateError.Text = "تاریخ نامعتبر است";
                endAndStartProductSelesReportDateError.Visibility = Visibility.Visible;
                return false;
            }
            endAndStartProductSelesReportDateError.Visibility = Visibility.Collapsed;
            return true;
        }

        private void dpStartDateproductSelesReport_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            validateDatesProductSellReport();
        }

        private void dpendDateProductSelesReport_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            validateDatesProductSellReport();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (validateDatesProductSellReport())
            {
                RefreshGridProductSelesReportGrid();



            }

        }
        public void RefreshGridProductSelesReportGrid()
        {
            HerbalDB herbal = new HerbalDB();
            DataTable originalTable;
            originalTable = herbal.GetProductSeleareport(dpStartDateproductSelesReport.SelectedDate.Value, dpendDateProductSelesReport.SelectedDate.Value);
            ProductSelesReportGrid.ItemsSource = originalTable.DefaultView;
        }

        private void btnChartProductSelesreport_Click(object sender, RoutedEventArgs e)

        {
            try
            {
                // بررسی انتخاب تاریخ
                if (!dpStartDateproductSelesReport.SelectedDate.HasValue ||
                    !dpendDateProductSelesReport.SelectedDate.HasValue)
                {
                    MessageBox.Show("لطفاً تاریخ شروع و پایان را انتخاب کنید", "هشدار");
                    return;
                }




                // دریافت تاریخ‌های انتخاب شده
                DateTime startDate = dpStartDateproductSelesReport.SelectedDate.Value;
                DateTime endDate = dpendDateProductSelesReport.SelectedDate.Value;

                HerbalDB herbal = new HerbalDB();

                // دریافت داده‌ها
                DataTable dt = herbal.GetProductSeleareport(startDate, endDate);

                // بررسی وجود داده
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("داده‌ای برای نمایش وجود ندارد", "هشدار");
                    return;
                }

                var chartWindow = new Chart(startDate, endDate);
                chartWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا: {ex.Message}", "خطا");
            }

        }

        private void DateEntryPageForCahrtBack_Click(object sender, RoutedEventArgs e)
        {
            dpendDateProductSelesReport.SelectedDate = null;
            dpStartDateproductSelesReport.SelectedDate = null;


            ProductSelesReportGrid.ItemsSource = null;

            endAndStartProductSelesReportDateError.Visibility = Visibility.Collapsed;
            DateEntryPageForCahrt.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Visible;


        }


        //گزارش سود  
        private void btnProfitSalesReport_Click(object sender, RoutedEventArgs e)
        {

            Main.Visibility = Visibility.Collapsed;
            ProfitReportPage.Visibility = Visibility.Visible;
            profitReportDateError.Visibility = Visibility.Collapsed;
        }

        private void ProfitReportPageBack_Click(object sender, RoutedEventArgs e)
        {
            profitReportDateError.Visibility = Visibility.Collapsed;
            ProfitReportGrid.ItemsSource = null;
            MathlyProfitReportGrid.ItemsSource = null;
            YearlyProfitReportGrid.ItemsSource = null;
            dpStartDateProfitReport.SelectedDate = null;
            dpEndDateProfitReport.SelectedDate = null;
            ProfitReportPage.Visibility = Visibility.Collapsed;
            Main.Visibility = Visibility.Visible;

        }

        private void btnDailyProfit_Click(object sender, RoutedEventArgs e)
        {
            BorderYearlyProfitReportGrid.Visibility = Visibility.Collapsed;
            BorderProfitReportGrid.Visibility = Visibility.Visible;
            BorderMathlyProfitReportGrid.Visibility = Visibility.Collapsed;

            try
            {
                // بررسی انتخاب تاریخ
                if (!dpStartDateProfitReport.SelectedDate.HasValue ||
                    !dpEndDateProfitReport.SelectedDate.HasValue)
                {
                    profitReportDateError.Text = "تاریخ نا معتبر است ";
                    profitReportDateError.Visibility = Visibility.Visible;

                    return;
                }
                profitReportDateError.Visibility = Visibility.Collapsed;
                DateTime startDate = dpStartDateProfitReport.SelectedDate.Value;
                DateTime endDate = dpEndDateProfitReport.SelectedDate.Value;
                if (validateDatesProfitReport())
                {
                    HerbalDB herbal = new HerbalDB();
                    DataTable originalTable;

                    originalTable = herbal.GetDailyProfitReports(startDate, endDate);
                    ProfitReportGrid.ItemsSource = originalTable.DefaultView;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا: {ex.Message}", "خطا");
            }
        }

        private void btnMonthlyProfit_Click(object sender, RoutedEventArgs e)
        {
            BorderProfitReportGrid.Visibility = Visibility.Collapsed;
            BorderMathlyProfitReportGrid.Visibility = Visibility.Visible;
            BorderYearlyProfitReportGrid.Visibility = Visibility.Collapsed;


            try
            {
                // بررسی انتخاب تاریخ
                if (!dpStartDateProfitReport.SelectedDate.HasValue ||
                    !dpEndDateProfitReport.SelectedDate.HasValue)
                {
                    profitReportDateError.Text = "تاریخ نا معتبر است ";
                    profitReportDateError.Visibility = Visibility.Visible;

                    return;
                }
                profitReportDateError.Visibility = Visibility.Collapsed;

                DateTime startDate = dpStartDateProfitReport.SelectedDate.Value;
                DateTime endDate = dpEndDateProfitReport.SelectedDate.Value;
                if (validateDatesProfitReport())
                {

                    HerbalDB herbal = new HerbalDB();
                    DataTable originalTable;

                    originalTable = herbal.GetMonthlyProfitReports(startDate, endDate);
                    MathlyProfitReportGrid.ItemsSource = originalTable.DefaultView;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا: {ex.Message}", "خطا");
            }

        }

        private void btnYearlyProfit_Click(object sender, RoutedEventArgs e)
        {


            BorderProfitReportGrid.Visibility = Visibility.Collapsed;
            BorderMathlyProfitReportGrid.Visibility = Visibility.Collapsed;
            BorderYearlyProfitReportGrid.Visibility = Visibility.Visible;

            try
            {
                // بررسی انتخاب تاریخ
                if (!dpStartDateProfitReport.SelectedDate.HasValue ||
                    !dpEndDateProfitReport.SelectedDate.HasValue)
                {
                    profitReportDateError.Text = "تاریخ نا معتبر است ";
                    profitReportDateError.Visibility = Visibility.Visible;

                    return;
                }
                if (validateDatesProfitReport())
                {
                    profitReportDateError.Visibility = Visibility.Collapsed;

                    DateTime startDate = dpStartDateProfitReport.SelectedDate.Value;
                    DateTime endDate = dpEndDateProfitReport.SelectedDate.Value;


                    HerbalDB herbal = new HerbalDB();
                    DataTable originalTable;

                    originalTable = herbal.GetYearlyProfitReports(startDate, endDate);
                    YearlyProfitReportGrid.ItemsSource = originalTable.DefaultView;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا: {ex.Message}", "خطا");
            }




        }
        public bool validateDatesProfitReport()
        {

            DateTime? startDate = dpStartDateProfitReport.SelectedDate;
            DateTime? endDate = dpEndDateProfitReport.SelectedDate;


            if (startDate.HasValue && endDate.HasValue)
            {
                if (endDate < startDate)
                {
                    profitReportDateError.Text = " تاریخ نامعتبر است";
                    profitReportDateError.Visibility = Visibility.Visible;
                    return false;
                }


            }
            if (startDate.HasValue && !endDate.HasValue)
            {
                profitReportDateError.Text = " تاریخ پایان تعیین نشده است";
                profitReportDateError.Visibility = Visibility.Visible;
                return false;
            }
            if (!startDate.HasValue && endDate.HasValue)
            {
                profitReportDateError.Text = " تاریخ شروع تعیین نشده است";
                profitReportDateError.Visibility = Visibility.Visible;
                return false;
            }
            if (!startDate.HasValue && !endDate.HasValue)
            {
                profitReportDateError.Text = "تاریخ نامعتبر است";
                profitReportDateError.Visibility = Visibility.Visible;
                return false;
            }
            profitReportDateError.Visibility = Visibility.Collapsed;
            return true;
        }


        private void dpEndDateProfitReport_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            validateDatesProfitReport();
        }

        private void dpStartDateProfitReport_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            validateDatesProfitReport();
        }


    }

}

