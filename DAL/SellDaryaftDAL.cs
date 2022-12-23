using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using CRM_Utility;

namespace DAL
{
    public class SellDaryaftDAL
    {
        DB db = new DB();
        public List<SellDaryaft> GetCountDaryaftssByHesabId(int HesabID)
        {
            IQueryable<SellDaryaft> query = db.SellDaryafts.Include("HesabBank").Where(i => i.HesabBank.id == HesabID);
            query = query.Include("Invoice");
            query = query.Include("CategoriNoe");
            return query.ToList();
        }
        void GetCountHesab(int id)
        {
            //var q = db.BuyPardakhts.Include("NoePardakht").Where(i => i.id == id).ToList();
            //foreach (var item in q.ToList())
            //{
            //    NoePardakht p = db.NoePardakhts.Include("CategoriNoe").Where(i => /*i.anbarCategoryP.AnbarName == item.anbarname &&*/ i.id == item.NoePardakht.id).FirstOrDefault();
            //    p.MoJodi += item.Pardakhti;
            //    db.SaveChanges();
            //}
            var qq = db.SellDaryafts.Include("HesabBank").Include("Invoice").Where(i => i/*.Buy*/.id == id).ToList();
            foreach (var item in qq.ToList())
            {
                HesabBank p = db.HesabBanks.Where(i => i.id == item.HesabBank.id).FirstOrDefault();
                p.Stock += item.Daryafti;
                db.SaveChanges();
            }
        }
        public List<SellDaryaft> GetCountHesabsByBuyId(int InvoiceID)
        {
            IQueryable<SellDaryaft> query = db.SellDaryafts.Include("Invoice").Where(i => i.Invoice.id == InvoiceID);
            query = query.Include("HesabBank");
            //query = query.Include("NoePardakht");
            query = query.Include("CategoriNoe");
            return query.ToList();
        }
        public string Create(SellDaryaft sd, Invoice i, HesabBank hb, CategoriNoe cn)
        {
            try
            {
                sd.Invoice = db.Invoices.Find(i.id);
                sd.HesabBank = db.HesabBanks.Find(hb.id);
                sd.CategoriNoe = db.CategoriNoes.Find(cn.id);
                //bp.NoePardakht.CategoriNoe.name = bp.CategoriNoe.name;

                if (i.InvoiceNumber != null && hb.ShomareHesab != null && cn.name != null)
                {
                    db.SellDaryafts.Add(sd);
                    db.SaveChanges();
                    return "ثبت اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "شماره فاکتور یا شماره حساب یا نوع پرداخت در بانک اطلاعاتی یافت نشد";
                }
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;

            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT dbo.SellDaryafts.id, dbo.Invoices.InvoiceNumber AS [شماره فاکتور], dbo.HesabBanks.ShomareHesab AS [شماره حساب], dbo.SellDaryafts.Tozih AS توضیح, dbo.CategoriNoes.name AS [نوع پرداخت], dbo.SellDaryafts.Daryafti AS [مبلغ دریافت], dbo.SellDaryafts.DateDaryafti AS [تاریخ ثبت], dbo.SellDaryafts.DaryaftShod AS [وضعیت دریافت], dbo.SellDaryafts.MasolDaryaftShod AS [مسئول دریافت], dbo.SellDaryafts.DateDaryaftShod AS [تاریخ دریافت], dbo.SellDaryafts.Bargasht AS برگشتی, dbo.SellDaryafts.MasolBargasht AS [مسئول برگشتی], dbo.SellDaryafts.DateBargasht AS [تاریخ برگشتی] FROM dbo.SellDaryafts INNER JOIN dbo.Invoices ON dbo.SellDaryafts.Invoice_id = dbo.Invoices.id INNER JOIN dbo.HesabBanks ON dbo.SellDaryafts.HesabBank_id = dbo.HesabBanks.id INNER JOIN dbo.CategoriNoes ON dbo.SellDaryafts.CategoriNoe_id = dbo.CategoriNoes.id WHERE (dbo.HesabBanks.DeleteStatus = 0) AND (dbo.Invoices.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchSellDaryaft1");
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
        public SellDaryaft Read(int id)
        {
            return db.SellDaryafts.Include("Invoice").Include("HesabBank").Include("CategoriNoe").Where(i => i.id == id).FirstOrDefault();
        }
        public string Update(SellDaryaft sd, Invoice i, HesabBank hb, CategoriNoe cn, int id)
        {
            var q = db.SellDaryafts.Where(x => x.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Invoice = db.Invoices.Find(i.id);
                    q.HesabBank = db.HesabBanks.Find(hb.id);
                    q.CategoriNoe = db.CategoriNoes.Find(cn.id);
                    //q.NoePardakht.CategoriNoe.name = q.CategoriNoe.name;
                    q.Daryafti = sd.Daryafti;
                    q.DateDaryafti = sd.DateDaryafti;
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "دریافتی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string SabtDaryaftShod(User u, int id)
        {


            var q = db.SellDaryafts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.Bargasht == false)
                    {
                        if (q.DaryaftShod != true)
                        {
                            q.DaryaftShod = true;
                            q.Bargasht = false;
                            q.MasolDaryaftShod = u.Name;
                            q.DateDaryaftShod = MetodExtations.ToShamsi(DateTime.Now);
                            db.SaveChanges();
                            GetCountHesab(id);
                            return "پردازش اطلاعات با موفقیت انجام شد";
                        }
                        else
                        {
                            return "تیک دریافت شد خورده است";
                        }
                    }
                    else
                    {
                        return "چک مورد نظر برگشت خورده است";
                    }
                }
                else
                {
                    return "دریافتی مورد نظر یافت نشدم!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string BargashtDone(User u, int id)
        {


            var q = db.SellDaryafts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.DaryaftShod == false)
                    {
                        q.Bargasht = true;
                        q.MasolBargasht = u.Name;
                        q.DateBargasht = MetodExtations.ToShamsi(DateTime.Now);
                        db.SaveChanges();
                        return "پردازش اطلاعات با موفقیت انجام شد";
                    }
                    else
                    {
                        return "دریافتی مورد نظر ثبت شده است نمیتوانید برگشت بزنید";
                    }
                }
                else
                {
                    return "دریافتی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.SellDaryafts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.Bargasht==true)
                    {
                        db.SellDaryafts.Remove(q);
                        db.SaveChanges();
                        return "حذف اطلاعات با موفقیت انجام شد";
                    }
                    else
                    {
                        return "برای حذف ، نیاز هست دریافتی برگشت خورده باشد";
                    }
                }
                else
                {
                    return "دریافتی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public List<SellDaryaft> ReadBuyDaryaftNotChecked()
        {
            return db.SellDaryafts.Include("Invoice").Include("HesabBank").Include("CategoriNoe").Where(i => i.DaryaftShod == false).ToList();
        }
    }
}
