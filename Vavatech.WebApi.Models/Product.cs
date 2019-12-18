using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Vavatech.WebApi.Models
{

    public class Product : Item
    {
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public float Weight { get; set; }
    }
}
