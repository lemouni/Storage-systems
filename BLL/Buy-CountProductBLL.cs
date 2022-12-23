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
    public class Buy_CountProductBLL
    {
        Buy_CountProductDAL dal = new Buy_CountProductDAL();
        public List<Buy_CountProduct> GetCountProductsByBuyId(int BuyID)
        {
            return dal.GetCountProductsByBuyId(BuyID);
        }
        public DataTable Read()
        {
            return dal.Read();
        }

        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public string SabtVorodDone(User u, int id)
        {
            return dal.SabtVorodDone(u,id);
        }
        public string MarjoDone(User u, int id)
        {
            return dal.MarjoDone(u,id);
        }
    }
}
