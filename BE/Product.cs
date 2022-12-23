using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product
    {
        public Product()
        {
            DeleteStatus = false;
            RegDate = DateTime.Now;
        }
        public int id { get; set; }
        public bool DeleteStatus { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RegDate { get; set; }
        public string Name { get; set; }
        //public string Categori { get; set; }
        public double Price { get; set; }
        public double Price1 { get; set; }
        public int Stock { get; set; }
        public ProductCategory Category { get; set; }

        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public List<CountProduct> CountProducts { get; set; } = new List<CountProduct>();
        public List<AnbarCategory> AnbarCategories { get; set; } = new List<AnbarCategory>();
        public List<CounProductInAnbar> CounProductInAnbars { get; set; } = new List<CounProductInAnbar>();
        public List<Buy> Buys { get; set; } = new List<Buy>();
        public List<Buy_CountProduct> Buy_CountProducts { get; set; } = new List<Buy_CountProduct>();

    }
}
