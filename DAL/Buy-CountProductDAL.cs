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
    public class Buy_CountProductDAL
    {
        DB db = new DB();
        void GetCountProduct(int id)
        {
            var q = db.Buy_CountProducts.Include("counProductInAnbarB").Where(i => i.id == id).ToList();
            foreach (var item in q.ToList())
            {
                CounProductInAnbar p = db.CounProductInAnbars.Include("anbarCategoryP").Where(i => /*i.anbarCategoryP.AnbarName == item.anbarname &&*/ i.id == item.counProductInAnbarB.id).FirstOrDefault();
                p.count += item.count;
                db.SaveChanges();
            }
        }
        void GetCountProductMarjo(int id)
        {
            var q = db.Buy_CountProducts.Include("counProductInAnbarB").Where(i => i.id == id).ToList();
            foreach (var item in q.ToList())
            {
                CounProductInAnbar p = db.CounProductInAnbars.Include("anbarCategoryP").Where(i => /*i.anbarCategoryP.AnbarName == item.anbarname &&*/ i.id == item.counProductInAnbarB.id).FirstOrDefault();
                p.count -= item.count;
                db.SaveChanges();
            }
        }

        public List<Buy_CountProduct> GetCountProductsByBuyId(int BuyID)
        {
            IQueryable<Buy_CountProduct> query = db.Buy_CountProducts.Include("buyC").Where(i => i.buyC.id == BuyID);
            query = query.Include("productB");
            query = query.Include("counProductInAnbarB");
            return query.ToList();
        }

        public DataTable Read()
        {
            string cmd = "SELECT dbo.Buy_CountProduct.id, dbo.Buys.BuyNumber AS [شماره خرید], dbo.Products.Name AS [نام محصول], dbo.Buy_CountProduct.count AS تعداد, dbo.Buy_CountProduct.anbarname AS انبار, dbo.Buy_CountProduct.Marjo AS مرجوعی, dbo.Buy_CountProduct.MasolMarjo AS [مسئول مرجوع], dbo.Buy_CountProduct.DateMarjo AS [تاریخ مرجوع], dbo.Buy_CountProduct.SabtAnbar AS [ثبت ورود], dbo.Buy_CountProduct.MasolSabt AS [مسئول ثبت], dbo.Buy_CountProduct.DateSabt AS [تاریخ ثبت], dbo.CounProductInAnbars.count AS [تعداد کل همین محصول در این انبار], dbo.CounProductInAnbars.Kharab AS خرابی FROM dbo.Buys INNER JOIN dbo.Buy_CountProduct ON dbo.Buys.id = dbo.Buy_CountProduct.buyC_id INNER JOIN dbo.Products ON dbo.Buy_CountProduct.productB_id = dbo.Products.id INNER JOIN dbo.CounProductInAnbars ON dbo.Buy_CountProduct.counProductInAnbarB_id = dbo.CounProductInAnbars.id AND dbo.Products.id = dbo.CounProductInAnbars.productP_id WHERE (dbo.Products.DeleteStatus = 0) AND (dbo.Buys.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchBuyForMarjo");

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
        public string SabtVorodDone(User u, int id)
        {


            var q = db.Buy_CountProducts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.SabtAnbar != true)
                    {
                        q.SabtAnbar = true;
                        q.Marjo = false;
                        q.MasolSabt = u.Name;
                        q.DateSabt = MetodExtations.ToShamsi(DateTime.Now);
                        db.SaveChanges();
                        GetCountProduct(id);
                        return "پردازش اطلاعات با موفقیت انجام شد";
                    }
                    else
                    {
                        return "تیک ثبت انبار قبلا خورده شده است";
                    }
                }
                else
                {
                    return "محصول خرید مورد نظر یافت نشدم!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string MarjoDone(User u, int id)
        {


            var q = db.Buy_CountProducts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.SabtAnbar == true)
                    {
                        if (q.Marjo != true)
                        {
                            q.Marjo = true;
                            q.SabtAnbar = false;
                            q.MasolMarjo = u.Name;
                            q.DateMarjo = MetodExtations.ToShamsi(DateTime.Now);
                            db.SaveChanges();
                            GetCountProductMarjo(id);
                            return "پردازش اطلاعات با موفقیت انجام شد";
                        }
                        else
                        {
                            return "تیک مرجوع قبلا خورده شده است";
                        }
                    }
                    else
                    {
                        return "کالای مورد نظر هنوز ثبت ورود نشده است که بخواهد ثبت مرجوع شود";
                    }
                }
                else
                {
                    return "محصول خرید مورد نظر یافت نشدم!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
    }
}
