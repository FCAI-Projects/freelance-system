namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class savedposts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavedPostsModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FreelancerId = c.String(maxLength: 128),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.FreelancerId, cascadeDelete: true)
                .ForeignKey("dbo.PostModels", t => t.PostId, cascadeDelete: true)
                .Index(t => t.FreelancerId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedPostsModels", "PostId", "dbo.PostModels");
            DropForeignKey("dbo.SavedPostsModels", "FreelancerId", "dbo.AspNetUsers");
            DropIndex("dbo.SavedPostsModels", new[] { "PostId" });
            DropIndex("dbo.SavedPostsModels", new[] { "FreelancerId" });
            DropTable("dbo.SavedPostsModels");
        }
    }
}
