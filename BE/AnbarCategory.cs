using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class AnbarCategory
    {
        public AnbarCategory()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string AnbarName { get; set; }
        public string AnbarAdress { get; set; }
        public bool DeleteStatus { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<CounProductInAnbar> CounProductInAnbars { get; set; } = new List<CounProductInAnbar>();
    }
}
