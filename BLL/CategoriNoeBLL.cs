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
    public class CategoriNoeBLL
    {
        CategoriNoeDAL dal = new CategoriNoeDAL();
        public string Create(CategoriNoe cn)
        {
            if (dal.Read(cn))
            {
                return dal.Create(cn);

            }
            else
            {
                return "این نوع پرداخت تکراری می باشد";
            }
        }
        public bool Read(CategoriNoe cn)
        {
            return dal.Read(cn);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public CategoriNoe Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(CategoriNoe cn, int id)
        {
            return dal.Update(cn,id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);   
        }
        public List<string> ReadNamesANC()
        {
            return dal.ReadNamesANC();
        }
        public CategoriNoe Read(string p)
        {
            return dal.Read(p);
        }
    }
}
