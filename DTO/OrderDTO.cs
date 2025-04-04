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
            string details = $"Mã đơn hàng: {OrderID}\nNgày đặt: {OrderDate}\nTrạng thái: {OrderStatus}\n";
            details += "Danh sách sản phẩm:\n";
            foreach (var item in OrderedProducts)
            {
                details += $"  - {item.Product.Name} (Mã: {item.Product.ProductID}), Giá: {item.Product.Price}, Số lượng: {item.Quantity}\n";
            }
            details += $"Tổng giá trị đơn hàng: {CalculateTotal()}";
            return details;
        }
    }
}
