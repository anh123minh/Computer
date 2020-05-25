namespace Computer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedDb2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationGroups", newName: "Sizes");
            RenameTable(name: "dbo.ApplicationRoles", newName: "AppRoles");
            RenameTable(name: "dbo.ApplicationUserRoles", newName: "AppUserRoles");
            RenameTable(name: "dbo.ApplicationUsers", newName: "AppUsers");
            RenameTable(name: "dbo.ApplicationUserClaims", newName: "AppUserClaims");
            RenameTable(name: "dbo.ApplicationUserLogins", newName: "AppUserLogins");
            DropForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles");
            DropForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.ApplicationGroups");
            DropForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.ApplicationUsers");
            DropIndex("dbo.ApplicationRoleGroups", new[] { "GroupId" });
            DropIndex("dbo.ApplicationRoleGroups", new[] { "RoleId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "UserId" });
            DropIndex("dbo.ApplicationUserGroups", new[] { "GroupId" });
            RenameColumn(table: "dbo.AppUserClaims", name: "ApplicationUser_Id", newName: "AppUser_Id");
            RenameColumn(table: "dbo.AppUserLogins", name: "ApplicationUser_Id", newName: "AppUser_Id");
            RenameColumn(table: "dbo.AppUserRoles", name: "ApplicationUser_Id", newName: "AppUser_Id");
            RenameIndex(table: "dbo.AppUserClaims", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            RenameIndex(table: "dbo.AppUserLogins", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
            RenameIndex(table: "dbo.AppUserRoles", name: "IX_ApplicationUser_Id", newName: "IX_AppUser_Id");
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
                "dbo.Colors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 250),
                        Code = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ContactDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 250),
                        Website = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        Other = c.String(),
                        Lat = c.Double(),
                        Lng = c.Double(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Email = c.String(maxLength: 250),
                        Message = c.String(maxLength: 500),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Footers",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50),
                        Content = c.String(nullable: false),
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Functions", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        OrderID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ColorId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderID, t.ProductID })
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID)
                .Index(t => t.ColorId)
                .Index(t => t.SizeId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 256),
                        CustomerAddress = c.String(nullable: false, maxLength: 256),
                        CustomerEmail = c.String(nullable: false, maxLength: 256),
                        CustomerMobile = c.String(nullable: false, maxLength: 50),
                        CustomerMessage = c.String(nullable: false, maxLength: 256),
                        PaymentMethod = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        PaymentStatus = c.String(),
                        Status = c.Boolean(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AppUsers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        CategoryID = c.Int(nullable: false),
                        ThumbnailImage = c.String(maxLength: 256),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OriginalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PromotionPrice = c.Decimal(precision: 18, scale: 2),
                        IncludedVAT = c.Boolean(nullable: false),
                        Warranty = c.Int(),
                        Description = c.String(maxLength: 500),
                        Content = c.String(),
                        HomeFlag = c.Boolean(),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        Tags = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        HomeOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.TagID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Content = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.PostCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        CategoryID = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        Description = c.String(maxLength: 500),
                        Content = c.String(),
                        HomeFlag = c.Boolean(),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PostCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        PostID = c.Int(nullable: false),
                        TagID = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => new { t.PostID, t.TagID })
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Path = c.String(maxLength: 250),
                        Caption = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductQuantities",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.SizeId, t.ColorId })
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.SizeId)
                .Index(t => t.ColorId);
            
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 256),
                        Image = c.String(maxLength: 256),
                        Url = c.String(maxLength: 256),
                        DisplayOrder = c.Int(),
                        Status = c.Boolean(nullable: false),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SupportOnlines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Department = c.String(maxLength: 50),
                        Skype = c.String(maxLength: 50),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Yahoo = c.String(maxLength: 50),
                        Facebook = c.String(maxLength: 50),
                        Status = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VisitorStatistics",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        VisitedDate = c.DateTime(nullable: false),
                        IPAddress = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AppUsers", "Avatar", c => c.String());
            AddColumn("dbo.AppUsers", "Status", c => c.Boolean());
            AddColumn("dbo.AppUsers", "Gender", c => c.Boolean());
            AlterColumn("dbo.AppRoles", "Description", c => c.String());
            DropColumn("dbo.Sizes", "Description");
            DropTable("dbo.ApplicationRoleGroups");
            DropTable("dbo.ApplicationUserGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserGroups",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId });
            
            CreateTable(
                "dbo.ApplicationRoleGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupId, t.RoleId });
            
            AddColumn("dbo.Sizes", "Description", c => c.String(maxLength: 250));
            DropForeignKey("dbo.ProductQuantities", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.ProductQuantities", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductQuantities", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.ProductImages", "ProductId", "dbo.Products");
            DropForeignKey("dbo.PostTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories");
            DropForeignKey("dbo.Permissions", "FunctionId", "dbo.Functions");
            DropForeignKey("dbo.Permissions", "RoleId", "dbo.AppRoles");
            DropForeignKey("dbo.OrderDetails", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.AppUsers");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "ColorId", "dbo.Colors");
            DropForeignKey("dbo.Functions", "ParentId", "dbo.Functions");
            DropForeignKey("dbo.Announcements", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.AnnouncementUsers", "UserId", "dbo.AppUsers");
            DropForeignKey("dbo.AnnouncementUsers", "AnnouncementId", "dbo.Announcements");
            DropIndex("dbo.ProductQuantities", new[] { "ColorId" });
            DropIndex("dbo.ProductQuantities", new[] { "SizeId" });
            DropIndex("dbo.ProductQuantities", new[] { "ProductId" });
            DropIndex("dbo.ProductImages", new[] { "ProductId" });
            DropIndex("dbo.PostTags", new[] { "TagID" });
            DropIndex("dbo.PostTags", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.Permissions", new[] { "FunctionId" });
            DropIndex("dbo.Permissions", new[] { "RoleId" });
            DropIndex("dbo.ProductTags", new[] { "TagID" });
            DropIndex("dbo.ProductTags", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderDetails", new[] { "SizeId" });
            DropIndex("dbo.OrderDetails", new[] { "ColorId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Functions", new[] { "ParentId" });
            DropIndex("dbo.AnnouncementUsers", new[] { "UserId" });
            DropIndex("dbo.AnnouncementUsers", new[] { "AnnouncementId" });
            DropIndex("dbo.Announcements", new[] { "UserId" });
            AlterColumn("dbo.AppRoles", "Description", c => c.String(maxLength: 250));
            DropColumn("dbo.AppUsers", "Gender");
            DropColumn("dbo.AppUsers", "Status");
            DropColumn("dbo.AppUsers", "Avatar");
            DropTable("dbo.VisitorStatistics");
            DropTable("dbo.SupportOnlines");
            DropTable("dbo.Slides");
            DropTable("dbo.ProductQuantities");
            DropTable("dbo.ProductImages");
            DropTable("dbo.PostTags");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Permissions");
            DropTable("dbo.Pages");
            DropTable("dbo.Tags");
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Functions");
            DropTable("dbo.Footers");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.ContactDetails");
            DropTable("dbo.Colors");
            DropTable("dbo.AnnouncementUsers");
            DropTable("dbo.Announcements");
            RenameIndex(table: "dbo.AppUserRoles", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.AppUserLogins", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.AppUserClaims", name: "IX_AppUser_Id", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.AppUserRoles", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.AppUserLogins", name: "AppUser_Id", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.AppUserClaims", name: "AppUser_Id", newName: "ApplicationUser_Id");
            CreateIndex("dbo.ApplicationUserGroups", "GroupId");
            CreateIndex("dbo.ApplicationUserGroups", "UserId");
            CreateIndex("dbo.ApplicationRoleGroups", "RoleId");
            CreateIndex("dbo.ApplicationRoleGroups", "GroupId");
            AddForeignKey("dbo.ApplicationUserGroups", "UserId", "dbo.ApplicationUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserGroups", "GroupId", "dbo.ApplicationGroups", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationRoleGroups", "RoleId", "dbo.ApplicationRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationRoleGroups", "GroupId", "dbo.ApplicationGroups", "ID", cascadeDelete: true);
            RenameTable(name: "dbo.AppUserLogins", newName: "ApplicationUserLogins");
            RenameTable(name: "dbo.AppUserClaims", newName: "ApplicationUserClaims");
            RenameTable(name: "dbo.AppUsers", newName: "ApplicationUsers");
            RenameTable(name: "dbo.AppUserRoles", newName: "ApplicationUserRoles");
            RenameTable(name: "dbo.AppRoles", newName: "ApplicationRoles");
            RenameTable(name: "dbo.Sizes", newName: "ApplicationGroups");
        }
    }
}
