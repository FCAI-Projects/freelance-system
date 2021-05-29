namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfristlastname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Fname", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Lname", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Lname");
            DropColumn("dbo.AspNetUsers", "Fname");
        }
    }
}
