namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "FeePaid", c => c.Double(nullable: false));
            AddColumn("dbo.Invoices", "TotalCost", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "TotalCost");
            DropColumn("dbo.Invoices", "FeePaid");
        }
    }
}
