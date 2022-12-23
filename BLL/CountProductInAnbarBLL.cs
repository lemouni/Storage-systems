using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BLL
{
    public class CountProductInAnbarBLL
    {
        CountProductInAnbarDAL dal = new CountProductInAnbarDAL();
        public List<BE.CounProductInAnbar> GetCountProductInAnbarByProductId(int ProductID)
        {
            return dal.GetCountProductInAnbarByProductId(ProductID);
        }
        public List<BE.CounProductInAnbar> GetProductsInAnbarByanbarcategorId(int anbarcategorId)
        {
            return dal.GetProductsInAnbarByanbarcategorId(anbarcategorId);
        }
        public List<BE.CounProductInAnbar> GetAnbarForTextBoxFactorP(string s)
        {
            return dal.GetAnbarForTextBoxFactorP(s);
        }
        public List<BE.CounProductInAnbar> GetAnbarForTextBoxBuyP()
        {
            return dal.GetAnbarForTextBoxBuyP();
        }
        public CounProductInAnbar ReadN(string p, Product pr)
        {
            return dal.ReadN(p,pr);
        }
    }
}
