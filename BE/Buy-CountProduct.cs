using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Buy_CountProduct
    {
        public Buy_CountProduct()
        {
            Marjo = false;
            SabtAnbar = false;
        }
        public int id { get; set; }
        public Product productB { get; set; }
        public Buy buyC { get; set; }
        public int count { get; set; }
        public double price { get; set; }
        public string anbarname { get; set; }
        public bool Marjo { get; set; }
        public string MasolMarjo { get; set; }
        public string DateMarjo { get; set; }
        public bool SabtAnbar { get; set; }
        public string MasolSabt { get; set; }
        public string DateSabt { get; set; }
        public CounProductInAnbar counProductInAnbarB { get; set; }
    }
}
