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
    public class SellDaryaftBLL
    {
        SellDaryaftDAL dal = new SellDaryaftDAL();
        public List<SellDaryaft> GetCountDaryaftssByHesabId(int HesabID)
        {
            return dal.GetCountDaryaftssByHesabId(HesabID);
        }
        public List<SellDaryaft> GetCountHesabsByBuyId(int InvoiceID)
        {
            return dal.GetCountHesabsByBuyId(InvoiceID);
        }
        public string Create(SellDaryaft sd, Invoice i, HesabBank hb, CategoriNoe cn)
        {
            return dal.Create(sd, i, hb, cn);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public SellDaryaft Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(SellDaryaft sd, Invoice i, HesabBank hb, CategoriNoe cn, int id)
        {
            return dal.Update(sd,i,hb,cn,id);
        }
        public string SabtDaryaftShod(User u, int id)
        {
            return dal.SabtDaryaftShod(u,id);
        }
        public string BargashtDone(User u, int id)
        {
            return dal.BargashtDone(u,id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<SellDaryaft> ReadBuyDaryaftNotChecked()
        {
            return dal.ReadBuyDaryaftNotChecked();
        }
    }
}
