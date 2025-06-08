using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Registeration.xaml
    /// </summary>
    public partial class Registeration : Window
    {
        
        public Registeration()
        {
            InitializeComponent();
           
        }
        //
        bool isValidPassword(string password)
        {
            if (txtPassword.Password.Length < 8 || txtPassword.Password.Length > 16)
            {
                return false;

            }
            bool hasletter = password.Any(char.IsLetter);
            bool hasDigit = password.Any(char.IsDigit);
            return hasDigit && hasletter;
        }

        int flag = 1;
        public bool ValidateInput()
        {
            if (txtFirstName.Text == "")
            {
                FnameError.Visibility = Visibility.Visible;
                flag = 0;
            }


            if (txtFirstName.Text != "")
            {
                FnameError.Visibility = Visibility.Collapsed;
            }

            if (txtLastName.Text =="")
            {
                LnameError.Visibility = Visibility.Visible;
                flag = 0;
            }
            if (txtLastName.Text != "")
            {
                LnameError.Visibility = Visibility.Collapsed;
            }



            if (txtNationalCode.Text == "")
            {
                NationalCodeError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (!Regex.IsMatch(txtNationalCode.Text, @"^\d+$"))
            {
                NationalCodeError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtNationalCode.Text != "")
            {
                NationalCodeError.Visibility = Visibility.Collapsed;
            }

            if (!Regex.IsMatch(txtNationalCode.Text, @"^\d+$") && txtNationalCode.Text.Length > 0)
            {
                IsdigitNationalCodeError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtNationalCode.Text.Length != 10 && txtNationalCode.Text.Length > 0)
            {
                LengthNationalCodeError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtPhoneNumber.Text == "")
            {
                PhonrnumberError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtPhoneNumber.Text != "")
            {
                PhonrnumberError.Visibility = Visibility.Collapsed;
            }

            if (!Regex.IsMatch(txtPhoneNumber.Text, @"^\d+$") && txtPhoneNumber.Text.Length > 0)
            {
                IsdigitPhoneNumberError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtPhoneNumber.Text.Length != 11 && txtPhoneNumber.Text.Length > 0)
            {
                LengthPhoneNumberError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtaddress.Text == "")
            {
                AddressError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtaddress.Text != "")
            {
                AddressError.Visibility = Visibility.Collapsed;
            }

            if (txtFavoriteNumber.Text == "")
            {
                FavoriteNumberError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtFavoriteNumber.Text != "")
            {
                FavoriteNumberError.Visibility = Visibility.Collapsed;
            }

            if (dpBirthDate.SelectedDate == null)
            {
                NullAgeError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (dpBirthDate.SelectedDate != null)
            {
                NullAgeError.Visibility = Visibility.Collapsed;
            }

            if (txtPassword.Password == "")
            {
                PasswordError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtPassword.Password != "")
            {
                PasswordError.Visibility = Visibility.Collapsed;
            }

            if (!isValidPassword(txtPassword.Password) && txtPassword.Password.Length > 0)
            {
                VallidPasswordError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtRepeatPassword.Password == "")
            {
                RepeatError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtRepeatPassword.Password != "")
            {
                RepeatError.Visibility = Visibility.Collapsed;
            }

            if (txtPassword.Password != txtRepeatPassword.Password  && txtRepeatPassword.Password.Length > 0)
            {
                TrueRepeatError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (flag == 0)
            {
                return false;
            }


                return true;
        }

        // وقتی کلیک کرد روی فیلد ایمیل
        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text == "(Optional)")
            {
                txtEmail.Text = "";
                txtEmail.Foreground = Brushes.Black;
            }
        }
        //وقتی که کلیک نکرده روی فید ایمیل
        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmail.Text = "(Optional)";
                txtEmail.Foreground = Brushes.Gray;
            }
        }
        //تابع برای  تاریخ تولد دارای محدودیت سنی
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
                if (age < 18 || age > 70)
                {
                    AgeError.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            HerbalDB herbalDB = new HerbalDB();
            Person person = new Person();

            if (ValidateInput())
            {
                ///مقدار دهی پارمتر هایی که کاربر وارو می کند
                person.FirstName = txtFirstName.Text.ToString();
                person.LastName = txtLastName.Text.ToString();
                person.Id = txtNationalCode.Text.ToString();
                person.Gender = ((ComboBoxItem)cmbGender.SelectedItem).Content.ToString();
                person.Role = ((ComboBoxItem)cmbRole.SelectedItem).Content.ToString();
                person.PhoneNumber = txtPhoneNumber.Text.ToString();
                person.Email = txtEmail.Text.ToString();
                person.BirthDate = dpBirthDate.SelectedDate.Value;
                person.PasswordHash = txtPassword.Password.ToString();
                person.Address = txtaddress.Text.ToString();
                person.FavoriteNumber = txtFavoriteNumber.Text.ToString();
                if (RegistertionOrEdite.Text == "Edite")
                {
                    if (herbalDB.UpdatePerson(person))
                    {
                        MessageBox.Show("✅ Edite was successful !", "Edite ", MessageBoxButton.OK);
                        RegistertionOrEdite.Text = "Registration";
                        btnRegister.Content = "Registration";
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Edite was unsuccessful !", "Edite ", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                }
                else
                {
                    //بررسی می کند که ایا ثبت نام وفقیت امیز بوده یا نه
                    if (herbalDB.AddPerson(person))
                    {
                        MessageBox.Show("✅ Registration was successful !", "Registration ", MessageBoxButton.OK);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Registration was unsuccessful !", "Registration ", MessageBoxButton.OK, MessageBoxImage.Error);
                        this.Close();
                    }
                }
            }
        }
    }
}
