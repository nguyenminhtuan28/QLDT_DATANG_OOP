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
        private List<OrderDTO> orderList = new List<OrderDTO>();

        public OrderDTO CreateOrder(string orderID, DateTime orderDate, string status)
        {
            var order = new OrderDTO
            {
                OrderID = orderID,
                OrderDate = orderDate,
                OrderStatus = status
            };
            orderList.Add(order);
            return order;
        }

        public bool AddProductToOrder(OrderDTO order, ProductDTO product, int quantity)
        {
            if (product.Stock < quantity)
            {
                Console.WriteLine("Khong du ton kho.");
                return false;
            }

            var item = order.OrderedProducts.Find(p => p.Product.ProductID == product.ProductID);
            if (item != null)
            {
                if (product.Stock < item.Quantity + quantity)
                {
                    Console.WriteLine("Khong du ton kho de tang so luong.");
                    return false;
                }
                item.Quantity += quantity;
            }
            else
            {
                order.OrderedProducts.Add(new OrderItemDTO(product, quantity));
            }
            product.Stock -= quantity;
            return true;
        }

        public bool RemoveProductFromOrder(OrderDTO order, string productID)
        {
            var item = order.OrderedProducts.Find(p => p.Product.ProductID == productID);
            if (item != null)
            {
                item.Product.Stock += item.Quantity;
                order.OrderedProducts.Remove(item);
                return true;
            }
            Console.WriteLine("Khong tim thay san pham trong don hang.");
            return false;
        }

        public bool UpdateProductQuantity(OrderDTO order, string productID, int newQty)
        {
            var item = order.OrderedProducts.Find(p => p.Product.ProductID == productID);
            if (item != null)
            {
                int diff = newQty - item.Quantity;
                if (diff > 0 && item.Product.Stock < diff)
                {
                    Console.WriteLine("Khong du ton kho.");
                    return false;
                }
                item.Product.Stock -= diff;
                item.Quantity = newQty;
                return true;
            }
            Console.WriteLine("Khong tim thay san pham trong don hang.");
            return false;
        }

        public void PrintOrder(OrderDTO order)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine(order.GetOrderDetails());
            Console.WriteLine("---------------------------");
        }
    }
}
