using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class BuyBLL
    {
        BuyDAL dal = new BuyDAL();
        public void CheckPardakht()
        {
            dal.CheckPardakht();
        }
        public Buy Create(Buy bi, User u, List<Buy_CountProduct> bcc)
        {
            return dal.Create(bi,u, bcc);
        }
        public string ReadBuyNum()
        {
            return dal.ReadBuyNum();
        }
        public List<BuyViewModel> ReadViewModel()
        {
            return dal.ReadViewModel();
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public List<BuyViewModel> SearchReadViewModel(string s)
        {
            return dal.SearchReadViewModel(s);
        }
        public Buy Read(int id)
        {
            return dal.Read(id);
        }
        public Buy ReadP(int id)
        {
            return dal.ReadP(id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public string Done(int id)
        {
            return dal.Done(id);
        }
        public string NotDone(int id)
        {
            return dal.NotDone(id);
        }
        public Buy Readd(string p)
        {
            return dal.Readd(p);
        }
        public List<string> ReadNamesANC()
        {
            return dal.ReadNamesANC();
        }
        public List<Buy> ReadBuy(DateTime strat, DateTime end)
        {
            return dal.ReadBuy(strat, end);
        }
    }
}
