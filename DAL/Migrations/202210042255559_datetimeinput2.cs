namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeinput2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activities", "RegDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Customers", "RegDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Invoices", "RegDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Invoices", "CheckoutDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Products", "RegDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Users", "RegDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Reminders", "RegDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reminders", "RegDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "RegDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Products", "RegDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Invoices", "CheckoutDate", c => c.DateTime());
            AlterColumn("dbo.Invoices", "RegDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Customers", "RegDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Activities", "RegDate", c => c.DateTime(nullable: false));
        }
    }
}
