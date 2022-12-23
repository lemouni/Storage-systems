using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProductViewModel
    {
        //برای نمایش محصولات در فاکتور
        public int id { get; set; }
        public string invoicenumberC { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double Percentage { get; set; }
        public int count { get; set; }
        public double total { get; set; }
        public string anbarname { get; set; }
        public string masolsabt { get; set; }
        public string date { get; set; }
        public string masolmarjo { get; set; }
        public string datemarjo { get; set; }
    }
}
