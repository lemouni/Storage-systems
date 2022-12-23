namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CountProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountProducts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        count = c.Int(nullable: false),
                        invoiceC_id = c.Int(),
                        producC_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Invoices", t => t.invoiceC_id)
                .ForeignKey("dbo.Products", t => t.producC_id)
                .Index(t => t.invoiceC_id)
                .Index(t => t.producC_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CountProducts", "producC_id", "dbo.Products");
            DropForeignKey("dbo.CountProducts", "invoiceC_id", "dbo.Invoices");
            DropIndex("dbo.CountProducts", new[] { "producC_id" });
            DropIndex("dbo.CountProducts", new[] { "invoiceC_id" });
            DropTable("dbo.CountProducts");
        }
    }
}
