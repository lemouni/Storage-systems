using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using CRM_Utility;

namespace DAL
{
    public class DashbordDAL
    {
        DB db = new DB();
        public string UserRemindersCount(User u)
        {
            User q = db.Users.Include("Reminders").Where(i => i.id == u.id && i.DeleteStatus==false).FirstOrDefault();
            return q.Reminders.Where(i=>i.DeleteStatus==false && i.RemindDate == DateTime.Today).Count().ToString();
        }
        public string CustomersCount()
        {
            return db.Customers.Where(i=>i.DeleteStatus==false).Count().ToString();
        }
        public string SellsCount()
        {
            int sum = 0;
            foreach (var item in db.Invoices)
            {
                if (item.RegDate.Date ==Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Today)) && item.DeleteStatus==false)
                {
                    sum = sum + 1; 
                }
            }
            return sum.ToString();
        }
        public List<Reminder> GetUserReminders(User u)
        {
            return db.Reminders.Include("Users").Where(i => i.Users.id == u.id && i.DeleteStatus== false && i.RemindDate == DateTime.Today).ToList();
        }
    }

}
