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

namespace WpfMedecine
{
    /// <summary>
    /// Interaction logic for MedicineSellQuantity.xaml
    /// </summary>
    public partial class MedicineSellQuantity : Window
    {
        public MedicineSellQuantity()
        {
            InitializeComponent();
        }
        public float Quantiry;
       public bool ValidateInputQuantity()
        {
            if (txtQuantity.Text == "")
            {
                MedicinSellQuantityError.Visibility = Visibility.Visible;
                return false;
            }
            if (!IsNumberInput(txtQuantity.Text))
            {
                IsdigitMedicinSellQuantityError.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }
        public bool IsNumberInput(string input)
        {
            if (double.TryParse(input, out _))
            {
                return true;
            }
            return false;
        }
        private void txtQuantity_GotFocus(object sender, RoutedEventArgs e)
        {
            MedicinSellQuantityError.Visibility = Visibility.Collapsed;
            IsdigitMedicinSellQuantityError.Visibility = Visibility.Collapsed;
        }
        

        

        private void btnQuanity_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputQuantity())
            {
                Quantiry = Convert.ToSingle(txtQuantity.Text);
                this.Close();
            }
           

        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtQuantity.Text = "";
        }
    }
}
