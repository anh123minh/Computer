namespace Computer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedDb : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AppUsers", newName: "ApplicationUsers");
            RenameTable(name: "dbo.AppUserClaims", newName: "ApplicationUserClaims");
            RenameTable(name: "dbo.AppUserLogins", newName: "ApplicationUserLogins");
            RenameTable(name: "dbo.AppUserRoles", newName: "ApplicationUserRoles");
            RenameTable(name: "dbo.AppRoles", newName: "ApplicationRoles");
            DropForeignKey("dbo.AnnouncementUsers", "AnnouncementId", "dbo.Announcements");
            DropForeignKey("dbo.AnnouncementUsers", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Announcements", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.ComputerUsingHistories", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.Functions", "ParentId", "dbo.Functions");
            DropForeignKey("dbo.Permissions", "RoleId", "dbo.AppRoles");
            DropForeignKey("dbo.Permissions", "FunctionId", "dbo.Functions");
            DropIndex("dbo.Announcements", new[] { "UserId" });
            DropIndex("dbo.AnnouncementUsers", new[] { "AnnouncementId" });
            DropIndex("dbo.AnnouncementUsers", new[] { "UserId" });
            DropIndex("dbo.ComputerUsingHistories", new[] { "UserId" });
            DropIndex("dbo.Functions", new[] { "ParentId" });
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropIndex("dbo.Permissions", new[] { "FunctionId" });
            RenameColumn(table: "dbo.ApplicationUserClaims", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.ApplicationUserLogins", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.ApplicationUserRoles", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameIndex(table: "dbo.ApplicationUserRoles", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.ApplicationUserClaims", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.ApplicationUserLogins", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            CreateTable(
                "dbo.Errors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.ApplicationRoles", "Description", c => c.String(maxLength: 250));
            DropColumn("dbo.ApplicationUsers", "Avatar");
            DropColumn("dbo.ApplicationUsers", "Status");
            DropColumn("dbo.ApplicationUsers", "Gender");
            DropTable("dbo.Announcements");
            DropTable("dbo.AnnouncementUsers");
            DropTable("dbo.Functions");
            DropTable("dbo.Permissions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        FunctionId = c.String(maxLength: 50, unicode: false),
                        CanCreate = c.Boolean(nullable: false),
                        CanRead = c.Boolean(nullable: false),
                        CanUpdate = c.Boolean(nullable: false),
                        CanDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        URL = c.String(nullable: false, maxLength: 256),
                        DisplayOrder = c.Int(nullable: false),
                        ParentId = c.String(maxLength: 50, unicode: false),
                        Status = c.Boolean(nullable: false),
                        IconCss = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AnnouncementUsers",
                c => new
                    {
                        AnnouncementId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        HasRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnnouncementId, t.UserId });
            
            CreateTable(
                "dbo.Announcements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Content = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.ApplicationUsers", "Gender", c => c.Boolean());
            AddColumn("dbo.ApplicationUsers", "Status", c => c.Boolean());
            AddColumn("dbo.ApplicationUsers", "Avatar", c => c.String());
            AlterColumn("dbo.ApplicationRoles", "Description", c => c.String());
            DropTable("dbo.Errors");
            RenameIndex(table: "dbo.ApplicationUserLogins", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            RenameIndex(table: "dbo.ApplicationUserClaims", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            RenameIndex(table: "dbo.ApplicationUserRoles", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            RenameColumn(table: "dbo.ApplicationUserRoles", name: "ApplicationUser_Id", newName: "AppUser_Id");
            RenameColumn(table: "dbo.ApplicationUserLogins", name: "ApplicationUser_Id", newName: "AppUser_Id");
            RenameColumn(table: "dbo.ApplicationUserClaims", name: "ApplicationUser_Id", newName: "AppUser_Id");
            CreateIndex("dbo.Permissions", "FunctionId");
            CreateIndex("dbo.Permissions", "RoleId");
            CreateIndex("dbo.Functions", "ParentId");
            CreateIndex("dbo.ComputerUsingHistories", "UserId");
            CreateIndex("dbo.AnnouncementUsers", "UserId");
            CreateIndex("dbo.AnnouncementUsers", "AnnouncementId");
            CreateIndex("dbo.Announcements", "UserId");
            AddForeignKey("dbo.Permissions", "FunctionId", "dbo.Functions", "ID");
            AddForeignKey("dbo.Permissions", "RoleId", "dbo.AppRoles", "Id");
            AddForeignKey("dbo.Functions", "ParentId", "dbo.Functions", "ID");
            AddForeignKey("dbo.ComputerUsingHistories", "UserId", "dbo.AppUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Announcements", "UserId", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.AnnouncementUsers", "UserId", "dbo.AppUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AnnouncementUsers", "AnnouncementId", "dbo.Announcements", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.ApplicationRoles", newName: "AppRoles");
            RenameTable(name: "dbo.ApplicationUserRoles", newName: "AppUserRoles");
            RenameTable(name: "dbo.ApplicationUserLogins", newName: "AppUserLogins");
            RenameTable(name: "dbo.ApplicationUserClaims", newName: "AppUserClaims");
            RenameTable(name: "dbo.ApplicationUsers", newName: "AppUsers");
        }
    }
}
