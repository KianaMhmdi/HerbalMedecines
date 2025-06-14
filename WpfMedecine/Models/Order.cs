using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMedecine
{
    class Order
    {
        
        public string CustomerFullName { get; set; }
        public string MedincineName { get; set; }
        public float SellPrice  { get; set; }
        public float Quntity { set; get; }
        public float TotalAmunt { get; set; }
        public DateTime orderTime { set; get; }
    }
}
