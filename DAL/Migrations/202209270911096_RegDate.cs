namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RegDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "RegDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "RegDate");
        }
    }
}
