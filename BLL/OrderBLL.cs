using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BLL
{
    public class OrderBLL
    {
        private OrderDAL orderDAL;

        public OrderBLL()
        {
            orderDAL = new OrderDAL();
        }

        // Tạo đơn hàng mới
        public OrderDTO CreateOrder(string orderID, DateTime orderDate, string orderStatus)
        {
            OrderDTO order = new OrderDTO
            {
                OrderID = orderID,
                OrderDate = orderDate,
                OrderStatus = orderStatus
            };
            orderDAL.AddOrder(order);
            return order;
        }

        // Thêm sản phẩm vào đơn hàng
        public bool AddProductToOrder(OrderDTO order, ProductDTO product, int quantity)
        {
            if (product.Stock < quantity)
            {
                Console.WriteLine("Số lượng tồn không đủ.");
                return false;
            }

            OrderItemDTO orderItem = order.OrderedProducts.Find(item => item.Product.ProductID == product.ProductID);
            if (orderItem != null)
            {
                if (product.Stock < orderItem.Quantity + quantity)
                {
                    Console.WriteLine("Số lượng tồn không đủ để tăng số lượng đặt.");
                    return false;
                }
                orderItem.Quantity += quantity;
            }
            else
            {
                order.OrderedProducts.Add(new OrderItemDTO(product, quantity));
            }

            product.Stock -= quantity;
            return true;
        }

        // Xóa sản phẩm khỏi đơn hàng
        public bool RemoveProductFromOrder(OrderDTO order, string productID)
        {
            OrderItemDTO orderItem = order.OrderedProducts.Find(item => item.Product.ProductID == productID);
            if (orderItem != null)
            {
                orderItem.Product.Stock += orderItem.Quantity;
                order.OrderedProducts.Remove(orderItem);
                return true;
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm trong đơn hàng.");
                return false;
            }
        }

        // Cập nhật số lượng sản phẩm đặt
        public bool UpdateProductQuantity(OrderDTO order, string productID, int newQuantity)
        {
            OrderItemDTO orderItem = order.OrderedProducts.Find(item => item.Product.ProductID == productID);
            if (orderItem != null)
            {
                int currentQuantity = orderItem.Quantity;
                int difference = newQuantity - currentQuantity;

                if (difference > 0)
                {
                    if (orderItem.Product.Stock < difference)
                    {
                        Console.WriteLine("Số lượng tồn không đủ để cập nhật số lượng đặt.");
                        return false;
                    }
                    orderItem.Product.Stock -= difference;
                }
                else if (difference < 0)
                {
                    orderItem.Product.Stock += (-difference);
                }
                orderItem.Quantity = newQuantity;
                return true;
            }
            else
            {
                Console.WriteLine("Không tìm thấy sản phẩm trong đơn hàng.");
                return false;
            }
        }

        // Tính tổng giá trị đơn hàng (bổ sung nếu cần, ở đây ta dùng hàm CalculateTotal của OrderDTO)
        public decimal CalculateOrderTotal(OrderDTO order)
        {
            return order.CalculateTotal();
        }

        // In chi tiết đơn hàng ra console
        public void PrintOrderDetails(OrderDTO order)
        {
            Console.WriteLine("=================================");
            Console.WriteLine(order.GetOrderDetails());
            Console.WriteLine("=================================");
        }
    }
}
