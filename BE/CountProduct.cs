using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CountProduct
    {
        public CountProduct()
        {
            Marjoei = false;
            SabtKhoroj = false;
        }
        public int id { get; set; }
        public Product producC { get; set; }
        public Invoice invoiceC { get; set; }
        public int count { get; set; } 
        public double priceselect { get; set; }
        public double Percentage { get; set; }
        public string anbarname { get; set; } 
        public bool Marjoei { get; set; }
        public string MasolMarjo { get; set; }
        public string DateMarjo { get; set; }
        public bool SabtKhoroj { get; set; }
        public string MasolSabt { get; set; }
        public string Datekhoroj { get; set; }
        public CounProductInAnbar counProductInAnbar { get; set; }

    }
}
