using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ProductCategory
    {
        public ProductCategory()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string CategoryNameP { get; set; }
        public bool DeleteStatus { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
