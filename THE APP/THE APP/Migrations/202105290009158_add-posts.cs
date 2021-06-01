namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addposts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Budget = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        ProposalNum = c.Int(nullable: false),
                        isAccepted = c.Boolean(nullable: true),
                        Client_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Client_Id)
                .Index(t => t.Client_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostModels", "Client_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PostModels", new[] { "Client_Id" });
            DropTable("dbo.PostModels");
        }
    }
}
