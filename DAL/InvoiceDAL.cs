using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Windows;
using CRM_Utility;
using System.Data.Entity;

namespace DAL
{
    public class InvoiceDAL
    {
        DB db = new DB();
        void GetCountProduct(int id)
        {
            var q= db.CountProducts.Include("producC").Include("invoiceC").Where(i => i.invoiceC.id == id).ToList();
            foreach (var item in q.ToList())
            {
                Product p = db.Products.Where(i => i.id == item.producC.id).FirstOrDefault();
                p.Stock -= item.count;
                db.SaveChanges();
            }
        }
        void GetCountProductDelete(int id)
        {
            var q = db.CountProducts.Include("producC").Include("invoiceC").Where(i => i.invoiceC.id == id).ToList();
            foreach (var item in q.ToList())
            {
                Product p = db.Products.Where(i => i.id == item.producC.id).FirstOrDefault();
                p.Stock += item.count;
                db.SaveChanges();
            }
        }
        //void GetCountProductInAnbar(int id)
        //{
        //    var q = db.CountProducts.Include("producC").Include("invoiceC").Where(i => i.invoiceC.id == id).ToList();
        //    var qq = db.CounProductInAnbars.Where(i => i.productP.id == q.producC.id).ToList();
        //    foreach (var item in q.ToList())
        //    {

        //    }
        public void CheckPardakht()
        {

            foreach (var item in db.Invoices.Include("SellDaryafts").ToList())
            {
                var result = item.SellDaryafts.Any(x => !x.DaryaftShod);
                var total = item.SellDaryafts.Sum(x => x.Daryafti);
                if (!result)
                {
                    if (total == item.TotalCost)
                    {
                        item.IsCheckedOut = true;
                        db.SaveChanges();
                    }
                }
            }
        }

        public Invoice Create(Invoice i, Customer c,User u, List<CountProduct> cc)
        {
            try
            {
                i.Customer = db.Customers.Find(c.id);
                i.User = db.Users.Find(u.id);


                foreach (var item in cc)
                {
                    var product = db.Products.FirstOrDefault(x => x.id == item.producC.id);

                    var countproductinanbar = db.CounProductInAnbars.FirstOrDefault(x=> x.id == item.counProductInAnbar.id);

                    var CountProduct = new CountProduct
                    {

                        counProductInAnbar = countproductinanbar,
                        producC = product,
                        count = item.count, 
                        priceselect = item.priceselect,
                        Percentage = item.Percentage,
                        anbarname = item.anbarname,
                        
                    };
                    i.CountProducts.Add(CountProduct);
                }

                ///استفاد از رندوم برای invoicenumber
                Random rnd = new Random();
                string s = rnd.Next(1000000).ToString();
                ///برای غیر تکراری بودن رندوم 
                var q = db.Invoices.Where(z => z.InvoiceNumber == s);
                while (q.Count() > 0)
                {
                    s = rnd.Next(1000000).ToString();
                }
                i.InvoiceNumber = s;


                if (c.Name != null || c.Phone != null)
                {
                    db.Invoices.Add(i);
                    db.SaveChanges();
                    GetCountProduct(i.id);
                    //درست شد بسیار عالی منblj,k 'vlزه
                    //return "ثبت فاکتور با موفقیت انجام شد";
                    MessageBox.Show("ثبت فاکتور با موفقیت انجام شد");
                    return i;
                }
                else
                {
                    //return "شماره تماس مشتری یا نام مشتری مربوطه در بانک اطلاعاتی یافت نشد";
                    MessageBox.Show("شماره تماس مشتری یا نام مشتری مربوطه در بانک اطلاعاتی یافت نشد");
                    return new Invoice();
                }
            }
            catch (Exception e)
            {

                //return "ثبت فاکتور با مشکلی مواجه شد" + e.Message;
                MessageBox.Show("ثبت فاکتور با مشکلی مواجه شد" + e.Message);
                return new Invoice();
            }
         
            //foreach (var item in GetCountProduct(i.id))
            //{
            //    //تعداد از کجا میا؟
            //    Product p = db.Products.Where(pr=> pr.id == item.producC.id).FirstOrDefault();
            //    p.Stock -= item.count;
            //    //کم نشد
            //}
            //  db.SaveChanges();
        }
        public string ReadInvoiceNum()
        {
            //return db.Invoices.Select(i=>i.InvoiceNumber).Last();

            var q = db.Invoices.OrderByDescending(i => i.id).FirstOrDefault();
            return q.InvoiceNumber;
        }
        public string ReadInvoiceNumForid(int id)
        {

            var q = db.Invoices.Where(i => i.id==id).FirstOrDefault();
            return q.InvoiceNumber;
        }

        //برای سرچ توی ویو مدل
        //public List<"نام کلاس"> search (List<"نام کلاس"> lst , string ser)
        //{
        //    return lst.Where(i => i.name.contains(ser)).tolist();
        //}

        //روش زیر نمایش از طریق ویومدل
        public List<InvoiceViewModel> ReadViewModel()
        {
            List<InvoiceViewModel>lst = new List<InvoiceViewModel>();
            IQueryable<Invoice> query = db.Invoices.Include("Customer").Where(i=>i.DeleteStatus==false);
            query = query.Include("User");
            foreach (var item in query.ToList())
            {
                lst.Add(new InvoiceViewModel() { 
                id = item.id,
                CheckoutDate = item.CheckoutDate.ToString(),
                CustomerName = item.Customer.Name,
                CutomerPhone = item.Customer.Phone,
                DeleteStatus = item.DeleteStatus,
                InvoiceNumber = item.InvoiceNumber,
                IsCheckedOut = item.IsCheckedOut ,
                feepaid = item.FeePaid,
                totalcost = item.TotalCost,
                RegDate = item.RegDate ,
                nameuser = item.User.Name,
                });
            }
            return lst;
        }

        //نمایش از طریق ویو دیتا بیس
        public DataTable Read()
        {
            string cmd = "SELECT dbo.Invoices.id, dbo.Invoices.InvoiceNumber AS [شماره فاکتور], dbo.Invoices.RegDate AS [تاریخ ثبت فاکتور], dbo.Invoices.IsCheckedOut AS [وضعیت پرداخت], dbo.Invoices.CheckoutDate AS [تاریخ پرداخت فاکتور], dbo.Customers.Name AS [نام مشتری], dbo.Customers.Phone AS [شماره مشتری], dbo.Users.Name AS [نام کاربر] FROM dbo.Invoices INNER JOIN dbo.Customers ON dbo.Invoices.Customer_id = dbo.Customers.id INNER JOIN dbo.Users ON dbo.Invoices.User_id = dbo.Users.id WHERE (dbo.Invoices.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchInvoiceC");
            
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
        public List<InvoiceViewModel> SearchReadViewModel(string s)
        {
            List<InvoiceViewModel> lst = new List<InvoiceViewModel>();

            for (int i = 0; i < Read(s).Rows.Count; i++)
            {
                InvoiceViewModel st = new InvoiceViewModel();
                st.id = Convert.ToInt32(Read(s).Rows[i]["ایدی"]);
                //st.CheckoutDate = Convert.ToDateTime(MetodExtations.ToShamsi((DateTime)Read(s).Rows[i]["تاریخ پرداخت"]));
                st.CheckoutDate = Read(s).Rows[i]["تاریخ پرداخت"].ToString();

                st.CustomerName = Read(s).Rows[i]["نام مشتری"].ToString();
                st.CutomerPhone = Read(s).Rows[i]["شماره تماس مشتری"].ToString();
                st.DeleteStatus = (bool)Read(s).Rows[i]["دلیت"];
                st.InvoiceNumber = Read(s).Rows[i]["شماره فاکتور"].ToString();
                st.IsCheckedOut = (bool)Read(s).Rows[i]["وضعیت پرداخت"];
                st.feepaid = Convert.ToDouble(Read(s).Rows[i]["مبلغ پرداخت شده"]);
                st.totalcost = Convert.ToDouble(Read(s).Rows[i]["مبلغ کل"]);
                //st.RegDate = Convert.ToDateTime(MetodExtations.ToShamsi((DateTime)Read(s).Rows[i]["تاریخ ثبت فاکتور"]));
                st.RegDate = Convert.ToDateTime(Read(s).Rows[i]["تاریخ ثبت فاکتور"]);

                st.nameuser = Read(s).Rows[i]["کاربر"].ToString();
                lst.Add(st);
            }
            return lst;


            //foreach (var item in Read(s).Rows)
            //{
            //    lst.Add(new InvoiceViewModel()
            //    {
            //        id = item["ایدی"],
            //        CheckoutDate = item["تاریخ پرداخت"],
            //        CustomerName = item["نام مشتری"],
            //        CutomerPhone = item["شماره تماس مشتری"],
            //        DeleteStatus = item["دلیت"],
            //        InvoiceNumber = item["شماره فاکتور"],
            //        IsCheckedOut = item["وضعیت پرداخت"],
            //        feepaid = item["مبلغ پرداخت شده"],
            //        totalcost = item["مبلغ کل"],
            //        RegDate = item["تاریخ ثبت فاکتور"],
            //        nameuser = item["کاربر"],
            //    });
            //}
            //return lst;
        }
        public Invoice Read(int id)
        {
            return db.Invoices.Where(i => i.id == id).FirstOrDefault();
        }
        public Invoice ReadP(int id)
        {
            return db.Invoices.Include("CountProducts").Include("Products").Where(i => i.id == id).FirstOrDefault();
        }
        public string Delete(int id)
        {
            try
            {
                var q = db.Invoices.Where(i => i.id == id).SingleOrDefault();
                if (q != null)
                {
                    if (q.IsCheckedOut!=true)
                    {



                        var invoice = db.Invoices.Include("SellDaryafts").Include("CountProducts").FirstOrDefault(x => x.id == id);
                        var result = invoice.SellDaryafts.ToList();
                        var resault1 = invoice.CountProducts.Any(x => !x.Marjoei);
                        if (result.Count() == 0 && !resault1)
                        {
                            q.DeleteStatus = true;
                            db.SaveChanges();
                            GetCountProductDelete(id);
                            return "حذف فاکتور با موفقیت انجام شد";
                        }
                        else
                        {
                            return "فاکتور مورد نظر دارای دریافتی یا ثبت خروج کالا می باشد";
                        }




                    }
                    else
                    {
                        return "فاکتور پرداخت کامل شده است";
                    }
                }
                else
                {
                    return "فاکتور مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "حذف فاکتور با مشکلی مواجه شد:\n" + e.Message;
            }
            
        }
        public string Done(int id)
        {
            var q = db.Invoices.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsCheckedOut = true;
                    q.CheckoutDate = Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Now));
                    q.FeePaid = q.TotalCost;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "فاکتور مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string NotDone(int id)
        {
            var q = db.Invoices.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsCheckedOut = false;
                    q.CheckoutDate = null;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "فاکتور مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        ///زیر برای گرفتن ادرس در فرم فاکتور
        public string ReadCustomeradrees(int id)
        {
            var q = db.Invoices.Include("Customer").Where(i => i.id == id).FirstOrDefault();
            return q.Customer.Adress;
        }
        public string ReadCustomercodpost(int id)
        {
            var q = db.Invoices.Include("Customer").Where(i => i.id == id).FirstOrDefault();
            return q.Customer.CodePost;
        }
        public string ReadCustomerName(int id)
        {
            var q = db.Invoices.Include("Customer").Where(i => i.id == id).FirstOrDefault();
            return q.Customer.Name;
        }
        public string ReadCustomerPhone(int id)
        {
            var q = db.Invoices.Include("Customer").Where(i => i.id == id).FirstOrDefault();
            return q.Customer.Phone;
        }
        public string ReadDate(int id)
        {
            var q = db.Invoices.Where(i => i.id == id).FirstOrDefault();
            return q.RegDate.ToString();
        }
        public Invoice Readd(string p)
        {
            return db.Invoices.Where(i => i.InvoiceNumber == p && i.DeleteStatus == false).SingleOrDefault();
        }
        public List<string> ReadNamesANC()
        {
            return db.Invoices.Where(i => i.DeleteStatus == false).Select(i => i.InvoiceNumber).ToList();
        }
        public List<Invoice> ReadInv(DateTime strat, DateTime end)
        {
            return db.Invoices.Include("Customer").Where(i => i.RegDate > strat && i.RegDate < end && i.DeleteStatus == false).ToList();
        }
        public List<Invoice> ReadInvNotChekedOut(DateTime strat, DateTime end)
        {
            return db.Invoices.Include("Customer").Where(i => i.RegDate > strat && i.RegDate < end && i.DeleteStatus == false && i.IsCheckedOut==false).ToList();
        }
    }
}
