namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "DeleteStatus", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DeleteStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DeleteStatus");
            DropColumn("dbo.Products", "DeleteStatus");
        }
    }
}
