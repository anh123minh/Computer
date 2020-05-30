using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Computer.Model.Models;

namespace Computer.Data
{
    public class ComputerDbContext : IdentityDbContext<AppUser>
    {
        public ComputerDbContext() : base("ComputerConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Model.Models.Computer> Computers { set; get; }
        public DbSet<ComputerType> ComputerTypes { set; get; }
        public DbSet<ProducerType> ProducerTypes { set; get; }
        public DbSet<DeparmentType> DeparmentTypes { set; get; }
        public DbSet<ComputerUsingHistory> ComputerUsingHistories { set; get; }

        public DbSet<SystemConfig> SystemConfigs { set; get; }
        public DbSet<Error> Errors { set; get; }
        
        public DbSet<Function> Functions { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<AppRole> AppRoles { set; get; }
        public DbSet<IdentityUserRole> UserRoles { set; get; }

        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

        public static ComputerDbContext Create()
        {
            return new ComputerDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasKey<string>(r => r.Id).ToTable("AppRoles");
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("AppUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("AppUserLogins");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("AppUserClaims");
        }
    }
}