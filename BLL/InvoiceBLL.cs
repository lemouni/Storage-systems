using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;
using System.Data;

namespace BLL
{
    public class InvoiceBLL
    {
        InvoiceDAL dal = new InvoiceDAL();
        public void CheckPardakht()
        {
            dal.CheckPardakht();
        }
        public Invoice Create(Invoice i, Customer c,User u,List<CountProduct> cc)
        { 
            return dal.Create(i, c,u ,cc);
        }
        public List<InvoiceViewModel>ReadViewModel()
        {
            return dal.ReadViewModel();
        }
        public string ReadInvoiceNum()
        {
            return dal.ReadInvoiceNum();
        }
        public string ReadInvoiceNumForid(int id)
        {
            return dal.ReadInvoiceNumForid(id);
        }

        public DataTable Read()
        {
            return dal.Read();
        }
        public DataTable Read(string s)
        {
            return dal.Read(s);
        }
        public List<InvoiceViewModel> SearchReadViewModel(string s)
        {
            return dal.SearchReadViewModel(s);
        }
        public Invoice Read(int id)
        {
            return dal.Read(id);
        }
        public Invoice ReadP(int id)
        {
            return dal.ReadP(id);
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
        public string ReadCustomeradrees(int id)
        {
            return dal.ReadCustomeradrees(id);
        }
        public string ReadCustomercodpost(int id)
        {
            return dal.ReadCustomercodpost(id);
        }
        public string ReadCustomerName(int id)
        {
            return dal.ReadCustomerName(id);
        }
        public string ReadCustomerPhone(int id)
        {
            return dal.ReadCustomerPhone(id);
        }
        public string ReadDate(int id)
        {
            return dal.ReadDate(id);
        }
        public Invoice Readd(string p)
        {
            return dal.Readd(p);
        }
        public List<string> ReadNamesANC()
        {
            return dal.ReadNamesANC();
        }
        public List<Invoice> ReadInv(DateTime strat, DateTime end)
        {
            return dal.ReadInv(strat,end);
        }
        public List<Invoice> ReadInvNotChekedOut(DateTime strat, DateTime end)
        {
            return dal.ReadInvNotChekedOut(strat, end);
        }
    }
}
