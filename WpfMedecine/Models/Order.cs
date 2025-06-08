using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMedecine
{
    class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public string MedincineId { get; set; }
        public float Quntity { set; get; }
        public float TotalAmunt { get; set; }
        public string OrderAddress { get; set; }
        public DateTime orderTime { set; get; }
    }
}
