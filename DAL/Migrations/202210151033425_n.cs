namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class n : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AnbarCategoryProducts", newName: "ProductAnbarCategories");
            RenameColumn(table: "dbo.CountProducts", name: "producC_id", newName: "Product_id");
            RenameIndex(table: "dbo.CountProducts", name: "IX_producC_id", newName: "IX_Product_id");
            DropPrimaryKey("dbo.ProductAnbarCategories");
            AddColumn("dbo.CountProducts", "productanbarC_id", c => c.Int());
            AddColumn("dbo.CounProductInAnbars", "Marjoei", c => c.Boolean(nullable: false));
            AddColumn("dbo.CounProductInAnbars", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.CounProductInAnbars", "Price1", c => c.Double(nullable: false));
            AddPrimaryKey("dbo.ProductAnbarCategories", new[] { "Product_id", "AnbarCategory_id" });
            CreateIndex("dbo.CountProducts", "productanbarC_id");
            AddForeignKey("dbo.CountProducts", "productanbarC_id", "dbo.CounProductInAnbars", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CountProducts", "productanbarC_id", "dbo.CounProductInAnbars");
            DropIndex("dbo.CountProducts", new[] { "productanbarC_id" });
            DropPrimaryKey("dbo.ProductAnbarCategories");
            DropColumn("dbo.CounProductInAnbars", "Price1");
            DropColumn("dbo.CounProductInAnbars", "Price");
            DropColumn("dbo.CounProductInAnbars", "Marjoei");
            DropColumn("dbo.CountProducts", "productanbarC_id");
            AddPrimaryKey("dbo.ProductAnbarCategories", new[] { "AnbarCategory_id", "Product_id" });
            RenameIndex(table: "dbo.CountProducts", name: "IX_Product_id", newName: "IX_producC_id");
            RenameColumn(table: "dbo.CountProducts", name: "Product_id", newName: "producC_id");
            RenameTable(name: "dbo.ProductAnbarCategories", newName: "AnbarCategoryProducts");
        }
    }
}
