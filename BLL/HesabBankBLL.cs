using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;
using System.Windows;

namespace BLL
{
    public class HesabBankBLL
    {
        HesabAsnadDAL dal = new HesabAsnadDAL();
        public string Create(HesabBank hb)
        {
            if (dal.Read(hb))
            {
                return dal.Create(hb);

            }
            else
            {
                return "حسابی با همین شماره حساب در سیستم ثبت شده است";
            }
        }
        public bool Read(HesabBank hb)
        {
            return dal.Read(hb);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public HesabBank ReadN(string p)
        {
            return dal.ReadN(p);
        }
        public HesabBank Read(int id)
        {
            return dal.Read(id);
        }
        public List<string> ReadNmaes()
        {
            return dal.ReadNmaes();
        }
        public string Update(HesabBank p, int id)
        {
            return dal.Update(p, id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public HesabBank Readdd(string p)
        {
            return dal.Readdd(p);
        }
    }
}
