using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class UserAccessBLL
    {
        UserAccessDLL dal = new UserAccessDLL();
        public string DoneEnter(int id)
        {
            return dal.DoneEnter(id);
        }
        public string NotDoneEnter(int id)
        {
            return dal.NotDoneEnter(id);
        }
        public string DoneCreate(int id)
        {
            return dal.DoneCreate(id);
        }
        public string NotDoneCreate(int id)
        {
            return dal.NotDoneCreate(id);
        }
        public string DoneUpdate(int id)
        {
            return dal.DoneUpdate(id);
        }
        public string NotDoneUpdate(int id)
        {
            return dal.NotDoneUpdate(id);
        }
        public string DoneDelete(int id)
        {
            return dal.DoneDelete(id);
        }
        public string NotDoneDelete(int id)
        {
            return dal.NotDoneDelete(id);
        }
    }
}
