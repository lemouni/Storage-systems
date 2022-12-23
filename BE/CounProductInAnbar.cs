using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CounProductInAnbar
    {
        public CounProductInAnbar()
        {
            Kharab = false;
        }
        public int id { get; set; }
        public AnbarCategory anbarCategoryP { get; set; }
        public Product productP { get; set; }
        public int count { get; set; }
        public bool Kharab { get; set; }
        public List<CountProduct> countProducts { get; set; } = new List<CountProduct>();
        public List<Buy_CountProduct> Buy_CountProducts { get; set; } = new List<Buy_CountProduct>();
    }
}
