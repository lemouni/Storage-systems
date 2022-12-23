namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdwqq : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MoshkhasatFactors",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        NameForoshgah = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MoshkhasatFactors");
        }
    }
}
