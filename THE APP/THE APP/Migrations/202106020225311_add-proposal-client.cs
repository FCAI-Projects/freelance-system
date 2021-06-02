namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproposalclient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProposalModels", "ClientId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ProposalModels", "ClientId");
            AddForeignKey("dbo.ProposalModels", "ClientId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProposalModels", "ClientId", "dbo.AspNetUsers");
            DropIndex("dbo.ProposalModels", new[] { "ClientId" });
            DropColumn("dbo.ProposalModels", "ClientId");
        }
    }
}
