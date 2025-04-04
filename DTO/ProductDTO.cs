using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DTO
{
    [XmlInclude(typeof(OrderDTO))] 
    public class ProductDTO
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }

        public ProductDTO() { }

        public ProductDTO(string productID, string name, decimal price, int stock, string description)
        {
            ProductID = productID;
            Name = name;
            Price = price;
            Stock = stock;
            Description = description;
        }
    }
}
