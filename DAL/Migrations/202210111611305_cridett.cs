namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cridett : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CreditWithoutDocuments", c => c.Double(nullable: false));
            AddColumn("dbo.Customers", "TotalCreditWithDocument", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "TotalCreditWithDocument");
            DropColumn("dbo.Customers", "CreditWithoutDocuments");
        }
    }
}
