using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab_5_3
{
    internal class OrderSQLRepository : IOrderRepository, ISQLRepository
    {
        private OrderGoodsSQLRepository orderGoodsRepository = default;

        public SqlConnection Connection { get; set; }

        public string SchemaName => "DBO";

        public string TableName => "Orders";

        public OrderSQLRepository(SqlConnection connection)
        {
            this.Connection = connection;
            orderGoodsRepository = new OrderGoodsSQLRepository(connection);
        }

        public Order GetOrderFromReader(SqlDataReader reader)
        {
            var currentOrder = new Order();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);

                switch (fieldName)
                {
                    case nameof(Order.ID):
                        currentOrder.ID = (int)reader[i];
                        break;

                    case nameof(Order.Date):
                        currentOrder.Date = (DateTime)reader[i];
                        break;

                    case nameof(Order.Details):
                        currentOrder.Details = (string)reader[i];
                        break;

                    case nameof(Order.Status):
                        currentOrder.Status = OrderStatusDataProcessor.GetOrderStatusByID((int)reader[i]);
                        break;

                    case nameof(Order.Customer):
                        currentOrder.Customer = CustomerDataProcessor.GetCustomerByID((int)reader[i]);  
                        break;

                    default:
                        Console.WriteLine($"Field  {fieldName} isn`t in class Order or it filling not implemented");
                        break;
                }
            }

            return currentOrder;
        }

        public void CreateOrder(Order order)
        {
            SQLRepositoryHelper.ExecuteInsertingObject(order, this);

            if (order.Goods!= null && order.Goods.Count > 0)
                SQLRepositoryHelper.ExecuteInsertingListObjects(order.Goods, orderGoodsRepository);
        }

        public void DeleteOrder(Order order)
        {
            var transction = this.Connection.BeginTransaction();

            try
            {
                if (order.Goods != null && order.Goods.Count > 0)
                {
                    foreach (var goodRow in order.Goods)
                    {
                        if (goodRow.ID != 0)
                            SQLRepositoryHelper.ExecuteDeletingObject(goodRow.ID, orderGoodsRepository, transction);
                    }
                }

                SQLRepositoryHelper.ExecuteDeletingObject(order.ID, this, transction);

                transction.Commit();
            }
            catch (Exception ex)
            {
                transction.Rollback();
                Console.WriteLine($"Error deleting object {order} because of: {ex.Message}");
            }            
        }

        public Order GetOrder(int id)
        {
            var reader = SQLRepositoryHelper.ExecuteGetObjectByID(id, this);

            if (reader == null)
                return null;

            var order = GetOrderFromReader(reader);
            orderGoodsRepository.FillOrderGoods(order);

            reader.Close();
            return order;
        }

        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();
            var reader = SQLRepositoryHelper.GetSeletionAllReader(this);

            while (reader.Read())
            {
                var currentOrder = GetOrderFromReader(reader);
                orderGoodsRepository.FillOrderGoods(currentOrder);
                orders.Add(currentOrder);
            }

            reader.Close();

            return orders;
        }

        public void UpdateOrder(Order order)
        {
            var transaction = this.Connection.BeginTransaction();

            try
            {
                if (order.DeletedGoods != null && order.Goods.Count > 0)
                {
                    foreach (var deletedRowID in order.DeletedGoods)
                    {
                        SQLRepositoryHelper.ExecuteDeletingObject(deletedRowID, orderGoodsRepository, transaction);
                    }
                }

                SQLRepositoryHelper.ExecuteUpdatingObject(order, this, transaction);

                if (order.Goods != null && order.Goods.Count > 0)
                {
                    foreach (var goodRow in order.Goods)
                    {
                        orderGoodsRepository.UpdateOrderGood(goodRow, transaction);
                    }
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);

            }
        }
    }
}
