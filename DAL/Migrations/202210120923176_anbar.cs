namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anbar : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnbarCategories",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        AnbarName = c.String(),
                        AnbarAdress = c.String(),
                        DeleteStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.AnbarCategoryProducts",
                c => new
                    {
                        AnbarCategory_id = c.Int(nullable: false),
                        Product_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnbarCategory_id, t.Product_id })
                .ForeignKey("dbo.AnbarCategories", t => t.AnbarCategory_id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_id, cascadeDelete: true)
                .Index(t => t.AnbarCategory_id)
                .Index(t => t.Product_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnbarCategoryProducts", "Product_id", "dbo.Products");
            DropForeignKey("dbo.AnbarCategoryProducts", "AnbarCategory_id", "dbo.AnbarCategories");
            DropIndex("dbo.AnbarCategoryProducts", new[] { "Product_id" });
            DropIndex("dbo.AnbarCategoryProducts", new[] { "AnbarCategory_id" });
            DropTable("dbo.AnbarCategoryProducts");
            DropTable("dbo.AnbarCategories");
        }
    }
}
