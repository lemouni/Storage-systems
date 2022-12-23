using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Invoice
    {
        public Invoice()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string InvoiceNumber { get; set; }
        public double FeePaid { get; set; }
        public double TotalCost { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RegDate { get; set; }
        public bool DeleteStatus { get; set; }
        public bool IsCheckedOut { get; set; }
        [Column(TypeName = "datetime2")]
        public Nullable <DateTime> CheckoutDate { get; set; }
        public Customer Customer { get; set; }
        public User User { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public List<CountProduct> CountProducts { get; set; } = new List<CountProduct>();
        public List<HesabBank> HesabBanks { get; set; } = new List<HesabBank>();
        public List<SellDaryaft> SellDaryafts { get; set; } = new List<SellDaryaft>();
    }
}
