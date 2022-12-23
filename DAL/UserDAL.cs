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
    public class UserDAL
    {
        DB db = new DB();
        public string Create(User u,UserGroup ug)
        {
            try
            {
                if (Read(u))
                {
                    ///
                    u.UserGroup = db.UserGroups.Find(ug.id);
                    ///
                    db.Users.Add(u);
                    db.SaveChanges();
                    return "ثبت اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "نام کاربری وارد شده تکراری می باشد";
                }
            }
            catch (Exception e)
            {

                return "ثبت اطلاعات با مشکلی مواجه شد\n"+e.Message;
            }
            
        }
        public bool IsRegistered()
        {
            return db.Users.Count() > 0;
        }
        public bool Read(User u)
        {
            var q = db.Users.Where(i => i.UserName == u.UserName);  
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
            string cmd = @"SELECT dbo.Users.id, dbo.Users.Name AS [نام و نام خانوادگی], dbo.Users.UserName AS [نام کاربری], dbo.Users.RegDate AS [تاریخ ثبت], dbo.UserGroups.Title AS [سطح کاربری] FROM dbo.Users INNER JOIN dbo.UserGroups ON dbo.Users.UserGroup_id = dbo.UserGroups.id WHERE (dbo.Users.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CRMToseGar;Integrated Security=true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public User Read (int id)
        {
            return db.Users.Where(i => i.id == id).FirstOrDefault();

        }
        public User ReadU(string s)
        {
            return db.Users.Where(i => i.UserName == s).SingleOrDefault();
        }
        public List<string> ReadUserNames()
        {
            return db.Users.Where(i => i.DeleteStatus == false).Select(i => i.UserName).ToList();
        }
        public string Update(User u,UserGroup ug, int id)
        {
            try
            {
                var q = db.Users.Where(i => i.id == id).FirstOrDefault();
                if (q != null)
                {
                    q.Name = u.Name;
                    q.UserName = u.UserName;
                    q.Password = u.Password;
                    q.UserGroup = db.UserGroups.Find(ug.id);
                    //q.Pic = u.Pic;
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "کاربر مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {
                return "ویرایش اطلاعات با مشکلی مواجه شد\n"+ e.Message;
                
            }
            
        }
        public string Delete(int id)
        {
            var q = db.Users.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    //db.Users.Remove(q);
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "کاربر مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public User Login(string u, string p)
        {
            return db.Users.Include("UserGroup").Where(i=>i.UserName == u && i.Password == p).SingleOrDefault();
        }
        public bool Access(User u,string s,int a)
        {
            UserGroup ug = db.UserGroups.Include("UserAccessRoles").Where(i => i.id == u.UserGroup.id).FirstOrDefault();
            UserAccessRole uar = ug.UserAccessRoles.Where(z => z.Section == s).FirstOrDefault();


            if (a == 1)
            {
                return uar.CanEnter;
            }
            else if (a == 2)
            {
                return uar.CanCreate;
            }
            else if (a == 3)
            {
                return uar.CanUpdate;
            }
            else 
            {
                return uar.CanDelete;
            }
        }
        public List<User> ReadInvoiceByUseer()
        {
            return db.Users.Include("Invoices").Where(i => i.DeleteStatus == false).ToList();
        }
    }
}
