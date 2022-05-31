using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Lab_5_3
{
    internal class OrderGoodsSQLRepository : ISQLRepository
    {
        public SqlConnection Connection { get; set;  }

        public string SchemaName => "DBO";

        public string TableName => "OrderGoods";

        public OrderGoodsSQLRepository(SqlConnection connection)
        {
            this.Connection = connection;
        }

        public void FillOrderGoods(Order order)
        {
            var conditions = new Dictionary<string, string>()
            {
                ["[Order]"] = order.ID.ToString()
            };

            var reader = SQLRepositoryHelper.ExecuteGetObjectByConditions(this, conditions);

            while (reader.Read())
            {
                var currentOrderGood = GetOrderGoodFromReader(reader, order);
                order.AddGoodRow(currentOrderGood);
            }

            order.RecalculateTotalSum();
        }

        public OrderGood GetOrderGoodFromReader(SqlDataReader reader, Order order)
        {
            var currentOrderGood = new OrderGood();            

            for (var i = 0; i < reader.FieldCount; i++)
            {
                var fieldName = reader.GetName(i);

                switch (fieldName)
                {
                    case nameof(OrderGood.ID):
                        currentOrderGood.ID = (int)reader[i];
                        break;

                    case nameof(OrderGood.Amount):
                        currentOrderGood.Amount = (decimal)reader[i];
                        break;

                    case nameof(OrderGood.Order):
                        currentOrderGood.Order = order;
                        break;

                    case nameof(OrderGood.Product):
                        currentOrderGood.Product = ProductDataProcessor.GetProductByID((int)reader[i]);
                        break;

                    default:
                        Console.WriteLine($"Field  {fieldName} isn`t in class Order or it filling not implemented");
                        break;
                }
            }

            return currentOrderGood;
        }

        public void DeleteOrderGood(int id)
        {
            SQLRepositoryHelper.ExecuteDeletingObject(id, this);
        }

        public void UpdateOrderGood(OrderGood orderGood, SqlTransaction transaction)
        {
            if (orderGood.ID == 0)
            { 
                SQLRepositoryHelper.ExecuteInsertingObject(orderGood, this, transaction);
                return;
            }

            SQLRepositoryHelper.ExecuteUpdatingObject(orderGood, this, transaction);
        }
    }
}
