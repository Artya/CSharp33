using System;
using System.Collections.Generic;

namespace Lab_5_3
{
    internal partial class OrderDataProcessor : IDBObjectDataProcessor
    {
        private static IOrderRepository orderRepository = default;

        public string MenuTitle => "Orders";

        public OrderDataProcessor(IOrderRepository repository)
        {
            if (orderRepository == null)
                orderRepository = repository;
        }
        public void CreateObject()
        {
            var order = new Order()
            {
                Date = DateTime.Now
            };

            order.Details = UserInteraction.GetString("Enter order detalis");
            order.Status = OrderStatusDataProcessor.GetDefauldOrderStatus();
            order.Customer = CustomerDataProcessor.SelectCustomer();

            var addingMode = OrderGoodRowAddingMode.AddNewRow;
            var products = ProductDataProcessor.GetProducts();

            do
            {
                AddNewRowToOrderGoods(order, products);

                addingMode = (OrderGoodRowAddingMode)UserInteraction.ChooseEnumValue("Choosse next step", typeof(OrderGoodRowAddingMode));
            }
            while (addingMode != OrderGoodRowAddingMode.FinishAdding);

            order.RecalculateTotalSum();

            orderRepository.CreateOrder(order);

            ShowOrder(order);
        }

        private static void AddNewRowToOrderGoods(Order order, List<Product> products = null)
        {
            if (products == null)
                products = ProductDataProcessor.GetProducts();

            var currentOrderGood = new OrderGood() { Order = order };
            var productID = UserInteraction.ChooseElementFromList<Product>(products, "Choose product");
            currentOrderGood.Product = products[productID];

            currentOrderGood.Amount = UserInteraction.GetDecimal("Enter product amount");

            order.AddGoodRow(currentOrderGood);
        }

        public void DeleteObject()
        {
            var id = UserInteraction.GetInt("Enter order ID");
            var order = orderRepository.GetOrder(id);
            orderRepository.DeleteOrder(order);
        }

        public void EditObject()
        {
            var order = GetOrderByID();

            if (order == null)
                return;

            var fields = VariableFields.GetChangingFields(typeof(Order));

            while (true)
            {
                var index = UserInteraction.ChooseElementFromList<VariableFields>(fields, "Choose field to edit");

                if (index == fields.Count - 1)
                {
                    if (order.IsModified)
                    {
                        orderRepository.UpdateOrder(order);
                    }
                    return;
                }

                switch (fields[index].FieldName)
                {
                    case "Details":
                        order.Details = UserInteraction.GetString($"Enter new {fields[index]} for {order}");
                        break;

                    case "Date":
                        order.Date = UserInteraction.GetDate($"Enter new {fields[index]} for {order}");
                        break;

                    case "Status":
                        order.Status = OrderStatusDataProcessor.SelectOrderStatus();
                        break;

                    case "Goods":
                        EditOrderGoods(order);
                        break;
                }

                order.SetModifiedOn();
            }
        }

        private static void EditOrderGoods(Order order)
        {
            var editMode = (OrderGoodsEditMode)UserInteraction.ChooseEnumValue("Choose edit mode", typeof(OrderGoodsEditMode));

            switch (editMode)
            {
                case OrderGoodsEditMode.AddNew:
                    AddNewRowToOrderGoods(order);
                    break;

                case OrderGoodsEditMode.Edit:
                    EditOrderGoodRow(order);
                    break;

                case OrderGoodsEditMode.Delete:
                    var orderRow = SelectOrderRowFromOrder(order);
                    order.RemoveGoodRow(orderRow);
                    break;
            }

            order.SetModifiedOn();
        }

        private static void EditOrderGoodRow(Order order)
        {
            if (order.Goods == null || order.Goods.Count == 0)
                return;

            var orderRow = SelectOrderRowFromOrder(order);
            var products = ProductDataProcessor.GetProducts();
            var fields = VariableFields.GetChangingFields(typeof(OrderGood));

            while (true)
            {
                var index = UserInteraction.ChooseElementFromList<VariableFields>(fields, "Choose field to edit");

                if (index == fields.Count - 1)
                    return;

                switch (fields[index].FieldName)
                {
                    case "Product":
                        var productID = UserInteraction.ChooseElementFromList<Product>(products, "Choose a new product");
                        orderRow.Product = products[productID];
                        break;

                    case "Amount":
                        orderRow.Amount = UserInteraction.GetDecimal("Enter new amount");
                        break;
                }

                order.SetModifiedOn();
            }
        }

        private static OrderGood SelectOrderRowFromOrder(Order order)
        {
            if (order.Goods == null || order.Goods.Count == 0)
                return null;

            var orderGoodID = UserInteraction.ChooseElementFromList(order.Goods, "Select row");
            var orderRow = order.Goods[orderGoodID];
            return orderRow;
        }

        public void ReadObject()
        {
            var order = GetOrderByID();

            if (order == null)
                return;

            ShowOrder(order);
        }

        private static Order GetOrderByID()
        {
            var id = UserInteraction.GetInt("Enter order ID:");
            var order = orderRepository.GetOrder(id);
            return order;
        }

        public void ShowAllObjects()
        {
            var orders = orderRepository.GetOrders();
            DatabaseTable.ShowObjectsList(orders, "Orders: ");
        }
        public static void ShowOrder(Order order)
        {
            Console.WriteLine(order);
            Console.WriteLine($"Customer: {order.Customer} Status: {order.Status}");
            Console.WriteLine("Details:");
            Console.WriteLine(order.Details);

            if (order.Goods == null)
            {
                Console.WriteLine("No Goods");
                return;
            }

            Console.WriteLine("Goods:");

            var counter = 0;

            foreach (var goodRow in order.Goods)
            {
                Console.WriteLine($"{++counter}. {goodRow.Product} Amount: {goodRow.Amount}");
            }
        }
    }
}
