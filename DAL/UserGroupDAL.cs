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
    public class UserGroupDAL
    {
        DB db = new DB();
        public string Create(UserGroup ug)
        {
            try
            {
                if (Read(ug.Title))
                {
                    db.UserGroups.Add(ug);
                    db.SaveChanges();
                    return "ثبت گروه کاربری با موفقیت انجام شد. بعد از وارد کردن گروه های کاربری مورد نظر یک بار از این فرم خارج شوید سپس مجددا وارد شوید";
                }
                else
                {
                    return "گروه کاربری دیگری با این نام وجود دارد";
                }
            }
            catch (Exception e)
            {

                return "در ثبت گروه کاربری مشکلی به وجود آمده" + e.Message;
            }
        }
        public bool Read(string Name)
        {
            return !db.UserGroups.Any(i => i.Title == Name);
        }
        public List<string> ReadUGNames()
        {
            return db.UserGroups.Select(i=>i.Title).ToList();
        }
        public UserGroup ReadN(string n)
        {
            return db.UserGroups.Where(i => i.Title == n).SingleOrDefault();
        }
        public DataTable Read()
        {
            //string cmd = "SELECT dbo.UserGroups.id, dbo.UserGroups.Title AS [نام گروه کاربری], dbo.UserAccessRoles.Section AS [مکان مورد نظر], dbo.UserAccessRoles.CanEnter AS [اجازه ورود], dbo.UserAccessRoles.CanCreate AS [اجازه افزودن], dbo.UserAccessRoles.CanUpdate AS [اجازه اپدیت], dbo.UserAccessRoles.CanDelete AS [اجازه دیلیت] FROM dbo.UserGroups INNER JOIN dbo.UserAccessRoles ON dbo.UserGroups.id = dbo.UserAccessRoles.UserGroup_id";
            string cmd = "SELECT dbo.UserAccessRoles.id, dbo.UserGroups.Title AS [گروه کاربری], dbo.UserAccessRoles.Section AS [محل مربوطه], dbo.UserAccessRoles.CanEnter AS [دسترسی ورود], dbo.UserAccessRoles.CanCreate AS [دسترسی افزودن], dbo.UserAccessRoles.CanUpdate AS [دسترسی آپدیت], dbo.UserAccessRoles.CanDelete AS [دسترسی حذف] FROM dbo.UserGroups INNER JOIN dbo.UserAccessRoles ON dbo.UserGroups.id = dbo.UserAccessRoles.UserGroup_id";

            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }

    }
}
