namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductAnbarCategories", newName: "AnbarCategoryProducts");
            DropForeignKey("dbo.CountProducts", "productanbarC_id", "dbo.CounProductInAnbars");
            DropIndex("dbo.CountProducts", new[] { "productanbarC_id" });
            RenameColumn(table: "dbo.CountProducts", name: "Product_id", newName: "producC_id");
            RenameIndex(table: "dbo.CountProducts", name: "IX_Product_id", newName: "IX_producC_id");
            DropPrimaryKey("dbo.AnbarCategoryProducts");
            AddPrimaryKey("dbo.AnbarCategoryProducts", new[] { "AnbarCategory_id", "Product_id" });
            DropColumn("dbo.CountProducts", "productanbarC_id");
            DropColumn("dbo.CounProductInAnbars", "Price");
            DropColumn("dbo.CounProductInAnbars", "Price1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CounProductInAnbars", "Price1", c => c.Double(nullable: false));
            AddColumn("dbo.CounProductInAnbars", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.CountProducts", "productanbarC_id", c => c.Int());
            DropPrimaryKey("dbo.AnbarCategoryProducts");
            AddPrimaryKey("dbo.AnbarCategoryProducts", new[] { "Product_id", "AnbarCategory_id" });
            RenameIndex(table: "dbo.CountProducts", name: "IX_producC_id", newName: "IX_Product_id");
            RenameColumn(table: "dbo.CountProducts", name: "producC_id", newName: "Product_id");
            CreateIndex("dbo.CountProducts", "productanbarC_id");
            AddForeignKey("dbo.CountProducts", "productanbarC_id", "dbo.CounProductInAnbars", "id");
            RenameTable(name: "dbo.AnbarCategoryProducts", newName: "ProductAnbarCategories");
        }
    }
}
