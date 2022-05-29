using EFCodeFirst.DB.Models;
using System.Data.Entity;

namespace EFCodeFirst.DB
{
    class Context : DbContext
    {
        public Context()
            : base("data source=.\\;initial catalog=EF_DB;integrated security=True;")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderList> OrderLists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}