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
    public class ActivityCategoriDAL
    {
        DB db = new DB();
        public string Create(ActivityCategory ac)
        {
            try
            {
                db.ActivityCategories.Add(ac);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public bool Read(ActivityCategory ac)
        {
            var q = db.ActivityCategories.Where(i => ac.CategoryName == i.CategoryName);
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
            string cmd = "SELECT id, CategoryName AS [دسته بندی ها] FROM dbo.ActivityCategories WHERE (DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public ActivityCategory Read(int id)
        {
            return db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
        }
        public string Update(ActivityCategory ac, int id)
        {
            var q = db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CategoryName = ac.CategoryName;
                    

                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "نوع فعالیت مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string Delete(int id)
        {
            var q = db.ActivityCategories.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    //db.ActivityCategories.Remove(q);
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "نوع فعالیت مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public List<string> ReadNames()
        {
            return db.ActivityCategories.Where(i => i.DeleteStatus == false).Select(i => i.CategoryName).ToList();
        }
        public ActivityCategory Read(string p)
        {
            return db.ActivityCategories.Where(i => i.CategoryName == p && i.DeleteStatus==false).SingleOrDefault();
        }
    }


}
