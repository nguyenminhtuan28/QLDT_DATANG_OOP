using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductBLL
    {
        private ProductDAL _productDAL;

        public ProductBLL(ProductDAL dal)
        {
            _productDAL = dal;
        }

        public void AddProduct(ProductDTO product)
        {
            _productDAL.Add(product);
        }

        public List<ProductDTO> GetAllProducts()
        {
            return new List<ProductDTO>(_productDAL.GetAll());
        }

        public ProductDTO FindProduct(string id)
        {
            return _productDAL.FindById(id);
        }

        public LinkedList<ProductDTO> GetStorage()
        {
            return _productDAL.GetStorage();
        }
    }
}
