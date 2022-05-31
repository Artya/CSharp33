using System;
using System.Linq;


namespace Lab_5_3
{
    internal class OrderStatusDataProcessor : IDBObjectDataProcessor
    {
        private static IOrderStatusRepository orderStatusRepository = default;
        public string MenuTitle => "Order statuses";

        public OrderStatusDataProcessor(IOrderStatusRepository repository)
        {
            if (orderStatusRepository == null)
                orderStatusRepository = repository;
        }

        private static OrderStatus GetOrderStatusByID()
        {
            var id = UserInteraction.GetInt("Enter order status ID");
            return GetOrderStatusByID(id);
        }

        public static OrderStatus GetOrderStatusByID(int id)
        {
            return orderStatusRepository.GetOrderStatus(id);
        }

        public static OrderStatus GetDefauldOrderStatus()
        {
            var defaultStatusId = 1;

            return orderStatusRepository.GetOrderStatus(defaultStatusId);
        }

        public static OrderStatus SelectOrderStatus()
        {
            var statuses = orderStatusRepository.GetOrderStatuses().ToList();
            var statusID = UserInteraction.ChooseElementFromList(statuses, "Select status");

            return statuses[statusID];
        }

        public void CreateObject()
        {
            var orderStatus = new OrderStatus()
            {
                Name = UserInteraction.GetString("Enter order status name")
            };

            orderStatusRepository.CreateOrderStatus(orderStatus);
        }

        public void DeleteObject()
        {
            var id = UserInteraction.GetInt("Enter product type ID");
            orderStatusRepository.DeleteOrderStatus(id);
        }

        public void EditObject()
        {
            var orderStatus = GetOrderStatusByID();

            if (orderStatus == null)
                return;

            orderStatus.Name = UserInteraction.GetString("Enter new order status Name");

            orderStatusRepository.UpdateOrderStatus(orderStatus);
        }

        public void ReadObject()
        {
            var orderStatus = GetOrderStatusByID();
            Console.WriteLine(orderStatus);
        }

        public void ShowAllObjects()
        {
            var orderStatuses = orderStatusRepository.GetOrderStatuses();
            DatabaseTable.ShowObjectsList(orderStatuses, "Order statuses: ");
        }
    }
}
