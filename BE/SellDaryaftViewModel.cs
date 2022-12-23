using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class SellDaryaftViewModel
    {
        public int id { get; set; }
        public string HesabBank { get; set; }
        public string Invoice { get; set; }
        public string Tozih { get; set; }
        public double Daryafti { get; set; }
        public string DateDaryafti { get; set; }
        public bool DaryaftShod { get; set; }
        public string MasolDaryaftShod { get; set; }
        public string DateDaryaftShod { get; set; }
        public bool Bargasht { get; set; }
        public string MasolBargasht { get; set; }
        public string DateBargasht { get; set; }
        public string CategoriNoe { get; set; }
    }
}
