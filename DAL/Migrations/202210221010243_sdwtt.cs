namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdwtt : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CategoriNoeHesabBanks", newName: "HesabBankCategoriNoes");
            DropPrimaryKey("dbo.HesabBankCategoriNoes");
            AddColumn("dbo.BuyPardakhts", "CategoriNoe_id", c => c.Int());
            AddColumn("dbo.SellDaryafts", "CategoriNoe_id", c => c.Int());
            AddPrimaryKey("dbo.HesabBankCategoriNoes", new[] { "HesabBank_id", "CategoriNoe_id" });
            CreateIndex("dbo.BuyPardakhts", "CategoriNoe_id");
            CreateIndex("dbo.SellDaryafts", "CategoriNoe_id");
            AddForeignKey("dbo.SellDaryafts", "CategoriNoe_id", "dbo.CategoriNoes", "id");
            AddForeignKey("dbo.BuyPardakhts", "CategoriNoe_id", "dbo.CategoriNoes", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BuyPardakhts", "CategoriNoe_id", "dbo.CategoriNoes");
            DropForeignKey("dbo.SellDaryafts", "CategoriNoe_id", "dbo.CategoriNoes");
            DropIndex("dbo.SellDaryafts", new[] { "CategoriNoe_id" });
            DropIndex("dbo.BuyPardakhts", new[] { "CategoriNoe_id" });
            DropPrimaryKey("dbo.HesabBankCategoriNoes");
            DropColumn("dbo.SellDaryafts", "CategoriNoe_id");
            DropColumn("dbo.BuyPardakhts", "CategoriNoe_id");
            AddPrimaryKey("dbo.HesabBankCategoriNoes", new[] { "CategoriNoe_id", "HesabBank_id" });
            RenameTable(name: "dbo.HesabBankCategoriNoes", newName: "CategoriNoeHesabBanks");
        }
    }
}
