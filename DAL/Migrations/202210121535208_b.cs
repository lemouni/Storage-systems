namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class b : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CounProductInAnbars",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        count = c.Int(nullable: false),
                        anbarCategoryP_id = c.Int(),
                        productP_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AnbarCategories", t => t.anbarCategoryP_id)
                .ForeignKey("dbo.Products", t => t.productP_id)
                .Index(t => t.anbarCategoryP_id)
                .Index(t => t.productP_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CounProductInAnbars", "productP_id", "dbo.Products");
            DropForeignKey("dbo.CounProductInAnbars", "anbarCategoryP_id", "dbo.AnbarCategories");
            DropIndex("dbo.CounProductInAnbars", new[] { "productP_id" });
            DropIndex("dbo.CounProductInAnbars", new[] { "anbarCategoryP_id" });
            DropTable("dbo.CounProductInAnbars");
        }
    }
}
