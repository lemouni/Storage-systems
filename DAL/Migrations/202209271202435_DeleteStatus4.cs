namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStatus4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "DeleteStatus", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Invoices", "CheckoutDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "CheckoutDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Invoices", "DeleteStatus");
        }
    }
}
