namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dsa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountProducts", "MasolSabt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CountProducts", "MasolSabt");
        }
    }
}
