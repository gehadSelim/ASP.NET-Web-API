using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Day3.Data
{
    public class ComContext : IdentityDbContext<ApplicationUser>
    {
        public ComContext(DbContextOptions<ComContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("AppUsers");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AppUsersClaims");
        }
    }
}
