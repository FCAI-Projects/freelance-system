namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatetime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PostModels", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.PostModels", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostModels", "CreationDate", c => c.DateTime());
            AlterColumn("dbo.PostModels", "Type", c => c.Int(nullable: false));
        }
    }
}
