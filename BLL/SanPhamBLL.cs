using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using DAL;
using DTO;
namespace BLL
{
    internal class SanPhamBLL
    {
    }
    public class ProductBLL
    {
        private ProductDAL productDAL;

        public ProductBLL(string xmlFilePath)
        {
            productDAL = new ProductDAL(xmlFilePath);
        }

        public List<ProductDTO> GetProducts()
        {
            return productDAL.GetAllProducts();
        }
    }
}
