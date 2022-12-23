using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class CategoriNoe
    {
        public CategoriNoe()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string name { get; set; }
        public List<HesabBank> banks { get; set; } = new List<HesabBank>();
        public List<NoePardakht> noePardakhts { get; set; } = new List<NoePardakht>();
        public bool DeleteStatus { get; set; }
        public List<BuyPardakht> BuyPardakhts { get; set; } = new List<BuyPardakht>();
        public List<SellDaryaft> SellDaryafts { get; set; } = new List<SellDaryaft>();


    }
}
