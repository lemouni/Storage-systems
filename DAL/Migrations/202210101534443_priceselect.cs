namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priceselect : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountProducts", "priceselect", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CountProducts", "priceselect");
        }
    }
}
