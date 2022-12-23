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
    public class AnbarCategorDAL
    {
        DB db = new DB();
        public string Create(AnbarCategory anc)
        {
            try
            {
                db.AnbarCategories.Add(anc);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public bool Read(AnbarCategory anc)
        {
            var q = db.AnbarCategories.Where(i => anc.AnbarName == i.AnbarName);
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
            string cmd = "SELECT id, AnbarName AS [نام انبار], AnbarAdress AS [آدرس انبار] FROM dbo.AnbarCategories WHERE (DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public AnbarCategory Read(int id)
        {
            return db.AnbarCategories.Where(i => i.id == id).FirstOrDefault();
        }
        public string Update(AnbarCategory anc, int id)
        {
            var q = db.AnbarCategories.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.AnbarName = anc.AnbarName;
                    q.AnbarAdress = anc.AnbarAdress;


                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "انبار مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string Delete(int id)
        {
            var q = db.AnbarCategories.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null && q.Products.Count==0)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "انبار مورد نظر یافت نشد یا در انبار مورد نظر محصول وجود دارد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public List<string> ReadNamesANC()
        {
            return db.AnbarCategories.Where(i => i.DeleteStatus == false).Select(i => i.AnbarName).ToList();
        }
        public AnbarCategory Read(string p)
        {
            return db.AnbarCategories.Where(i => i.AnbarName == p && i.DeleteStatus == false).SingleOrDefault();
        }
    }
}
