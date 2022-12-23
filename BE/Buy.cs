using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Buy
    {
        public Buy()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string BuyNumber { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Tozih { get; set; }
        public double FeepaidB { get; set; }
        public double TotalCostB { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RegDate { get; set; }
        public bool IsCheckOut { get; set; }
        [Column(TypeName = "datetime2")]
        public Nullable<DateTime> CheckOutDate { get; set; }
        public bool DeleteStatus { get; set; }
        public User User { get; set; }
        public List<Product> ProductsB { get; set; } = new List<Product>();
        public List<Buy_CountProduct> Buy_CountProducts { get; set; } = new List<Buy_CountProduct>();
        public List<HesabBank> HesabBanks { get; set; } = new List<HesabBank>();
        public List<BuyPardakht> BuyPardakhts { get; set; } = new List<BuyPardakht>();
    }
}
