using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal interface IOrderStatusRepository 
    {
        public IEnumerable<OrderStatus> GetOrderStatuses();
        public OrderStatus GetOrderStatus(int id);
        public void UpdateOrderStatus(OrderStatus orderStatus);
        public void CreateOrderStatus(OrderStatus orderStatus);
        public void DeleteOrderStatus(int id);
    }
}
