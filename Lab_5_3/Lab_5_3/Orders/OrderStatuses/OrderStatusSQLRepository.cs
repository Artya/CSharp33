using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab_5_3
{
    internal class OrderStatusSQLRepository : IOrderStatusRepository, ISQLRepository
    {
        public SqlConnection Connection { get; }
        public string SchemaName => "DBO";
        public string TableName => "OrderStatuses";

        public OrderStatusSQLRepository(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public OrderStatus GetOrderStatusFromReader(SqlDataReader reader)
        {
            var currentOrderStatus = new OrderStatus();

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);

                switch (fieldName)
                {
                    case nameof(OrderStatus.ID):
                        currentOrderStatus.ID = (int)reader[i];
                        break;

                    case nameof(OrderStatus.Name):
                        currentOrderStatus.Name = (string)reader[i];
                        break;

                    default:
                        Console.WriteLine($"Field  {fieldName} isn`t in class order status, or it filling not implemented");
                        break;
                }
            }

            return currentOrderStatus;
        }

        public IEnumerable<OrderStatus> GetOrderStatuses()
        {
            var orderStatus = new List<OrderStatus>();

            var reader = SQLRepositoryHelper.GetSeletionAllReader(this);

            while (reader.Read())
            {
                var currentOrderStatus = GetOrderStatusFromReader(reader);
                orderStatus.Add(currentOrderStatus);
            }

            reader.Close();

            return orderStatus;
        }

        public OrderStatus GetOrderStatus(int id)
        {
            var reader = SQLRepositoryHelper.ExecuteGetObjectByID(id, this);

            if (reader == null)
                return null;

            var orderStatus = GetOrderStatusFromReader(reader);
            reader.Close();
            return orderStatus;
        }

        public void UpdateOrderStatus(OrderStatus orderStatus)
        {
            SQLRepositoryHelper.ExecuteUpdatingObject(orderStatus, this);
        }

        public void CreateOrderStatus(OrderStatus orderStatus)
        {
            SQLRepositoryHelper.ExecuteInsertingObject(orderStatus, this);
        }

        public void DeleteOrderStatus(int id)
        {
            SQLRepositoryHelper.ExecuteDeletingObject(id, this);
        }
    }
}
