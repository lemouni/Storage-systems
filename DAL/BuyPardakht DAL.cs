using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BE;
using CRM_Utility;
using System.Data.Entity;

namespace DAL
{
    public class BuyPardakht_DAL
    {
        DB db = new DB();
        void GetCountHesab(int id)
        {
            //var q = db.BuyPardakhts.Include("NoePardakht").Where(i => i.id == id).ToList();
            //foreach (var item in q.ToList())
            //{
            //    NoePardakht p = db.NoePardakhts.Include("CategoriNoe").Where(i => /*i.anbarCategoryP.AnbarName == item.anbarname &&*/ i.id == item.NoePardakht.id).FirstOrDefault();
            //    p.MoJodi += item.Pardakhti;
            //    db.SaveChanges();
            //}
            var qq = db.BuyPardakhts.Include("HesabBank").Include("Buy").Where(i => i/*.Buy*/.id == id).ToList();
            foreach (var item in qq.ToList())
            {
                HesabBank p = db.HesabBanks.Where(i => i.id == item.HesabBank.id).FirstOrDefault();
                p.Stock -= item.Pardakhti;
                db.SaveChanges();
            }
        }
        public List<BuyPardakht> GetCountHesabsByBuyId(int BuyID)
        {
            IQueryable<BuyPardakht> query = db.BuyPardakhts.Include("Buy").Where(i => i.Buy.id == BuyID);
            query = query.Include("HesabBank");
            //query = query.Include("NoePardakht");
            query = query.Include("CategoriNoe");
            return query.ToList();
        }
        public List<BuyPardakht> GetCountPardakhtssByHesabId(int HesabID)
        {
            IQueryable<BuyPardakht> query = db.BuyPardakhts.Include("HesabBank").Where(i => i.HesabBank.id == HesabID);
            query = query.Include("Buy");
            query = query.Include("CategoriNoe");
            return query.ToList();
        }
        public string Create(BuyPardakht bp, Buy b, HesabBank hb, CategoriNoe cn)
        {
            try
            {
                bp.Buy = db.Buys.Find(b.id);
                bp.HesabBank = db.HesabBanks.Find(hb.id);
                bp.CategoriNoe = db.CategoriNoes.Find(cn.id);
                //bp.NoePardakht.CategoriNoe.name = bp.CategoriNoe.name;

                if (b.BuyNumber != null && hb.ShomareHesab!=null && cn.name!=null)
                {
                    db.BuyPardakhts.Add(bp);
                    db.SaveChanges();
                    return "ثبت اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "شماره خرید یا شماره حساب یا نوع پرداخت در بانک اطلاعاتی یافت نشد";
                }
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;

            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT dbo.BuyPardakhts.id, dbo.Buys.BuyNumber AS [شماره خرید], dbo.HesabBanks.ShomareHesab AS [شماره حساب], dbo.BuyPardakhts.Tozih AS توضیح, dbo.CategoriNoes.name AS [نوع پرداخت], dbo.BuyPardakhts.Pardakhti AS [مبلغ پرداخت], dbo.BuyPardakhts.DatePardakht AS [تاریخ ثبت], dbo.BuyPardakhts.PardakhtShod AS [وضعیت پرداخت], dbo.BuyPardakhts.MasolPardakhtShod AS [مسئول پرداخت], dbo.BuyPardakhts.DatePardakhtShod AS [تاریخ پرداخت], dbo.BuyPardakhts.Bargasht AS برگشتی, dbo.BuyPardakhts.MasolBargasht AS [مسئول برگشتی], dbo.BuyPardakhts.DateBargasht AS [تاریخ برگشتی] FROM dbo.BuyPardakhts INNER JOIN dbo.Buys ON dbo.BuyPardakhts.Buy_id = dbo.Buys.id INNER JOIN dbo.HesabBanks ON dbo.BuyPardakhts.HesabBank_id = dbo.HesabBanks.id INNER JOIN dbo.CategoriNoes ON dbo.BuyPardakhts.CategoriNoe_id = dbo.CategoriNoes.id WHERE (dbo.Buys.DeleteStatus = 0) AND (dbo.HesabBanks.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchBuyPardakht1");
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
        public BuyPardakht Read(int id)
        {
            return db.BuyPardakhts.Include("Buy").Include("HesabBank").Include("CategoriNoe").Where(i => i.id == id).FirstOrDefault();
        }
        public string Update(BuyPardakht bp, Buy b,HesabBank hb,CategoriNoe cn, int id)
        {
            var q = db.BuyPardakhts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Buy = db.Buys.Find(b.id);
                    q.HesabBank = db.HesabBanks.Find(hb.id);
                    q.CategoriNoe = db.CategoriNoes.Find(cn.id);
                    //q.NoePardakht.CategoriNoe.name = q.CategoriNoe.name;
                    q.Pardakhti = bp.Pardakhti;
                    q.DatePardakht = bp.DatePardakht;
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "پرداختی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string SabtPardakhtShod(User u, int id)
        {


            var q = db.BuyPardakhts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.Bargasht ==false)
                    {
                        if (q.PardakhtShod != true)
                        {
                            q.PardakhtShod = true;
                            q.Bargasht = false;
                            q.MasolPardakhtShod = u.Name;
                            q.DatePardakhtShod = MetodExtations.ToShamsi(DateTime.Now);
                            db.SaveChanges();
                            GetCountHesab(id);
                            return "پردازش اطلاعات با موفقیت انجام شد";
                        }
                        else
                        {
                            return "تیک پرداخت شد خورده است";
                        }
                    }
                    else
                    {
                        return "چک مورد نظر برگشت خورده است";
                    }
                }
                else
                {
                    return "پرداختی مورد نظر یافت نشدم!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string BargashtDone(User u, int id)
        {


            var q = db.BuyPardakhts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.PardakhtShod == false)
                    {
                        q.Bargasht = true;
                        q.MasolBargasht = u.Name;
                        q.DateBargasht = MetodExtations.ToShamsi(DateTime.Now);
                        db.SaveChanges();
                        return "پردازش اطلاعات با موفقیت انجام شد";
                    }
                    else
                    {
                        return "پرداختی مورد نظر ثبت پرداخت شده است و نمیتوانید برگشت بزنید";
                    }
                }
                else
                {
                    return "پرداختی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string Delete(int id)
        {
            var q = db.BuyPardakhts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.Bargasht==true)
                    {
                        db.BuyPardakhts.Remove(q);
                        db.SaveChanges();
                        return "حذف اطلاعات با موفقیت انجام شد";
                    }
                    else
                    {
                        return "برای حذف، نیاز هست پرداختی برگشت خورده باشد";
                    }
                }
                else
                {
                    return "پرداختی مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public List<BuyPardakht> ReadBuyPadakhtNotChecked()
        {
            return db.BuyPardakhts.Include("Buy").Include("HesabBank").Include("CategoriNoe").Where(i => i.PardakhtShod == false).ToList();
        }

    }
}
