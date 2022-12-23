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
    public class AnbarCategoryBLL
    {
        AnbarCategorDAL dal = new AnbarCategorDAL();
        public string Create(AnbarCategory anc)
        {
            return dal.Create(anc);
        }
        public bool Read(AnbarCategory anc)
        {
            return dal.Read(anc);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public AnbarCategory Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(AnbarCategory anc, int id)
        {
            return dal.Update(anc, id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<string> ReadNamesANC()
        {
            return dal.ReadNamesANC();
        }
        public AnbarCategory Read(string p)
        {
            return dal.Read(p);
        }
    }
}
