namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sd : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AnbarCategoryProducts", newName: "ProductAnbarCategories");
            DropPrimaryKey("dbo.ProductAnbarCategories");
            AddColumn("dbo.CountProducts", "counProductInAnbar_id", c => c.Int());
            AddPrimaryKey("dbo.ProductAnbarCategories", new[] { "Product_id", "AnbarCategory_id" });
            CreateIndex("dbo.CountProducts", "counProductInAnbar_id");
            AddForeignKey("dbo.CountProducts", "counProductInAnbar_id", "dbo.CounProductInAnbars", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CountProducts", "counProductInAnbar_id", "dbo.CounProductInAnbars");
            DropIndex("dbo.CountProducts", new[] { "counProductInAnbar_id" });
            DropPrimaryKey("dbo.ProductAnbarCategories");
            DropColumn("dbo.CountProducts", "counProductInAnbar_id");
            AddPrimaryKey("dbo.ProductAnbarCategories", new[] { "AnbarCategory_id", "Product_id" });
            RenameTable(name: "dbo.ProductAnbarCategories", newName: "AnbarCategoryProducts");
        }
    }
}
