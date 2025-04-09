using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDTO : ProductDTO
    {
        public string OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItemDTO> OrderedProducts { get; set; }
        public string OrderStatus { get; set; }

        public OrderDTO() : base()
        {
            OrderedProducts = new List<OrderItemDTO>();
        }

        public decimal CalculateTotal()
        {
            decimal total = 0;
            foreach (var item in OrderedProducts)
            {
                total += item.Product.Price * item.Quantity;
            }
            return total;
        }

        public string GetOrderDetails()
        {
            string details = $"Ma don hang: {OrderID}\nNgay dat: {OrderDate}\nTrang thai: {OrderStatus}\n";
            details += "Danh sach san pham:\n";
            foreach (var item in OrderedProducts)
            {
                details += $"  - {item.Product.Name} (Ma: {item.Product.ProductID}), Gia: {item.Product.Price}, SL: {item.Quantity}\n";
            }
            details += $"Tong tien: {CalculateTotal()}\n";
            return details;
        }
    }
}
