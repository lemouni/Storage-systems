using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public class CountProductInAnbarDAL
    {
        DB db = new DB();
        public List<BE.CounProductInAnbar> GetCountProductInAnbarByProductId(int ProductID)
        {
            IQueryable<BE.CounProductInAnbar> query = db.CounProductInAnbars.Include("productP").Where(i => i.productP.id == ProductID && i.productP.DeleteStatus==false);
            query = query.Include("anbarCategoryP");
            return query.ToList();
        }
        public List<BE.CounProductInAnbar> GetProductsInAnbarByanbarcategorId(int anbarcategorId)
        {
            IQueryable<BE.CounProductInAnbar> query = db.CounProductInAnbars.Include("anbarCategoryP").Where(i => i.anbarCategoryP.id == anbarcategorId && i.productP.DeleteStatus==false);
            query = query.Include("productP");
            return query.ToList();
        }
        public List<BE.CounProductInAnbar> GetAnbarForTextBoxFactorP(string s)
        {
            IQueryable<BE.CounProductInAnbar> query = db.CounProductInAnbars.Include("productP").Where(i => i.productP.Name == s && i.anbarCategoryP.DeleteStatus == false);
            query = query.Include("anbarCategoryP");
            return query.ToList();
        }
        public List<BE.CounProductInAnbar> GetAnbarForTextBoxBuyP()
        {
            IQueryable<BE.CounProductInAnbar> query = db.CounProductInAnbars.Include("anbarCategoryP").Where(i => i.anbarCategoryP.DeleteStatus == false);
            return query.ToList();
        }
        public CounProductInAnbar ReadN(string p , Product pr)
        {
            return db.CounProductInAnbars.Include("anbarCategoryP").Where(i => i.anbarCategoryP.AnbarName == p && i.productP.id ==pr.id).SingleOrDefault();
        }
    }
}
