using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BuyPardakht
    {
        public BuyPardakht()
        {
            PardakhtShod = false;
            Bargasht = false;
        }
        public int id { get; set; }
        public HesabBank HesabBank { get; set; }
        public Buy Buy { get; set; }
        public string Tozih { get; set; }
        public double Pardakhti { get; set; }
        public string DatePardakht { get; set; }
        public bool PardakhtShod { get; set; }
        public string MasolPardakhtShod { get; set; }
        public string DatePardakhtShod { get; set; }
        public bool Bargasht { get; set; }
        public string MasolBargasht { get; set; }
        public string DateBargasht { get; set; }
        public NoePardakht NoePardakht { get; set; }
        public CategoriNoe CategoriNoe { get; set; }

    }
}
