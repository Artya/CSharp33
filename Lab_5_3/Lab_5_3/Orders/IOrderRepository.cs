using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal interface IOrderRepository 
    {
        public IEnumerable<Order> GetOrders();
        public Order GetOrder(int id);
        public void UpdateOrder(Order order);
        public void CreateOrder(Order order);
        public void DeleteOrder(Order order);
    }
}
