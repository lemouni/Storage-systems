using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BE;
using CRM_Utility;

namespace DAL
{
    public class BuyDAL
    {

        DB db = new DB();
        void GetCountProductB(int id)
        {
            var q = db.Buy_CountProducts.Include("productB").Include("buyC").Where(i => i.buyC.id == id).ToList();
            foreach (var item in q.ToList())
            {
                Product p = db.Products.Where(i => i.id == item.productB.id).FirstOrDefault();
                p.Stock += item.count;
                db.SaveChanges();
            }
        }
        void GetCountProductBDelete(int id)
        {
            var q = db.Buy_CountProducts.Include("productB").Include("buyC").Where(i => i.buyC.id == id).ToList();
            foreach (var item in q.ToList())
            {
                Product p = db.Products.Where(i => i.id == item.productB.id).FirstOrDefault();
                p.Stock -= item.count;
                db.SaveChanges();
            }
        }
        public void CheckPardakht()
        {

            foreach (var item in db.Buys.Include("BuyPardakhts").ToList())
            {
                var result = item.BuyPardakhts.Any(x => !x.PardakhtShod);
                var total = item.BuyPardakhts.Sum(x => x.Pardakhti);
                if (!result)
                {
                    if (total == item.TotalCostB)
                    {
                        item.IsCheckOut = true;
                        db.SaveChanges();
                    }
                }
            }
        }
        public Buy Create(Buy bi, User u, List<Buy_CountProduct> bcc)
        {
            try
            {
                bi.User = db.Users.Find(u.id);


                foreach (var item in bcc)
                {
                    var productb = db.Products.FirstOrDefault(x => x.id == item.productB.id);

                    var countproductinanbarb = db.CounProductInAnbars.FirstOrDefault(x => x.id == item.counProductInAnbarB.id);

                    var CountProductb = new Buy_CountProduct
                    {

                        counProductInAnbarB = countproductinanbarb,
                        productB = productb,
                        count = item.count,
                        anbarname = item.anbarname,
                        price = item.price,
                        
                        

                    };
                    bi.Buy_CountProducts.Add(CountProductb);
                }

                ///استفاد از رندوم برای invoicenumber
                Random rnd = new Random();
                string s = rnd.Next(1000000).ToString();
                ///برای غیر تکراری بودن رندوم 
                var q = db.Buys.Where(z => z.BuyNumber == s);
                while (q.Count() > 0)
                {
                    s = rnd.Next(1000000).ToString();
                }
                bi.BuyNumber = s;

                db.Buys.Add(bi);
                db.SaveChanges();
                GetCountProductB(bi.id);
                //برای کم کردن تعداد یادت نره فعلش در بالا


                MessageBox.Show("ثبت خرید با موفقیت انجام شد");
                return bi;
                //return "ثبت خرید با موفقیت انجام شد";

            }
            catch (Exception e)
            {

                MessageBox.Show("ثبت خرید با مشکلی مواجه شد" + e.Message);
                return new Buy();
                //return "ثبت خرید با مشکلی مواجه شد" + e.Message;
            }
        }
        public string ReadBuyNum()
        {
            var q = db.Buys.OrderByDescending(i => i.id).FirstOrDefault();
            return q.BuyNumber;
        }
        public List<BuyViewModel> ReadViewModel()
        {
            List<BuyViewModel> lst = new List<BuyViewModel>();
            IQueryable<Buy> query = db.Buys.Include("User").Where(i => i.DeleteStatus == false);
            //query = query.Include("User");
            foreach (var item in query.ToList())
            {
                lst.Add(new BuyViewModel()
                {
                    id = item.id,
                    CheckoutDate = item.CheckOutDate.ToString(),
                    TypeB=item.Type,
                    Titleb = item.Title,
                    ToziB = item.Tozih,
                    DeleteStatus = item.DeleteStatus,
                    BuyNumber = item.BuyNumber,
                    IsCheckedOut = item.IsCheckOut,
                    feepaidB = item.FeepaidB,
                    totalcostB = item.TotalCostB,
                    RegDate = item.RegDate,
                    nameuserB = item.User.Name,
                });
            }
            return lst;
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchBuyC");

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
        public List<BuyViewModel> SearchReadViewModel(string s)
        {
            List<BuyViewModel> lst = new List<BuyViewModel>();

            for (int i = 0; i < Read(s).Rows.Count; i++)
            {
                BuyViewModel st = new BuyViewModel();
                st.id = Convert.ToInt32(Read(s).Rows[i]["ایدی"]);
                st.CheckoutDate = Read(s).Rows[i]["تاریخ پرداخت"].ToString();
                st.TypeB = Read(s).Rows[i]["نوع خرید"].ToString();
                st.Titleb = Read(s).Rows[i]["موضوع"].ToString();
                st.ToziB = Read(s).Rows[i]["توضیح"].ToString();
                st.DeleteStatus= (bool)Read(s).Rows[i]["دلیت"];
                st.BuyNumber = Read(s).Rows[i]["شماره خرید"].ToString();
                st.IsCheckedOut = (bool)Read(s).Rows[i]["وضعیت پرداخت"];
                st.feepaidB = Convert.ToDouble(Read(s).Rows[i]["میزان پرداخت"]);
                st.totalcostB = Convert.ToDouble(Read(s).Rows[i]["جمع خرید"]);
                st.RegDate = Convert.ToDateTime(Read(s).Rows[i]["تاریخ خرید"]);
                st.nameuserB = Read(s).Rows[i]["نام کاربر"].ToString();
                lst.Add(st);
            }
            return lst;
        }
        public Buy Read(int id)
        {
            return db.Buys.Where(i => i.id == id).FirstOrDefault();
        }
        public Buy ReadP(int id)
        {
            return db.Buys.Include("Buy_CountProducts").Include("ProductsB").Where(i => i.id == id).FirstOrDefault();
        }
        public string Delete(int id)
        {
            try
            {
                var q = db.Buys.Where(i => i.id == id).SingleOrDefault();
                if (q != null)
                {
                    if (q.IsCheckOut != true)
                    {




                        var buy = db.Buys.Include("BuyPardakhts").Include("Buy_CountProducts").FirstOrDefault(x => x.id == id);
                        var result = buy.BuyPardakhts.ToList();
                        var resault1 = buy.Buy_CountProducts.Any(x=> !x.Marjo);
                        if (result.Count()==0 && !resault1)
                        {
                            q.DeleteStatus = true;
                            db.SaveChanges();
                            GetCountProductBDelete(id);
                            return "حذف خرید با موفقیت انجام شد";
                        }
                        else
                        {
                            return "خرید مورد نظر دارای پرداختی یا ثبت ورود کالا می باشد";
                        }




                    }
                    else
                    {
                        return "خرید پرداخت کامل شده است";
                    }
                }
                else
                {
                    return "خرید مورد نظر یافت نشد";
                }
            }
            catch (Exception e)
            {

                return "حذف خرید با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string Done(int id)
        {
            var q = db.Buys.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsCheckOut = true;
                    q.CheckOutDate = Convert.ToDateTime(MetodExtations.ToShamsi(DateTime.Now));
                    q.FeepaidB = q.TotalCostB;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "خرید مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string NotDone(int id)
        {
            var q = db.Buys.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.IsCheckOut = false;
                    q.CheckOutDate = null;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "خرید مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public Buy Readd(string p)
        {
            return db.Buys.Where(i => i.BuyNumber == p && i.DeleteStatus == false).SingleOrDefault();
        }
        public List<string> ReadNamesANC()
        {
            return db.Buys.Where(i => i.DeleteStatus == false).Select(i => i.BuyNumber).ToList();
        }
        public List<Buy> ReadBuy(DateTime strat, DateTime end)
        {
            return db.Buys.Where(i => i.RegDate > strat && i.RegDate < end && i.DeleteStatus == false).ToList();
        }
    }
}
