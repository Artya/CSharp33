using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;
using EFCodeFirst.DB;
using EFCodeFirst.DB.Models;

namespace EFCodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new Context();

            Console.WriteLine("Preparing DB...");
            prepareDB(context);
            Console.WriteLine("Finished!\n");

            using (var memoryStream = serializeCustomersToJSON(context.Customers.ToList()))
            {
                WriteToFileJSON(memoryStream);
            }

            var customer = context.Customers.First();

            var product = context.Products
                .Where(p => p.productPrice < 6000)
                .First();

            Console.WriteLine("Before adding new order:");
            findAndPrintOrderInfoByCustomer(customer, context);
            Console.WriteLine();

            addNewOrder(customer, product, 2, "Kiev, Khreschatyk st. 36, 01044", context);

            Console.WriteLine("After inserting new order:");
            findAndPrintOrderInfoByCustomer(customer, context);
        }

        static MemoryStream serializeCustomersToJSON(List<Customer> customers)
        {
            var memoryStream = new MemoryStream();
            var jsonSerializer = new DataContractJsonSerializer(typeof(List<Customer>));

            jsonSerializer.WriteObject(memoryStream, customers);
            memoryStream.Position = 0;

            return memoryStream;
        }
        static void WriteToFileJSON(MemoryStream memoryStream)
        {
            using var file = File.Open("customers.json", FileMode.Create);
            using var streamReader = new StreamReader(memoryStream);

            file.Write(Encoding.UTF8.GetBytes(streamReader.ReadToEnd()));

            memoryStream.Position = 0;
        }

        static void findAndPrintOrderInfoByCustomer(Customer customer, Context context)
        {
            var orderInfos = context.OrderLists
                    .Join(context.Orders, entryPoint => entryPoint.orderId, entry => entry.orderId, (orderlist, order) => new { orderlist, order })
                    .Join(context.Products, entryPoint => entryPoint.orderlist.productId, entry => entry.productId, (entryPoint, product) => new { entryPoint.orderlist, entryPoint.order, product })
                    .Join(context.Customers, entryPoint => entryPoint.order.customerId, entry => entry.customerId, (entryPoint, cutomer) => new { entryPoint.orderlist, entryPoint.order, entryPoint.product, cutomer })
                    .Where(c => c.cutomer.customerId == customer.customerId)
                    .Select(q => new
                    {
                        OrderDetails = q.order.orderDetails,
                        CustomerName = q.cutomer.cutomerName,
                        ProductName = q.product.productName,
                        ProductPrice = q.product.productPrice,
                        ProductQuantity = q.orderlist.productQuantity,
                        OrderDate = q.order.orderDate,
                        TotalPrice = q.product.productPrice * q.orderlist.productQuantity
                    })
                    .ToList();

            foreach (var orderInfo in orderInfos)
            {
                Console.WriteLine(@$"Order details: {orderInfo.OrderDetails}
Customer name: {orderInfo.CustomerName}
Product name: {orderInfo.ProductName}
Product price: {orderInfo.ProductPrice}
Product quantity: {orderInfo.ProductQuantity}
Order date: {orderInfo.OrderDate}
Total price: {orderInfo.TotalPrice}");
                Console.WriteLine();
            }
        }
        static void addNewOrder(Customer customer, Product product, int productQuantity, string orderDetails, Context context)
        {
            using var transaction = context.Database.BeginTransaction();

            try
            {
                var order = new Order
                {
                    orderDetails = orderDetails,
                    orderStatus = true,
                    orderDate = DateTime.Now,
                    customer = customer
                };

                var orderList = new OrderList
                {
                    product = product,
                    productQuantity = productQuantity,
                    order = order
                };

                context.Orders.Add(order);
                context.OrderLists.Add(orderList);

                context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Operation failes with error: {ex.Message}");
                transaction.Rollback();
            }
        }

        static void clearDB(Context context)
        {
            context.Database.ExecuteSqlCommand("DELETE FROM dbo.Suppliers");
            context.Database.ExecuteSqlCommand("DELETE FROM dbo.Customers");
        }
        static void prepareDB(Context context)
        {
            clearDB(context);

            prepareCustomers(context);
            prepareSuppliers(context);
            prepareProducts(context);
            prepareOrders(context);
        }
        static void prepareCustomers(Context context)
        {
            var customer1 = new Customer
            {
                cutomerName = "Petya",
                customerEmail = "petya@email.com"
            };
            var customer2 = new Customer
            {
                cutomerName = "Vasya",
                customerEmail = "vasya@email.com"
            };
            var customer3 = new Customer
            {
                cutomerName = "Kolya",
                customerEmail = "kolya@email.com"
            };

            context.Customers.Add(customer1);
            context.Customers.Add(customer2);
            context.Customers.Add(customer3);

            context.SaveChanges();
        }
        static void prepareSuppliers(Context context)
        {
            var supplier1 = new Supplier
            {
                supplierName = "Rozetka",
                supplierPhone = "+38(098)-988-88-88",
                supplierEmail = "supplier@rozetka.ua"
            };
            var supplier2 = new Supplier
            {
                supplierName = "Citrus",
                supplierPhone = "+38(099)-999-09-09",
                supplierEmail = "supplier@citrus.ua"
            };

            context.Suppliers.Add(supplier1);
            context.Suppliers.Add(supplier2);

            context.SaveChanges();
        }
        static void prepareProducts(Context context)
        {
            var suppliers = context.Suppliers.Take(2).ToArray();

            var product1 = new Product
            {
                productName = "IPhone 18 512TB",
                productPrice = 5000,
                productType = "IPhone",
                supplier = suppliers[0]
            };
            var product2 = new Product
            {
                productName = "IPhone 18 PRO 1024TB",
                productPrice = 7000,
                productType = "IPhone",
                supplier = suppliers[0]
            };
            var product3 = new Product
            {
                productName = "Samsung Galaxy S50 512TB",
                productPrice = 5000,
                productType = "Samsung Galaxy series",
                supplier = suppliers[1]
            };

            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);

            context.SaveChanges();
        }
        static void prepareOrders(Context context)
        {
            var products = context.Products.Take(2).ToArray();
            var customers = context.Customers.Take(2).ToArray();

            var order1 = new Order
            {
                orderStatus = true,
                orderDate = DateTime.Now,
                orderDetails = "Kiev, Khreschatyk st. 36, 01044",
                customer = customers[0]
            };
            var orderList1 = new OrderList
            {
                order = order1,
                product = products[0],
                productQuantity = 1
            };

            var order2 = new Order
            {
                orderStatus = true,
                orderDate = DateTime.Now,
                orderDetails = "Kiev, Khreaschatyak st. 15, 01001",
                customer = customers[1]
            };
            var orderList2 = new OrderList
            {
                order = order2,
                product = products[1],
                productQuantity = 1
            };

            context.Orders.Add(order1);
            context.Orders.Add(order2);

            context.OrderLists.Add(orderList1);
            context.OrderLists.Add(orderList2);

            context.SaveChanges();
        }
    }
}
