using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class DashbordBLL
    {
        DashbordDAL dal = new DashbordDAL();
        public string UserRemindersCount(User u)
        {
            return dal.UserRemindersCount(u);
        }
        public string CustomersCount()
        {
            return dal.CustomersCount();
        }
        public string SellsCount()
        {
            return dal.SellsCount();
        }
        public List<Reminder> GetUserReminders(User u)
        {
            return dal.GetUserReminders(u);
        }
    }
}
