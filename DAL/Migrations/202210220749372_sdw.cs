namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdw : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HesabBanks", "CategoriNoe_id", "dbo.CategoriNoes");
            DropIndex("dbo.HesabBanks", new[] { "CategoriNoe_id" });
            CreateTable(
                "dbo.CategoriNoeHesabBanks",
                c => new
                    {
                        CategoriNoe_id = c.Int(nullable: false),
                        HesabBank_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CategoriNoe_id, t.HesabBank_id })
                .ForeignKey("dbo.CategoriNoes", t => t.CategoriNoe_id, cascadeDelete: true)
                .ForeignKey("dbo.HesabBanks", t => t.HesabBank_id, cascadeDelete: true)
                .Index(t => t.CategoriNoe_id)
                .Index(t => t.HesabBank_id);
            
            DropColumn("dbo.HesabBanks", "CategoriNoe_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HesabBanks", "CategoriNoe_id", c => c.Int());
            DropForeignKey("dbo.CategoriNoeHesabBanks", "HesabBank_id", "dbo.HesabBanks");
            DropForeignKey("dbo.CategoriNoeHesabBanks", "CategoriNoe_id", "dbo.CategoriNoes");
            DropIndex("dbo.CategoriNoeHesabBanks", new[] { "HesabBank_id" });
            DropIndex("dbo.CategoriNoeHesabBanks", new[] { "CategoriNoe_id" });
            DropTable("dbo.CategoriNoeHesabBanks");
            CreateIndex("dbo.HesabBanks", "CategoriNoe_id");
            AddForeignKey("dbo.HesabBanks", "CategoriNoe_id", "dbo.CategoriNoes", "id");
        }
    }
}
