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
    public class SmsBLL
    {
        SmsDAL dal = new SmsDAL();
        public string Create(Sms s)
        {
            return dal.Create(s);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
    }
}
