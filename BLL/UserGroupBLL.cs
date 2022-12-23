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
    public class UserGroupBLL
    {
        UserGroupDAL dal = new UserGroupDAL();
        public string Create(UserGroup ug)
        {
            return dal.Create(ug);
        }
        public bool Read(string Name)
        {
            return dal.Read(Name);
        }
        public List<string> ReadUGNames()
        {
            return dal.ReadUGNames();
        }
        public UserGroup ReadN(string n)
        {
            return dal.ReadN(n);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
    }
}
