using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BuyViewModel
    {
        //برای نمایش فرم خرید در دیتا گرید ویو با روش دوم ویو مدل
        public int id { get; set; }
        public string BuyNumber { get; set; }
        public DateTime RegDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool IsCheckedOut { get; set; }
        public string CheckoutDate { get; set; }
        public string TypeB { get; set; }

        public string Titleb { get; set; }
        public string ToziB { get; set; }
        public double feepaidB { get; set; }
        public double totalcostB { get; set; }
        public string nameuserB { get; set; }
    }
}
