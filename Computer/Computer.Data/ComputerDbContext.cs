using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Computer.Model.Models;

namespace Computer.Data
{
    public class ComputerDbContext : IdentityDbContext<ApplicationUser>
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

        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public static ComputerDbContext Create()
        {
            return new ComputerDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            builder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
        }
    }
}