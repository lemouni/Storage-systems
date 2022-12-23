namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class daggg : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApiSms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserApiKey = c.String(),
                        SecretKey = c.String(),
                        Khat = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Sms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Payam = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sms");
            DropTable("dbo.ApiSms");
        }
    }
}
