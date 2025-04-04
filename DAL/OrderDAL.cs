using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;



namespace DAL
{
    public class OrderDAL
    {
        private List<OrderDTO> orders = new List<OrderDTO>();

        public void AddOrder(OrderDTO order)
        {
            orders.Add(order);
        }

        public OrderDTO GetOrderById(string orderID)
        {
            return orders.Find(o => o.OrderID == orderID);
        }

        public List<OrderDTO> GetAllOrders()
        {
            return orders;
        }

        public void RemoveOrder(string orderID)
        {
            OrderDTO order = GetOrderById(orderID);
            if (order != null)
                orders.Remove(order);
        }
    }
}
