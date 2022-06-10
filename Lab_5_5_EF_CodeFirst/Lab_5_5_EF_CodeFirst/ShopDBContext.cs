using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;

namespace Lab_5_5_EF_CodeFirst
{
    internal class ShopDBContext : DbContext
    {
        public ShopDBContext()
        {

        }

        public ShopDBContext(string connectionString)
            : base(connectionString)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderList> OrderList { get; set; }
    }
}
