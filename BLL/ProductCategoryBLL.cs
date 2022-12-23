using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class ProductCategoryBLL
    {
        ProductCategoryDAL dal = new ProductCategoryDAL();
        public string Create(ProductCategory pc)
        {
            return dal.Create(pc);
        }
        public bool Read(ProductCategory pc)
        {
            return dal.Read(pc);
        }
        public DataTable Read()
        {
            return dal.Read();
        }
        public ProductCategory Read(int id)
        {
            return dal.Read(id);
        }
        public string Update(ProductCategory pc, int id)
        {
            return dal.Update(pc, id);
        }
        public string Delete(int id)
        {
            return dal.Delete(id);
        }
        public List<string> ReadNamesPC()
        {
            return dal.ReadNamesPC();
        }
        public ProductCategory Read(string p)
        {
            return dal.Read(p);
        }

    }
}
