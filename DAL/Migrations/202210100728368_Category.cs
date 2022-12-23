namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Products", name: "ProductCategory_id", newName: "Category_id");
            RenameIndex(table: "dbo.Products", name: "IX_ProductCategory_id", newName: "IX_Category_id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_Category_id", newName: "IX_ProductCategory_id");
            RenameColumn(table: "dbo.Products", name: "Category_id", newName: "ProductCategory_id");
        }
    }
}
