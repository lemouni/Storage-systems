using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class ReminderBLL
    {
        ReminderDAL dal = new ReminderDAL();
        public string Create(Reminder r , User u)
        { 
                return dal.Create(r ,u);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public Reminder Read(int id)
        {
            return dal.Read(id);
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public string Update(Reminder r, int id)
        {
            return dal.Update(r, id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public string Done(int id)
        {
            return dal.Done(id);
        }
        public string NotDone(int id)
        {
            return dal.NotDone(id);
        }
    }
}
