namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Price1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Price1", c => c.Double(nullable: false));
            DropColumn("dbo.Products", "Categori");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Categori", c => c.String());
            DropColumn("dbo.Products", "Price1");
        }
    }
}
