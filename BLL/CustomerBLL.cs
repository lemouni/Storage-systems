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
    public class CustomerBLL
    {
        CustomerDAL dal = new CustomerDAL();
        public string Create(Customer c)
        {
            if (dal.Read(c))
            {
                return dal.Create(c);
            }
            else
            {
                return "کاربری با همین شماره تماس در سیستم ثبت شده است";
            }
        }
        public Customer Read(int id)
        {
            return dal.Read(id);
        }

        public DataTable Read(string s, int index)
        {
            return dal.Read(s, index);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public string Update(Customer c, int id)
        {
            return dal.Update(c, id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }

        public List<string> ReadPhoneNumbers()
        {
            return dal.ReadPhoneNumbers();
        }
        public List<string> ReadCustName()
        {
            return dal.ReadCustName();
        }
        public Customer Read(string p)
        {
            return dal.Read(p);
        }
        public Customer ReadCN(string p)
        {
            return dal.ReadCN(p);
        }



        public List<Customer> ReadCust(DateTime strat, DateTime end)
        {
            return dal.ReadCust(strat, end);
        }
        public List<Customer> ReadInvoiceByCustomer()
        {
            return dal.ReadInvoiceByCustomer();
        }
    }
}
