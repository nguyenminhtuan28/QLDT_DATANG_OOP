using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderItemDTO
    {
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }

        public OrderItemDTO() { }

        public OrderItemDTO(ProductDTO product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
