namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproposalsatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProposalModels", "status", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProposalModels", "status");
        }
    }
}
