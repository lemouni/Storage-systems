using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class ReminderDAL
    {
        DB db = new DB();
        public string Create(Reminder r , User u)
        {
            try
            {
                r.Users = db.Users.Find(u.id);
                if (u.UserName !=null)
                {
                db.Reminders.Add(r);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "یوزر کاربر مربوطه در بانک اطلاعاتی یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "ثبت اطلاعات با مشکلی مواجه شده\n"+ e.Message;
            }
        }
        public DataTable Read()
        {
            SqlCommand cmd = new SqlCommand("dbo.ReadRminders");
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            
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
        public Reminder Read(int id)
        {
            return db.Reminders.Where(i => i.id == id).FirstOrDefault();
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchReminders");
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
        public string Update(Reminder r , int id)
        {
            var q = db.Reminders.Where(i => i.id == id).SingleOrDefault();
            try
            {
                if (q !=null)
                {
                    q.RemindDate = r.RemindDate;
                    q.ReminderInfo = r.ReminderInfo;
                    q.Title = r.Title;
                    
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "یادآور مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.Reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    db.Reminders.Remove(q);
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "یادآور مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string Done(int id)
        {
            var q = db.Reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsDone = true;
                    db.SaveChanges();
                    return "یادآور روی انجام شده قرار گرفت";
                }
                else
                {
                    return "یادآور مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string NotDone(int id)
        {
            var q = db.Reminders.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsDone = false;
                    db.SaveChanges();
                    return "یادآور روی انجام نشده قرار گرفت";
                }
                else
                {
                    return "یادآور مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
    }
}
