using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class BuyPardakhtBLL
    {
        BuyPardakht_DAL dal = new BuyPardakht_DAL();
        public List<BuyPardakht> GetCountHesabsByBuyId(int BuyID)
        {
            return dal.GetCountHesabsByBuyId(BuyID);
        }
        public List<BuyPardakht> GetCountPardakhtssByHesabId(int HesabID)
        {
            return dal.GetCountPardakhtssByHesabId(HesabID);
        }

        public string Create(BuyPardakht bp, Buy b, HesabBank hb, CategoriNoe cn)
        {
            return dal.Create(bp, b, hb, cn);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public BuyPardakht Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(BuyPardakht bp, Buy b, HesabBank hb, CategoriNoe cn, int id)
        {
            return dal.Update(bp, b, hb, cn, id);
        }
        public string SabtPardakhtShod(User u, int id)
        {
            return dal.SabtPardakhtShod(u, id);
        }
        public string BargashtDone(User u, int id)
        {
            return dal.BargashtDone(u, id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<BuyPardakht> ReadBuyPadakhtNotChecked()
        {
            return dal.ReadBuyPadakhtNotChecked();
        }
    }
}
