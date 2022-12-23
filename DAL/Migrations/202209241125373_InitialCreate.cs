namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Info = c.String(),
                        RegDate = c.DateTime(nullable: false),
                        Category_id = c.Int(),
                        Customer_id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.ActivityCategories", t => t.Category_id)
                .ForeignKey("dbo.Customers", t => t.Customer_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Category_id)
                .Index(t => t.Customer_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.ActivityCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DeleteStatus = c.Boolean(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        RegDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        InvoiceNumber = c.String(),
                        RegDate = c.DateTime(nullable: false),
                        IsCheckedOut = c.Boolean(nullable: false),
                        CheckoutDate = c.DateTime(nullable: false),
                        Customer_id = c.Int(),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Customers", t => t.Customer_id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.Customer_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                        Pic = c.String(),
                        Status = c.Boolean(nullable: false),
                        RegDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Reminders",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReminderInfo = c.String(),
                        RegDate = c.DateTime(nullable: false),
                        RemindDate = c.DateTime(nullable: false),
                        IsDone = c.Boolean(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProductInvoices",
                c => new
                    {
                        Product_id = c.Int(nullable: false),
                        Invoice_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Product_id, t.Invoice_id })
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.Invoice_id, cascadeDelete: true)
                .Index(t => t.Product_id)
                .Index(t => t.Invoice_id);
            
            CreateTable(
                "dbo.ReminderUsers",
                c => new
                    {
                        Reminder_id = c.Int(nullable: false),
                        User_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Reminder_id, t.User_id })
                .ForeignKey("dbo.Reminders", t => t.Reminder_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .Index(t => t.Reminder_id)
                .Index(t => t.User_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReminderUsers", "User_id", "dbo.Users");
            DropForeignKey("dbo.ReminderUsers", "Reminder_id", "dbo.Reminders");
            DropForeignKey("dbo.Invoices", "User_id", "dbo.Users");
            DropForeignKey("dbo.Activities", "User_id", "dbo.Users");
            DropForeignKey("dbo.ProductInvoices", "Invoice_id", "dbo.Invoices");
            DropForeignKey("dbo.ProductInvoices", "Product_id", "dbo.Products");
            DropForeignKey("dbo.Invoices", "Customer_id", "dbo.Customers");
            DropForeignKey("dbo.Activities", "Customer_id", "dbo.Customers");
            DropForeignKey("dbo.Activities", "Category_id", "dbo.ActivityCategories");
            DropIndex("dbo.ReminderUsers", new[] { "User_id" });
            DropIndex("dbo.ReminderUsers", new[] { "Reminder_id" });
            DropIndex("dbo.ProductInvoices", new[] { "Invoice_id" });
            DropIndex("dbo.ProductInvoices", new[] { "Product_id" });
            DropIndex("dbo.Invoices", new[] { "User_id" });
            DropIndex("dbo.Invoices", new[] { "Customer_id" });
            DropIndex("dbo.Activities", new[] { "User_id" });
            DropIndex("dbo.Activities", new[] { "Customer_id" });
            DropIndex("dbo.Activities", new[] { "Category_id" });
            DropTable("dbo.ReminderUsers");
            DropTable("dbo.ProductInvoices");
            DropTable("dbo.Reminders");
            DropTable("dbo.Users");
            DropTable("dbo.Products");
            DropTable("dbo.Invoices");
            DropTable("dbo.Customers");
            DropTable("dbo.ActivityCategories");
            DropTable("dbo.Activities");
        }
    }
}
