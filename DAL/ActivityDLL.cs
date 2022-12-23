using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class ActivityDLL
    {
        DB db = new DB();
        public string Create (Activity a, User u, Customer c, ActivityCategory ac)
        {
            try
            {
                a.User = db.Users.Find(u.id);
                a.Customer = db.Customers.Find(c.id);
                a.Category = db.ActivityCategories.Find(ac.id);
                if (u.UserName != null && c.Phone != null && ac.CategoryName != null)
                {
                    db.Activities.Add(a);
                    db.SaveChanges();
                    return "ثبت اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "شماره تماس مشتری یا یوزر مربوطه یا دسته فعالیتی مورد نظر انتخاب در بانک اطلاعاتی یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "ثبت اطلاعات با مشکلی مواجه شده است" + e.Message;
            }
            
        }

        public DataTable Read()
        {
            string cmd = @"SELECT dbo.Activities.id, dbo.Activities.Title AS [موضوع فعالیت], dbo.ActivityCategories.CategoryName AS [دسته بندی], dbo.Customers.Name AS [نام مشتری], dbo.Users.Name AS [نام کاربر], dbo.Activities.RegDate AS [تاریخ ثبت] FROM dbo.Activities INNER JOIN dbo.ActivityCategories ON dbo.Activities.Category_id = dbo.ActivityCategories.id INNER JOIN dbo.Customers ON dbo.Activities.Customer_id = dbo.Customers.id INNER JOIN dbo.Users ON dbo.Activities.User_id = dbo.Users.id WHERE (dbo.Activities.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRMToseGar;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];

            //SqlCommand cmd = new SqlCommand("dbo.SearchActivity");
            //SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");

            //cmd.Connection = con;
            //cmd.CommandType = CommandType.StoredProcedure;
            ////string cmd = "exec dbo.SearchCustomer";
            //var sqladapter = new SqlDataAdapter();
            //sqladapter.SelectCommand = cmd;
            //var commandbuilder = new SqlCommandBuilder(sqladapter);
            //var ds = new DataSet();
            //sqladapter.Fill(ds);
            //return ds.Tables[0];
        }

        public Activity Read(int id)
        {
            return db.Activities.Where(i => i.id == id).FirstOrDefault();
        }
        public string Delete(int id)
        {
            try
            {
                var q = db.Activities.Where(i => i.id == id).SingleOrDefault();
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف فعالیت با موفقیت انجام شد";
                }
                else
                {
                    return "فعالیت مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
            
        }
        public string Update(Activity a,ActivityCategory ac, int id)
        {
            var q = db.Activities.Where(i => i.id == id).SingleOrDefault();
            var qq = db.ActivityCategories.Where(i => i.id == id).SingleOrDefault();
            try
            {
                if (q != null && qq!=null)
                {
                    q.Title = a.Title;
                    q.RegDate = a.RegDate;
                    q.Info = a.Info;
                    qq.CategoryName = ac.CategoryName;

                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "فعالیت مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد" + e.Message;
            }
        }
        public DataTable Read(string s, int index)
        {
            SqlCommand cmd = new SqlCommand();
            if (index == 0)
            {
                cmd.CommandText = "dbo.SearchActivity";

            }
            else if (index == 1)
            {
                cmd.CommandText = "dbo.SearchActivityNameUser";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchActivityNameCustomer";
            }
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            cmd.Parameters.AddWithValue("@search", s);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            //string cmd = "exec dbo.SearchCustomer";
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

    }

}
