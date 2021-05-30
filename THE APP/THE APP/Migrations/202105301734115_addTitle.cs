namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostModels", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostModels", "Title");
        }
    }
}
