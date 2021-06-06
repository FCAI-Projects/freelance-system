namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewcolumninposts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostModels", "AccpeptProposal", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostModels", "AccpeptProposal");
        }
    }
}
