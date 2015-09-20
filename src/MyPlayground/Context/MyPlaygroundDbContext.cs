using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using MyPlayground.Models;

namespace MyPlayground.Context
{
  public class MyPlaygroundDbContext : IdentityDbContext<User>
  {
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);

      builder.Entity<User>()
        .ToTable("User");

      builder.Entity<Role>()
        .ToTable("Role");

      builder.Entity<IdentityUserRole<string>>()
        .ToTable("UserRole");

      builder.Entity<IdentityUserClaim<string>>()
        .ToTable("UserClaim");

      builder.Entity<IdentityRoleClaim<string>>()
        .ToTable("RoleClaim");

      builder.Entity<IdentityUserLogin<string>>()
        .ToTable("UserLogin");
    }
  }
}
