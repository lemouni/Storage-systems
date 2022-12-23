using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProductInAnbarViewModel
    {
        ///برای نمایش انبار های هر محصول در فرم محصولات در قسمت جزییات نمایش
        public int id { get; set; }
        public string NameProduct { get; set; }
        public string NameAnbar { get; set; }
        public string AdressAnbar { get; set; }
        public int count { get; set; }
    }
}
