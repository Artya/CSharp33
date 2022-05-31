using System;
using System.Data.SqlClient;

namespace Lab_5_3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new SqlConnection(DBConnectionString.GetConnectionString()))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error to open connection with DB: " + ex.Message);
                    return;
                }

                var customersHandler = new CustomerDataProcessor(new CustomerSQLRepository(connection));
                var suppliersHandler = new SupplierDataProcessor(new SupplierSQLRepository(connection));
                var productTypeHandler = new ProductTypeDataProcessor(new ProductTypeSQLRepository(connection));
                var productMenuHandler = new ProductDataProcessor(new ProductSQLRepository(connection));
                var orderStatusMenuHandler = new OrderStatusDataProcessor(new OrderStatusSQLRepository(connection));
                var ordersMenuHandler = new OrderDataProcessor(new OrderSQLRepository(connection));

                while (true)
                {
                    var operation = (MainMenu)UserInteraction.ChooseEnumValue("MAIN MENU", typeof(MainMenu));

                    switch (operation)
                    {
                        case MainMenu.Exit:
                            return;

                        case MainMenu.Customers:
                            DatabaseTable.WorkWithDBObject<CustomerDataProcessor>(customersHandler);
                            break;

                        case MainMenu.Suppliers:
                            DatabaseTable.WorkWithDBObject<SupplierDataProcessor>((SupplierDataProcessor)suppliersHandler);
                            break;

                        case MainMenu.ProductTypes:
                            DatabaseTable.WorkWithDBObject<ProductTypeDataProcessor>(productTypeHandler);
                            break;

                        case MainMenu.Products:
                            DatabaseTable.WorkWithDBObject<ProductDataProcessor>(productMenuHandler);
                            break;

                        case MainMenu.OrderStatuses:
                            DatabaseTable.WorkWithDBObject<OrderStatusDataProcessor>(orderStatusMenuHandler);
                            break;

                        case MainMenu.Orders:
                            DatabaseTable.WorkWithDBObject<OrderDataProcessor>(ordersMenuHandler);
                            break;

                        default:
                            Console.WriteLine($"Main menu element {operation} not implemented");
                            break;                    
                    }
                }
            };
        }
    }
}
