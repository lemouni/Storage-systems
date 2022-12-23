using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BE;
using System.Data;

namespace DAL
{
    public class CustomerDAL
    {
        DB db = new DB();
        public string Create(Customer c)
        {
            try
            {
                db.Customers.Add(c);
                db.SaveChanges();
                return "ثبت اطلاعات مشتری با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public bool Read(Customer c)
        {
            var q = db.Customers.Where(i => c.Phone == i.Phone && i.DeleteStatus== false);
            if (q.Count() == 0)
            { 
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT id, Name AS [نام مشتری], Phone AS [شماره مشتری], AccountGroup AS [گروه حساب], CodeMeli AS [کد ملی], CodeEghtesadi AS [کد اقتصادی], Adress AS [آدرس مشتری], CodePost AS [کد پستی], CreditWithoutDocuments AS [اعتبار بدون سند], TotalCreditWithDocument AS [اعتبار کل با سند], RegDate AS [تاریخ ثبت] FROM dbo.Customers WHERE (DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s, int index)
        {
            SqlCommand cmd = new SqlCommand();
            if (index == 0)
            {
                cmd.CommandText = "dbo.SearchCustomer";

            }
            else if (index ==1)
            {
                cmd.CommandText = "dbo.SearchCustomerName";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchCustomerPhone";
            }
            else if (index == 3)
            {
                cmd.CommandText = "dbo.SearchCustomerAccountGroup11";
            }
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            cmd.Parameters.AddWithValue("@search", s);
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            //string cmd = "exec dbo.SearchCustomer";
            var sqladapter = new SqlDataAdapter();
            sqladapter.SelectCommand = cmd;
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public Customer Read (int id)
        {
            return db.Customers.Where(i => i.id == id).FirstOrDefault();
        }
        public string Update(Customer c ,int id)
        {
            var q = db.Customers.Where(i => i.id == id).FirstOrDefault();
            try
            {

                if (db.Customers.Any(i => i.Name == c.Name && i.DeleteStatus==false)&& q.Name!= c.Name)
                {
                    return "نام تکراری می باشد . و شماره تماس قابل ویرایش نیس!!";
                }
                else
                {
                    if (q != null)
                    {
                        q.Name = c.Name;
                        q.Phone = c.Phone;
                        q.AccountGroup = c.AccountGroup;
                        q.CodeMeli = c.CodeMeli;
                        q.CodeEghtesadi = c.CodeEghtesadi;
                        q.Adress = c.Adress;
                        q.CodePost = c.CodePost;
                        q.CreditWithoutDocuments = c.CreditWithoutDocuments;
                        q.TotalCreditWithDocument = c.TotalCreditWithDocument;

                        db.SaveChanges();
                        return "ویرایش اطلاعات با موفقیت انجام شد";
                    }
                    else
                    {
                        return "مشتری مورد نظر یافت نشد!";
                    }
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
            
        }
        public string Delete(int id)
        {
            var q = db.Customers.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "مشتری مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }

        public List<string> ReadPhoneNumbers()
        {
            return db.Customers.Where(i => i.DeleteStatus == false).Select(i => i.Phone).ToList();
        }
        public List<string> ReadCustName()
        {
            return db.Customers.Where(i => i.DeleteStatus == false).Select(i => i.Name).ToList();
        }
        public Customer Read (string p)
        {
            return db.Customers.Where(i=>i.Phone == p).SingleOrDefault();
        }
        public Customer ReadCN(string p)
        {
            return db.Customers.Where(i => i.Name == p && i.DeleteStatus==false).SingleOrDefault();
        }


        public List<Customer> ReadCust(DateTime strat , DateTime end)
        {
            return db.Customers.Where(i => i.RegDate > strat && i.RegDate < end).ToList();
        }
        public List<Customer> ReadInvoiceByCustomer()
        {
            return db.Customers.Include("Invoices").Where(i => i.DeleteStatus == false).ToList();
        }
    }
}


