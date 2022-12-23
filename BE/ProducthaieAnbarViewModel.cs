using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProducthaieAnbarViewModel
    {
        ///برای محصولات هر انبار در نمایش جزییات در فرم ستینگ قسمت مدیریت انبار
        public int id { get; set; }
        public string nameanbar { get; set; }
        public string nameproduct { get; set; }
        public int count { get; set; }
        public int totalstock { get; set; }
    }
}
