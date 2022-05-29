namespace EFCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        customerId = c.Long(nullable: false, identity: true),
                        cutomerName = c.String(nullable: false, maxLength: 50),
                        customerEmail = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.customerId);
            
            CreateTable(
                "dbo.OrderList",
                c => new
                    {
                        orderListId = c.Long(nullable: false, identity: true),
                        orderId = c.Long(nullable: false),
                        productId = c.Long(nullable: false),
                        productQuantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.orderListId)
                .ForeignKey("dbo.Orders", t => t.orderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.productId, cascadeDelete: true)
                .Index(t => t.orderId)
                .Index(t => t.productId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        orderId = c.Long(nullable: false, identity: true),
                        customerId = c.Long(nullable: false),
                        orderStatus = c.Boolean(nullable: false),
                        orderDetails = c.String(nullable: false),
                        orderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.orderId)
                .ForeignKey("dbo.Customers", t => t.customerId, cascadeDelete: true)
                .Index(t => t.customerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        productId = c.Long(nullable: false, identity: true),
                        productName = c.String(nullable: false, maxLength: 50),
                        productPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        productType = c.String(nullable: false, maxLength: 50),
                        supplierId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.productId)
                .ForeignKey("dbo.Suppliers", t => t.supplierId, cascadeDelete: true)
                .Index(t => t.supplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        supplierId = c.Long(nullable: false, identity: true),
                        supplierName = c.String(nullable: false, maxLength: 50),
                        supplierPhone = c.String(nullable: false, maxLength: 18),
                        supplierEmail = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.supplierId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderList", "productId", "dbo.Products");
            DropForeignKey("dbo.Products", "supplierId", "dbo.Suppliers");
            DropForeignKey("dbo.OrderList", "orderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "customerId", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "supplierId" });
            DropIndex("dbo.Orders", new[] { "customerId" });
            DropIndex("dbo.OrderList", new[] { "productId" });
            DropIndex("dbo.OrderList", new[] { "orderId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderList");
            DropTable("dbo.Customers");
        }
    }
}
