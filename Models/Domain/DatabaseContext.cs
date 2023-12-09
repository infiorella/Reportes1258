using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Models.Domain
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser>
    {
        //public DbSet<PersonalEducativo> PersonalEducativo { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) 
        {
        
        }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);

			builder.Entity<ApplicationUser>(entity =>
			{
				entity.ToTable(name: "Users");
			});

			builder.Entity<IdentityRole>(entity =>
			{
				entity.ToTable(name: "Role");
			});
			builder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.ToTable("UserRoles");
				//in case you chagned the TKey type
				//  entity.HasKey(key => new { key.UserId, key.RoleId });
			});

			builder.Entity<IdentityUserClaim<string>>(entity =>
			{
				entity.ToTable("UserClaims");
			});

			builder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.ToTable("UserLogins");
				//in case you chagned the TKey type
				//  entity.HasKey(key => new { key.ProviderKey, key.LoginProvider });       
			});

			builder.Entity<IdentityRoleClaim<string>>(entity =>
			{
				entity.ToTable("RoleClaims");

			});

			builder.Entity<IdentityUserToken<string>>(entity =>
			{
				entity.ToTable("UserTokens");
				//in case you chagned the TKey type
				// entity.HasKey(key => new { key.UserId, key.LoginProvider, key.Name });

			});
		}
	}
}
