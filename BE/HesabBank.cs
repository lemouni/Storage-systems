using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HesabBank
    {
        public HesabBank()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string name { get; set; }
        public string Sheba { get; set; }
        public string ShomareHesab { get; set; }
        public double Stock { get; set; }
        public List<Buy> Buys { get; set; } = new List<Buy>();
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public List<NoePardakht> NoePardakhts { get; set; } = new List<NoePardakht>();
        public List<CategoriNoe> CategoriNoes { get; set; } = new List<CategoriNoe>();
        public List<BuyPardakht> BuyPardakhts { get; set; } = new List<BuyPardakht>();
        public List<SellDaryaft> SellDaryafts { get; set; } = new List<SellDaryaft>();
        public bool DeleteStatus { get; set; }




    }
}
