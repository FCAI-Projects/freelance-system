namespace THE_APP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAdminRole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd4f10aa5-2629-416d-bb24-008f0291214d', N'admin')
    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'f19e8514-7559-4217-8700-5951bfc91ff2', N'client')
    INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'89b363fd-e33c-4715-94ca-abc2188cc214', N'freelancer')

    INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Fname], [Lname]) VALUES (N'a5ae82ff-fb02-4384-9646-4599df0e7b66', N'admin@support.com', 0, N'AFOFhinOMYBrDQHcoWeQ2CCGVqL6mCgtzLP7+OA80FDvCFNmnMiYeQUlSExWXmezhQ==', N'0209d145-67f8-43f7-ade8-a6a956b87ed7', NULL, 0, 0, NULL, 1, 0, N'admin@support.com', N'Ezzdin', N'Atef')

    INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a5ae82ff-fb02-4384-9646-4599df0e7b66', N'd4f10aa5-2629-416d-bb24-008f0291214d')
    ");

        }
        
        public override void Down()
        {
        }
    }
}
