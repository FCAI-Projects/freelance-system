namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproposal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProposalModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Proposal = c.String(),
                        FreelancerId = c.String(maxLength: 128),
                        PostId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.FreelancerId)
                .ForeignKey("dbo.PostModels", t => t.PostId, cascadeDelete: true)
                .Index(t => t.FreelancerId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProposalModels", "PostId", "dbo.PostModels");
            DropForeignKey("dbo.ProposalModels", "FreelancerId", "dbo.AspNetUsers");
            DropIndex("dbo.ProposalModels", new[] { "PostId" });
            DropIndex("dbo.ProposalModels", new[] { "FreelancerId" });
            DropTable("dbo.ProposalModels");
        }
    }
}
