namespace Computer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AnnouncementUsers",
                c => new
                    {
                        AnnouncementId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        HasRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnnouncementId, t.UserId })
                .ForeignKey("dbo.Announcements", t => t.AnnouncementId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.AnnouncementId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Avatar = c.String(),
                        BirthDay = c.DateTime(),
                        Status = c.Boolean(),
                        Gender = c.Boolean(),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AppUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AppUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.AppRoles", t => t.IdentityRole_Id)
                .Index(t => t.AppUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.AppRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Computers",
                c => new
                    {
                        ComputerId = c.Int(nullable: false, identity: true),
                        ComputerCode = c.String(maxLength: 10),
                        ComputerName = c.String(maxLength: 50),
                        ComputerDescription = c.String(maxLength: 250),
                        ComputerTypeId = c.Int(nullable: false),
                        ProducerTypeId = c.Int(nullable: false),
                        DeparmentTypeId = c.Int(nullable: false),
                        IsBusyNow = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ComputerId)
                .ForeignKey("dbo.ComputerTypes", t => t.ComputerTypeId, cascadeDelete: true)
                .ForeignKey("dbo.DeparmentTypes", t => t.DeparmentTypeId, cascadeDelete: true)
                .ForeignKey("dbo.ProducerTypes", t => t.ProducerTypeId, cascadeDelete: true)
                .Index(t => t.ComputerTypeId)
                .Index(t => t.ProducerTypeId)
                .Index(t => t.DeparmentTypeId);
            
            CreateTable(
                "dbo.ComputerTypes",
                c => new
                    {
                        ComputerTypeId = c.Int(nullable: false, identity: true),
                        ComputerTypeCode = c.String(maxLength: 10),
                        ComputerTypeName = c.String(maxLength: 50),
                        ComputerTypeDescription = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ComputerTypeId);
            
            CreateTable(
                "dbo.DeparmentTypes",
                c => new
                    {
                        DeparmentTypeId = c.Int(nullable: false, identity: true),
                        DeparmentTypeCode = c.String(maxLength: 10),
                        DeparmentTypeName = c.String(maxLength: 50),
                        DeparmentTypeDescription = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DeparmentTypeId);
            
            CreateTable(
                "dbo.ProducerTypes",
                c => new
                    {
                        ProducerTypeId = c.Int(nullable: false, identity: true),
                        ProducerTypeCode = c.String(maxLength: 10),
                        ProducerTypeName = c.String(maxLength: 50),
                        ProducerTypeDescription = c.String(maxLength: 250),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ProducerTypeId);
            
            CreateTable(
                "dbo.ComputerUsingHistories",
                c => new
                    {
                        ComputerUsingHistoryId = c.Int(nullable: false, identity: true),
                        ComputerId = c.Int(nullable: false),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ComputerUsingHistoryId)
                .ForeignKey("dbo.Computers", t => t.ComputerId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ComputerId)
                .Index(t => t.UserId);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Functions", t => t.ParentId)
                .Index(t => t.ParentId);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppRoles", t => t.RoleId)
                .ForeignKey("dbo.Functions", t => t.FunctionId)
                .Index(t => t.RoleId)
                .Index(t => t.FunctionId);
            
            CreateTable(
                "dbo.SystemConfigs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        ValueString = c.String(maxLength: 50),
                        ValueInt = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppUserRoles", "IdentityRole_Id", "dbo.AppRoles");
            DropForeignKey("dbo.Permissions", "FunctionId", "dbo.Functions");
            DropForeignKey("dbo.Permissions", "RoleId", "dbo.AppRoles");
            DropForeignKey("dbo.Functions", "ParentId", "dbo.Functions");
            DropForeignKey("dbo.ComputerUsingHistories", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.ComputerUsingHistories", "ComputerId", "dbo.Computers");
            DropForeignKey("dbo.Computers", "ProducerTypeId", "dbo.ProducerTypes");
            DropForeignKey("dbo.Computers", "DeparmentTypeId", "dbo.DeparmentTypes");
            DropForeignKey("dbo.Computers", "ComputerTypeId", "dbo.ComputerTypes");
            DropForeignKey("dbo.Announcements", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.AnnouncementUsers", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserRoles", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserLogins", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserClaims", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AnnouncementUsers", "AnnouncementId", "dbo.Announcements");
            DropIndex("dbo.Permissions", new[] { "FunctionId" });
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropIndex("dbo.Functions", new[] { "ParentId" });
            DropIndex("dbo.ComputerUsingHistories", new[] { "UserId" });
            DropIndex("dbo.ComputerUsingHistories", new[] { "ComputerId" });
            DropIndex("dbo.Computers", new[] { "DeparmentTypeId" });
            DropIndex("dbo.Computers", new[] { "ProducerTypeId" });
            DropIndex("dbo.Computers", new[] { "ComputerTypeId" });
            DropIndex("dbo.AppUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AppUserRoles", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserLogins", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserClaims", new[] { "AppUser_Id" });
            DropIndex("dbo.AnnouncementUsers", new[] { "UserId" });
            DropIndex("dbo.AnnouncementUsers", new[] { "AnnouncementId" });
            DropIndex("dbo.Announcements", new[] { "UserId" });
            DropTable("dbo.SystemConfigs");
            DropTable("dbo.Permissions");
            DropTable("dbo.Functions");
            DropTable("dbo.ComputerUsingHistories");
            DropTable("dbo.ProducerTypes");
            DropTable("dbo.DeparmentTypes");
            DropTable("dbo.ComputerTypes");
            DropTable("dbo.Computers");
            DropTable("dbo.AppRoles");
            DropTable("dbo.AppUserRoles");
            DropTable("dbo.AppUserLogins");
            DropTable("dbo.AppUserClaims");
            DropTable("dbo.AppUsers");
            DropTable("dbo.AnnouncementUsers");
            DropTable("dbo.Announcements");
        }
    }
}
