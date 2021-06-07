namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpostsrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostsRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        FreelancerId = c.String(maxLength: 128),
                        Rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.FreelancerId, cascadeDelete: true)
                .ForeignKey("dbo.PostModels", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId)
                .Index(t => t.FreelancerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostsRates", "PostId", "dbo.PostModels");
            DropForeignKey("dbo.PostsRates", "FreelancerId", "dbo.AspNetUsers");
            DropIndex("dbo.PostsRates", new[] { "FreelancerId" });
            DropIndex("dbo.PostsRates", new[] { "PostId" });
            DropTable("dbo.PostsRates");
        }
    }
}
