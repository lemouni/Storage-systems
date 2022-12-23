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
    public class CountProductBLL
    {
        CountProductDAL dal = new CountProductDAL();

        public List<CountProduct> GetCountProductsByInvoiceId(int InvoiceID)
        {
            return dal.GetCountProductsByInvoiceId(InvoiceID);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public string SabtKhorojoDone(User u, int id)
        {
            return dal.SabtKhorojoDone(u, id);
        }
        public string MarjoDone(User u, int id)
        {
            return dal.MarjoDone(u,id);
        }
    }
}
