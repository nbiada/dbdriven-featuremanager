using DbDrivenFM.App.Data;
using Microsoft.EntityFrameworkCore;

namespace DbDrivenFM.App.Services
{
    public class FeaturesDataService : IFeaturesDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public FeaturesDataService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<IReadOnlyCollection<FeatureFlag>> GetFeatureFlagAsync()
        {
            await using var context = await _factory.CreateDbContextAsync();
            return await context.FeatureFlags.AsNoTracking().ToListAsync();
        }

        public async Task Toggle(int featureId)
        {
            await using var context = await _factory.CreateDbContextAsync();
            var item = await context.FeatureFlags.FindAsync(featureId);
            if (item == null) return;

            item.Enabled = !item.Enabled;
            await context.SaveChangesAsync();
        }
    }
}
