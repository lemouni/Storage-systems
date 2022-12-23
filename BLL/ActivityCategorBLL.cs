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
    public class ActivityCategorBLL
    {
        ActivityCategoriDAL dal = new ActivityCategoriDAL();
        public string Create(ActivityCategory ac)
        {
            if (dal.Read(ac))
            {
                return dal.Create(ac);
            }
            else
            {
                return "نوع فعالیت با همین نام در سیستم ثبت شده است";
            }
        }
        public bool Read(ActivityCategory ac)
        {
            return dal.Read(ac);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public ActivityCategory Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(ActivityCategory ac, int id)
        {
            return dal.Update(ac , id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<string> ReadNames()
        {
            return dal.ReadNames();
        }
        public ActivityCategory Read(string p)
        {
            return dal.Read(p);
        }
    }
}
