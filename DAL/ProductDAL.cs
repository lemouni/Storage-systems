using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DAL
{
    public class ProductDAL
    {
        DB db = new DB();
        public Product Create(Product p , ProductCategory pc, List<CounProductInAnbar> cpa)
        {
            try
            {
                p.Category = db.ProductCategories.Find(pc.id);

                foreach (var item in cpa)
                {
                    var anbarcategory = db.AnbarCategories.FirstOrDefault(x => x.id == item.anbarCategoryP.id);
                    var CounProductInAnbar = new CounProductInAnbar
                    {
                        anbarCategoryP = anbarcategory,
                        count = item.count,

                    };
                    p.CounProductInAnbars.Add(CounProductInAnbar);
                }

                if (pc.CategoryNameP != null)
                {
                    db.Products.Add(p);
                    db.SaveChanges();
                    MessageBox.Show("ثبت اطلاعات با موفقیت انجام شد");
                    return p;
                }
                else
                {
                    MessageBox.Show("دسته محصولات مورد نظر در بانک اطلاعاتی یافت نشد");
                    return new Product();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message);
                return new Product();

            }
        }
        public bool Read(Product p)
        {
            var q = db.Products.Where(i => p.Name == i.Name);
            if (q.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public DataTable Read()
        {
            string cmd = "SELECT dbo.Products.id, dbo.Products.Name AS [نام محصول], dbo.ProductCategories.CategoryNameP AS [دسته محصول], dbo.Products.Stock AS موجودی, dbo.Products.Price AS [قیمت تکی], dbo.Products.Price1 AS [قیمت عمده], dbo.Products.RegDate AS [تاریخ ثبت] FROM dbo.Products INNER JOIN dbo.ProductCategories ON dbo.Products.Category_id = dbo.ProductCategories.id WHERE (dbo.Products.DeleteStatus = 0)";
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
                cmd.CommandText = "dbo.SearchProduct1";

            }
            else if (index == 1)
            {
                cmd.CommandText = "dbo.SearchProductName1";
            }
            else if (index == 2)
            {
                cmd.CommandText = "dbo.SearchProductCategoy";
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
        public Product Read(int id)
        {
            return db.Products.Include("Category").Where(i => i.id == id).FirstOrDefault();
        }
        public Product ReadN(string p)
        {
            return db.Products.Where(i => i.Name == p && i.DeleteStatus==false).SingleOrDefault();
        }
        public List<string> ReadNmaes()
        {
            return db.Products.Where(i => i.DeleteStatus == false).Select(i=>i.Name).ToList();
        }
        public string Update(Product p,ProductCategory pc, int id)
        {
            var q = db.Products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.Category = db.ProductCategories.Find(pc.id);
                    q.Name = p.Name;
                    q.Price = p.Price;
                    q.Price1 = p.Price1;
                    q.Stock = p.Stock;
                    //q.Category.CategoryNameP = p.Category.CategoryNameP;
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "محصول مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string Delete(int id)
        {
            var q = db.Products.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null && q.Stock ==0)
                {
                    //db.Products.Remove(q);
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "محصول مورد نظر یافت نشد یا دارای موجودی کالا می باشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }


    }

}


