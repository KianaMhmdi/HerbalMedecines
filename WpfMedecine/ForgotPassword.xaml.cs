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
using System.Text.RegularExpressions;
using WpfMedecine.Data;

namespace WpfMedecine
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void btnChang_Click(object sender, RoutedEventArgs e)
        {
            if (validateInput())
            {
                Person person = new Person();
                HerbalDB herbalDB = new HerbalDB();

                person.Id = txtNationalCode.Text.ToString();
                person.PhoneNumber = txtPhoneNumber.Text.ToString();
                person.FavoriteNumber = txtFavoriteNumber.Text.ToString();
                person.PasswordHash = txtNewPassword.Password;

                if (herbalDB.EditPassword(person))
                {
                    MessageBox.Show("✅ Password change was successful !", "Registration ", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Password change was unsuccessful !", "Registration ", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Close();
                }
;
            }
        }
        //محدودیت رمز جدید
        bool isValidNewPassword(string password)
        {
            if (txtNewPassword.Password.Length < 8 || txtNewPassword.Password.Length > 16)
            {
                return false;

            }
            bool hasletter = password.Any(char.IsLetter);
            bool hasDigit = password.Any(char.IsDigit);
            return hasDigit && hasletter;
        }
        //ارزیابی فید ها 
        int flag = 1;
        public bool validateInput()
        {

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
            //شماره تلفن
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

            ///عدد مورد علاقه
            if (txtFavoriteNumber.Text == "")
            {
                FavoriteNumberError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtFavoriteNumber.Text != "")
            {
                FavoriteNumberError.Visibility = Visibility.Collapsed;
            }
            //پسورد

            if (txtNewPassword.Password == "")
            {
                PasswordError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtNewPassword.Password != "")
            {
                PasswordError.Visibility = Visibility.Collapsed;
            }

            if (!isValidNewPassword(txtNewPassword.Password) && txtNewPassword.Password.Length > 0)
            {
                VallidPasswordError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtRepeatNewPassword.Password == "")
            {
                RepeatError.Visibility = Visibility.Visible;
                flag = 0;
            }

            if (txtRepeatNewPassword.Password != "")
            {
                RepeatError.Visibility = Visibility.Collapsed;
            }

            if (txtRepeatNewPassword.Password != txtRepeatNewPassword.Password && txtRepeatNewPassword.Password.Length > 0)
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
    }
}
