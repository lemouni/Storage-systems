using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BLL
{
    public class MoshkhasatFactorBLL
    {
        MoshkhasatFactorDAL dal = new MoshkhasatFactorDAL();
        public string Create(MoshkhasatFactor m)
        {
            return dal.Create(m);
        }
        public string ReadNameForosghah()
        {
            return dal.ReadNameForosghah();
        }
    }
}
