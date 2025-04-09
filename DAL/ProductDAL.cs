using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{

    public class ProductDAL
    {
        private LinkedList<ProductDTO> _storage;

        public ProductDAL()
        {
            _storage = new LinkedList<ProductDTO>();
        }

        public LinkedList<ProductDTO> GetAll() => _storage;

        public void Add(ProductDTO product)
        {
            _storage.AddLast(product);
        }

        public ProductDTO FindById(string id)
        {
            foreach (var item in _storage)
            {
                if (item.ProductID == id) return item;
            }
            return null;
        }

        public void Save(LinkedList<ProductDTO> list)
        {
            _storage = list;
        }

        public LinkedList<ProductDTO> GetStorage() => _storage;
    }
}
