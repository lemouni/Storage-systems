using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class CategoriNoeDAL
    {
        DB db = new DB();
        public string Create(CategoriNoe cn)
        {
            try
            {
                db.CategoriNoes.Add(cn);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public bool Read(CategoriNoe cn)
        {
            var q = db.CategoriNoes.Where(i => cn.name == i.name);
            if (q.Count() == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT id, name AS [نوع پرداخت] FROM dbo.CategoriNoes WHERE (DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public CategoriNoe Read(int id)
        {
            return db.CategoriNoes.Where(i => i.id == id).FirstOrDefault();
        }
        public string Update(CategoriNoe cn, int id)
        {
            var q = db.CategoriNoes.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.name = cn.name;


                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "نوع پرداخت مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string Delete(int id)
        {
            var q = db.CategoriNoes.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null && q.banks.Count() == 0)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "نوع پرداخت مورد نظر یافت نشد یا در نوع پرداخت مورد نظر حساب بانکی وجود دارد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public List<string> ReadNamesANC()
        {
            return db.CategoriNoes.Where(i => i.DeleteStatus == false).Select(i => i.name).ToList();
        }
        public CategoriNoe Read(string p)
        {
            return db.CategoriNoes.Where(i => i.name == p && i.DeleteStatus == false).SingleOrDefault();
        }
    }
}
