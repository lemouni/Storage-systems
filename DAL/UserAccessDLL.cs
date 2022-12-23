using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class UserAccessDLL
    {
        DB db = new DB();
        public string DoneEnter(int id)
        {
            var q = db.UserAccessRoles.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CanEnter = true;
                    db.SaveChanges();
                    return "دسترسی این سطح از گروه کاربری در بخش مورد نظر برای ورود آزاد شد";
                }
                else
                {
                    return "دسترسی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string NotDoneEnter(int id)
        {
            var q = db.UserAccessRoles.Include("UserGroup").Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CanEnter = false;
                    db.SaveChanges();
                    return "دسترسی این سطح از گروه کاربری در بخش مورد نظر برای ورود غیر فعال شد";
                }
                else
                {
                    return "دسترسی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string DoneCreate(int id)
        {
            var q = db.UserAccessRoles.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CanCreate = true;
                    db.SaveChanges();
                    return "دسترسی این سطح از گروه کاربری در بخش مورد نظر برای افزودن آزاد شد";
                }
                else
                {
                    return "دسترسی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string NotDoneCreate(int id)
        {
            var q = db.UserAccessRoles.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CanCreate = false;
                    db.SaveChanges();
                    return "دسترسی این سطح از گروه کاربری در بخش مورد نظر برای افزودن غیر فعال شد";
                }
                else
                {
                    return "دسترسی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string DoneUpdate(int id)
        {
            var q = db.UserAccessRoles.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CanUpdate = true;
                    db.SaveChanges();
                    return "دسترسی این سطح از گروه کاربری در بخش مورد نظر برای آپدیت آزاد شد";
                }
                else
                {
                    return "دسترسی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string NotDoneUpdate(int id)
        {
            var q = db.UserAccessRoles.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CanUpdate = false;
                    db.SaveChanges();
                    return "دسترسی این سطح از گروه کاربری در بخش مورد نظر برای آپدیت غیر فعال شد";
                }
                else
                {
                    return "دسترسی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string DoneDelete(int id)
        {
            var q = db.UserAccessRoles.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CanDelete = true;
                    db.SaveChanges();
                    return "دسترسی این سطح از گروه کاربری در بخش مورد نظر برای حذف آزاد شد";
                }
                else
                {
                    return "دسترسی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string NotDoneDelete(int id)
        {
            var q = db.UserAccessRoles.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.CanDelete = false;
                    db.SaveChanges();
                    return "دسترسی این سطح از گروه کاربری در بخش مورد نظر برای حذف غیر فعال شد";
                }
                else
                {
                    return "دسترسی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
    }
}
