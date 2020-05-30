using System.Collections.Generic;
using Computer.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Computer.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Computer.Data.ComputerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Computer.Data.ComputerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            CreateConfigTitle(context);
            CreateUser(context);
            CreateFunction(context);

        }

        private void CreateFunction(ComputerDbContext context)
        {
            if (!context.Functions.Any())
            {
                context.Functions.AddRange(new List<Function>()
                {
                    new Function() {ID = "SYSTEM", Name = "Hệ thống",ParentId = null,DisplayOrder = 1,Status = true,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {ID = "ROLE", Name = "Nhóm",ParentId = "SYSTEM",DisplayOrder = 1,Status = true,URL = "/main/role/index",IconCss = "fa-home"  },
                    new Function() {ID = "FUNCTION", Name = "Chức năng",ParentId = "SYSTEM",DisplayOrder = 2,Status = true,URL = "/main/function/index",IconCss = "fa-home"  },
                    new Function() {ID = "USER", Name = "Người dùng",ParentId = "SYSTEM",DisplayOrder =3,Status = true,URL = "/main/user/index",IconCss = "fa-home"  },
                    new Function() {ID = "ERROR", Name = "Lỗi",ParentId = "SYSTEM",DisplayOrder = 4,Status = true,URL = "/main/error/index",IconCss = "fa-home"  },
                    new Function() {ID = "SETTING", Name = "Cấu hình",ParentId = "SYSTEM",DisplayOrder = 5,Status = true,URL = "/main/setting/index",IconCss = "fa-home"  },
                    new Function() {ID = "ANNOUNCEMENT",Name = "Thông báo",ParentId = "SYSTEM",DisplayOrder = 6,Status = true,URL = "/main/announcement/index",IconCss = "fa-clone"  },

                    new Function() {ID = "COMPUTER_MANAGEMENT",Name = "Quản lý máy tính",ParentId = null,DisplayOrder = 2,Status = true,URL = "/",IconCss = "fa-laptop-house"  },
                    new Function() {ID = "COMPUTER",Name = "Máy tính",ParentId = "COMPUTER_MANAGEMENT",DisplayOrder = 1,Status = true,URL = "/main/computer/computer",IconCss = "fa-desktop"  },
                    new Function() {ID = "COMPUTER_TYPE",Name = "Loại máy tính",ParentId = "COMPUTER_MANAGEMENT",DisplayOrder = 2,Status = true,URL = "/main/computer/computer-type",IconCss = "fa-laptop-code"  },
                    new Function() {ID = "PRODUCER_TYPE",Name = "Nhà sản xuất",ParentId = "COMPUTER_MANAGEMENT",DisplayOrder = 3,Status = true,URL = "/main/computer/producer-type",IconCss = "fa-laptop"  },
                    new Function() {ID = "DEPARMENT_TYPE",Name = "Phòng ban",ParentId = "COMPUTER_MANAGEMENT",DisplayOrder = 4,Status = true,URL = "/main/computer/deparment-type",IconCss = "fa-network-wired"  },
                    new Function() {ID = "COMPUTER_HISTORY",Name = "Lịch sử sử dụng",ParentId = "COMPUTER_MANAGEMENT",DisplayOrder = 5,Status = true,URL = "/main/computer/computer-history",IconCss = "fa-tv"  },
                    new Function() {ID = "USING_REPORT",Name = "Báo cáo sử dụng",ParentId = "COMPUTER_MANAGEMENT",DisplayOrder = 6,Status = true,URL = "/main/computer/using-report",IconCss = "fa-database"  },

                });
                context.SaveChanges();
            }
        }

        private void CreateConfigTitle(ComputerDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "HomeTitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ Computer",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ Computer",
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ Computer",
                });
            }
        }

        private void CreateUser(ComputerDbContext context)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(new ComputerDbContext()));
            if (!manager.Users.Any())
            {
                var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(new ComputerDbContext()));

                var user = new AppUser()
                {
                    UserName = "phamanhminh@gmail.com",
                    Email = "phamanhminh@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.Now,
                    FullName = "Pham Anh Minh",
                    Avatar = "/assets/images/img.jpg",
                    Gender = true,
                    Status = true
                };
                if (manager.Users.Count(x => x.UserName == "admin") == 0)
                {
                    manager.Create(user, "123654$");

                    if (!roleManager.Roles.Any())
                    {
                        roleManager.Create(new AppRole { Name = "Admin", Description = "Quản trị viên" });
                        roleManager.Create(new AppRole { Name = "Member", Description = "Người dùng" });
                    }

                    var adminUser = manager.FindByName("admin");

                    manager.AddToRoles(adminUser.Id, new string[] { "Admin", "Member" });
                }
            }
        }
    }
}
