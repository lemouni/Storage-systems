namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CategoryNameP = c.String(),
                        DeleteStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Products", "ProductCategory_id", c => c.Int());
            CreateIndex("dbo.Products", "ProductCategory_id");
            AddForeignKey("dbo.Products", "ProductCategory_id", "dbo.ProductCategories", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductCategory_id", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "ProductCategory_id" });
            DropColumn("dbo.Products", "ProductCategory_id");
            DropTable("dbo.ProductCategories");
        }
    }
}
