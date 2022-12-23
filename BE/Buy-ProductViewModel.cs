using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Buy_ProductViewModel
    {
        //برای نمایش محصولات در خرید
        public int id { get; set; }
        public string BuynumberC { get; set; }
        public string nameProduct { get; set; }
        public double priceProduct { get; set; }
        public int count { get; set; }
        public double total { get; set; }
        public string anbarname { get; set; }
        public string masolsabt { get; set; }
        public string date { get; set; }
        public string masolmarjo { get; set; }
        public string datemarjo { get; set; }
    }
}

