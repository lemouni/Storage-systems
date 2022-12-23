using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class NoePardakht
    {
        public int id { get; set; }
        public CategoriNoe CategoriNoe { get; set; }
        public HesabBank HesabBank { get; set; }
        public double MoJodi { get; set; }
        public List<BuyPardakht> BuyPardakhts { get; set; } = new List<BuyPardakht>();
        List<SellDaryaft> SellDaryafts { get; set; } = new List<SellDaryaft>();
    }
}
