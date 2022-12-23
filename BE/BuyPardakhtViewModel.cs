using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BuyPardakhtViewModel
    {
        public int id { get; set; }
        public string HesabBank { get; set; }
        public string Buy { get; set; }
        public string Tozih { get; set; }
        public double Pardakhti { get; set; }
        public string DatePardakht { get; set; }
        public bool PardakhtShod { get; set; }
        public string MasolPardakhtShod { get; set; }
        public string DatePardakhtShod { get; set; }
        public bool Bargasht { get; set; }
        public string MasolBargasht { get; set; }
        public string DateBargasht { get; set; }
        public string CategoriNoe { get; set; }
    }
}
