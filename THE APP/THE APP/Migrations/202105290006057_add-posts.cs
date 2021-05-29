namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addposts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostModels", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostModels", "Type", c => c.String());
        }
    }
}
