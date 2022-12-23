using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class InvoiceViewModel
    {
        //برای نمایش فرم فاکتور در دیتا گرید ویو با روش دوم ویو مدل
        public int id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime RegDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool IsCheckedOut { get; set; }
        //public Nullable<DateTime> CheckoutDate { get; set; }
        public string CheckoutDate { get; set; }
        public string CustomerName { get; set; }
        public string CutomerPhone { get; set; }
        public double feepaid { get; set; }
        public double totalcost { get; set; }
        public string nameuser { get; set; }
    }
}
