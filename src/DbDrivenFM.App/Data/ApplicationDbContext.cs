using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DbDrivenFM.App.Data
{
    public class ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) :
        IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<FeatureFlag> FeatureFlags => Set<FeatureFlag>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<FeatureFlag>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.Property(e => e.Enabled).HasDefaultValue(false);

                entity.HasData(
                    new FeatureFlag
                    {
                        Id = 1,
                        Name = FeatureConstants.Weather,
                        Enabled = true
                    },
                    new FeatureFlag
                    {
                        Id = 2,
                        Name = FeatureConstants.Counter,
                        Enabled = true
                    },
                    new FeatureFlag
                    {
                        Id = 3,
                        Name = FeatureConstants.FetchData,
                        Enabled = true
                    });
            });
        }
    }
}
