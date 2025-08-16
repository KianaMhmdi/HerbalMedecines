using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMedecine
{
    internal class Orders
    {
        public string Id { get; set; }
        public string NameMedecines { get; set; }
        public float REqQuantity { get; set; }
        public float PriceSell { set; get; }
        public string Unit { get; set; }
        public float TotalAmunt { get; set; }
        public string CusId { get; set; }
        public string CusFName { get; set; }
        public string CusLName { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
