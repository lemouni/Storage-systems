using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class MoshkhasatFactorDAL
    {
        DB db = new DB();
        public string Create(MoshkhasatFactor m)
        {
            try
            {
                db.MoshkhasatFactors.RemoveRange(db.MoshkhasatFactors.ToList());

                db.MoshkhasatFactors.Add(m);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string ReadNameForosghah()
        {
            var q = db.MoshkhasatFactors.OrderByDescending(i => i.id).FirstOrDefault();
            if (q != null)
            {
                return q.NameForoshgah;
            }
            return "";
        }
    }
}
