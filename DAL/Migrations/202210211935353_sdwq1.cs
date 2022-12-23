    namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdwq1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CategoriNoes", "DeleteStatus", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CategoriNoes", "DeleteStatus");
        }
    }
}
