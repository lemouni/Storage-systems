using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BE;
using DAL;

namespace BLL
{
    public class ProductBLL
    {
        ProductDAL dal = new ProductDAL();
        public Product Create(Product p , ProductCategory pc, List<CounProductInAnbar> cpa)
        {
            if (dal.Read(p))
            {
                return dal.Create(p,pc,cpa);
            }
            else
            {
                MessageBox.Show("محصولی با همین نام در سیستم ثبت شده است");
                return new Product();
            }
        }
        public Product Read(int id)
        {
            return dal.Read(id);
        }
        public DataTable Read(string s, int index)
        {
            return dal.Read(s, index);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public string Update(Product p,ProductCategory pc, int id)
        {
            return dal.Update(p,pc, id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<string> ReadNmaes()
        {
            return dal.ReadNmaes();
        }
        public Product ReadN(string p)
        {
            return dal.ReadN(p);
        }

    }
}
