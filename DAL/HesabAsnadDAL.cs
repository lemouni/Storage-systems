using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class HesabAsnadDAL
    {
        DB db = new DB();
        public string Create(HesabBank hb)
        {
            try
            {
                db.HesabBanks.Add(hb);
                db.SaveChanges();
                return "ثبت اطلاعات با موفقیت انجام شد";
            }
            catch (Exception e)
            {
                return "ثبت اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public bool Read(HesabBank hb)
        {
            var q = db.HesabBanks.Where(i => hb.ShomareHesab == i.ShomareHesab);
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
            string cmd = "SELECT id, name AS [نام بانک], Sheba AS [شماره شبا], ShomareHesab AS [شماره حساب], Stock AS موجودی FROM dbo.HesabBanks WHERE (DeleteStatus = 0)";
            SqlConnection con = new SqlConnection("Data Source =.; Initial Catalog = CRMToseGar; Integrated Security = true");
            var sqladapter = new SqlDataAdapter(cmd, con);
            var commandbuilder = new SqlCommandBuilder(sqladapter);
            var ds = new DataSet();
            sqladapter.Fill(ds);
            return ds.Tables[0];
        }
        public DataTable Read(string s)
        {
            SqlCommand cmd = new SqlCommand("dbo.SearchHesabBanks");
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
        public HesabBank ReadN(string p)
        {
            return db.HesabBanks.Where(i => i.name == p && i.DeleteStatus == false).SingleOrDefault();
        }
        public HesabBank Read(int id)
        {
            return db.HesabBanks.Where(i => i.id == id).FirstOrDefault();
        }
        public List<string> ReadNmaes()
        {
            return db.HesabBanks.Where(i => i.DeleteStatus == false).Select(i => i.ShomareHesab).ToList();
        }
        public string Update(HesabBank p, int id)
        {
            var q = db.HesabBanks.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null)
                {
                    q.name = p.name;
                    q.Sheba = p.Sheba;
                    q.ShomareHesab = p.ShomareHesab;
                    q.Stock = p.Stock;
                    db.SaveChanges();
                    return "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "حساب مورد نظر یافت نشد!";
                }
            }
            catch (Exception e)
            {

                return "ویرایش اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }

        }
        public string Delete(int id)
        {
            var q = db.HesabBanks.Where(i => i.id == id).FirstOrDefault();
            try
            {
                if (q != null && q.Stock == 0)
                {
                    //db.Products.Remove(q);
                    q.DeleteStatus = true;
                    db.SaveChanges();
                    return "حذف اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    return "حساب مورد نظر یافت نشد یا دارای موجودی می باشد!";
                }
            }
            catch (Exception e)
            {

                return "حذف اطلاعات با مشکلی مواجه شد:\n" + e.Message;
            }
        }
        public HesabBank Readdd(string p)
        {
            return db.HesabBanks.Where(i => i.ShomareHesab == p && i.DeleteStatus == false).SingleOrDefault();
        }
    }
}
