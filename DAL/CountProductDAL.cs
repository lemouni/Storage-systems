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
    public class CountProductDAL
    {
        DB db = new DB();

        void GetCountProduct(int id)
        {
            var q = db.CountProducts.Include("counProductInAnbar").Where(i => i.id == id).ToList();
            foreach (var item in q.ToList())
            {
                CounProductInAnbar p = db.CounProductInAnbars.Include("anbarCategoryP").Where(i => /*i.anbarCategoryP.AnbarName == item.anbarname &&*/ i.id == item.counProductInAnbar.id).FirstOrDefault();
                p.count -= item.count;
                db.SaveChanges();
            }
        }
        void GetCountProductMarjo(int id)
        {
            var q = db.CountProducts.Include("counProductInAnbar").Where(i => i.id == id).ToList();
            foreach (var item in q.ToList())
            {
                CounProductInAnbar p = db.CounProductInAnbars.Include("anbarCategoryP").Where(i => /*i.anbarCategoryP.AnbarName == item.anbarname &&*/ i.id == item.counProductInAnbar.id).FirstOrDefault();
                p.count += item.count;
                db.SaveChanges();
            }
        }

        public List<CountProduct> GetCountProductsByInvoiceId(int InvoiceID)
        {
            IQueryable<CountProduct> query = db.CountProducts.Include("invoiceC").Where(i => i.invoiceC.id == InvoiceID);
            query = query.Include("producC");
            query = query.Include("counProductInAnbar");
            return query.ToList();
        }
        public DataTable Read()
        {
            string cmd = "SELECT dbo.CountProducts.id, dbo.Invoices.InvoiceNumber AS [شماره فاکتور], dbo.Products.Name AS [نام محصول], dbo.CountProducts.count AS [تعداد در فاکتور], dbo.CountProducts.anbarname AS انبار, dbo.CountProducts.Marjoei AS مرجوعی, dbo.CountProducts.MasolMarjo AS [مسئول مرجوع], dbo.CountProducts.DateMarjo AS [تاریخ مرجوع], dbo.CountProducts.SabtKhoroj AS [ثبت خروج], dbo.CountProducts.MasolSabt AS [مسئول ثبت], dbo.CountProducts.Datekhoroj AS [تاریخ ثبت], dbo.CounProductInAnbars.count AS [تعداد کل همین محصول در این انبار], dbo.CounProductInAnbars.Kharab AS خرابی FROM dbo.Invoices INNER JOIN dbo.CountProducts ON dbo.Invoices.id = dbo.CountProducts.invoiceC_id INNER JOIN dbo.Products ON dbo.CountProducts.producC_id = dbo.Products.id INNER JOIN dbo.CounProductInAnbars ON dbo.CountProducts.counProductInAnbar_id = dbo.CounProductInAnbars.id AND dbo.Products.id = dbo.CounProductInAnbars.productP_id WHERE (dbo.Products.DeleteStatus = 0) AND (dbo.Invoices.DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchInvoiceForMarjo");

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
        public string SabtKhorojoDone(User u, int id)
        {


            var q = db.CountProducts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.SabtKhoroj !=true)
                    {
                        q.SabtKhoroj = true;
                        q.Marjoei = false;
                        q.MasolSabt = u.Name;
                        q.Datekhoroj = MetodExtations.ToShamsi(DateTime.Now);
                        db.SaveChanges();
                        GetCountProduct(id);
                        return "پردازش اطلاعات با موفقیت انجام شد";
                    }
                    else
                    {
                        return "تیک ثب خروج که خورده شده است!!!";
                    }
                }
                else
                {
                    return "محصول فاکتور مورد نظر یافت نشدم!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public string MarjoDone(User u, int id)
        {


            var q = db.CountProducts.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    if (q.SabtKhoroj == true)
                    {
                        q.Marjoei = true;
                        q.SabtKhoroj = false;
                        q.MasolMarjo = u.Name;
                        q.DateMarjo = MetodExtations.ToShamsi(DateTime.Now);
                        db.SaveChanges();
                        GetCountProductMarjo(id);
                        return "پردازش اطلاعات با موفقیت انجام شد";
                    }
                    else
                    {
                        return "کالای مورد نظر هنوز ثبت خروج نشده است که بخواهد ثبت مرجوع شود";
                    }
                }
                else
                {
                    return "محصول فاکتور مورد نظر یافت نشدم!";
                }
            }
            catch (Exception e)
            {

                return "پردازش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }

    }
}
