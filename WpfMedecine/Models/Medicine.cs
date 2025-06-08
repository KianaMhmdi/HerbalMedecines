using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMedecine
{
     class Medicine
    {
        public string Id { get; set; }
        public string NameMedecines { get; set; }
        public DateTime DateBuy { get; set; }
        public float PriceBuy { get; set; }
        public float PriceSell { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public float Price => Quantity * PriceSell; // محاسبه خودکار قیمت کل

    }
}
