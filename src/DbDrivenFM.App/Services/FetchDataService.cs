using DbDrivenFM.App.Data;
using Microsoft.EntityFrameworkCore;

namespace DbDrivenFM.App.Services
{
    public class FetchDataService : IFetchDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _factory;

        public FetchDataService(IDbContextFactory<ApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<IReadOnlyCollection<FeatureFlag>> GetFeatureFlagAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.FeatureFlags.ToListAsync();
        }

    }
}
