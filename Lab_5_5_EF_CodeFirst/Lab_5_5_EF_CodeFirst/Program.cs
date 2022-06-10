using System;
using System.Data.Entity;

namespace Lab_5_5_EF_CodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var init = new CreateDatabaseIfNotExists<ShopDBContext>();

            using (var context = new ShopDBContext("data source=vm-dev-erp3;initial catalog=NewShop;integrated security=True;"))
            {
                init.InitializeDatabase(context);

                for (var i = 0; i < 3; i++)
                {
                    var rand = new Random();

                    var newSupplier = CreateSupplier();
                    context.Suppliers.Add(newSupplier);
                    context.SaveChanges();

                    var newProduct = CreteNewProduct(rand, newSupplier);
                    context.Products.Add(newProduct);
                    context.SaveChanges();

                    var newCustomer = CreateNewCustomer();
                    context.Customers.Add(newCustomer);
                    context.SaveChanges();

                    try
                    {
                        var newOrder = CreateNewOrder(newCustomer);
                        context.Orders.Add(newOrder);

                        OrderList newOrderList = CreateOrderList(rand, newProduct, newOrder);
                        context.OrderList.Add(newOrderList);

                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return;
                    }                   
                }
            }
        }

        private static OrderList CreateOrderList(Random rand, Product newProduct, Order newOrder)
        {
            return new OrderList()
            {
                Order = newOrder,
                Product = newProduct,
                Quantity = rand.Next()
            };
        }

        private static Order CreateNewOrder(Customer newCustomer)
        {
            return new Order()
            {
                OrderDate = DateTime.Now,
                OrderDetails = $"Some order details {DateTime.Now.ToString()}",
                OrderStatus = "NEW",
                Customer = newCustomer,
                CustomerID = newCustomer.Id
            };
        }

        private static Customer CreateNewCustomer()
        {
            return new Customer()
            {
                Name = $"Customer {DateTime.Now.ToString()}",
                Phone = $"234 {DateTime.Now.ToString()}",
                Email = $"{DateTime.Now.ToString()}@Customers.ua",
                Details = $"Some customer details {DateTime.Now.ToString()}"
            };
        }

        private static Product CreteNewProduct(Random rand, Supplier newSupplier)
        {
            return new Product()
            {
                Name = $"Product {DateTime.Now.ToString()}",
                Price = (decimal)(rand.Next() + rand.NextDouble()),
                Supplier = newSupplier,
                SupplierID = newSupplier.ID
            };
        }

        private static Supplier CreateSupplier()
        {
            return new Supplier()
            {
                Name = $"Supplier {DateTime.Now.ToString()}",
                Phone = $"123 {DateTime.Now.ToString()}",
                Email = $"{DateTime.Now.ToString()}@Suppliers.ua"
            };
        }
    }
}
