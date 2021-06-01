namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclientid : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.PostModels", name: "Client_Id", newName: "ClientId");
            RenameIndex(table: "dbo.PostModels", name: "IX_Client_Id", newName: "IX_ClientId");
            AlterColumn("dbo.PostModels", "isAccepted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PostModels", "isAccepted", c => c.Boolean(nullable: false));
            RenameIndex(table: "dbo.PostModels", name: "IX_ClientId", newName: "IX_Client_Id");
            RenameColumn(table: "dbo.PostModels", name: "ClientId", newName: "Client_Id");
        }
    }
}
