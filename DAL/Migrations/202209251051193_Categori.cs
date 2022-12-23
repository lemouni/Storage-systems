namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categori : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Categori", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Categori");
        }
    }
}
