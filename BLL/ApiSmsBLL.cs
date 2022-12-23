using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class ApiSmsBLL
    {
        ApiSmsDAL dal = new ApiSmsDAL();
        public string Create(ApiSms a)
        {
            return dal.Create(a);
        }
        public string ReadApiKay()
        {
            return dal.ReadApiKay();
        }
        public string ReadSecretKay()
        {
            return dal.ReadSecretKay();
        }
        public string ReadKhat()
        {
            return dal.ReadKhat();
        }
    }
}
