using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class ApiSmsDAL
    {
        DB db = new DB();
        public string Create(ApiSms a)
        {
            try
            {
                db.ApiSmss.RemoveRange(db.ApiSmss.ToList());

                db.ApiSmss.Add(a);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string ReadApiKay()
        {
            var q = db.ApiSmss.OrderByDescending(i => i.id).FirstOrDefault();
            if (q != null)
            {
                return q.UserApiKey;
            }
            return "";
        }
        public string ReadSecretKay()
        {
            var q = db.ApiSmss.OrderByDescending(i => i.id).FirstOrDefault();
            if (q != null)
            {
                return q.SecretKey;
            }
            return "";
        }
        public string ReadKhat()
        {
            var q = db.ApiSmss.OrderByDescending(i => i.id).FirstOrDefault();
            if (q != null)
            {
                return q.Khat;
            }
            return "";
        }
    }
}
