namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Newpost : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostModels", "CreationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostModels", "CreationDate", c => c.DateTime(nullable: false));
        }
    }
}
