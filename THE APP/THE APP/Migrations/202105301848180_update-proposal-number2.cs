namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateproposalnumber2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostModels", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostModels", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
