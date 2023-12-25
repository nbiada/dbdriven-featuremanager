using DbDrivenFM.App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;

namespace DbDrivenFM.App.Services
{
    public class DbFeatureManager : IFeatureManager
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public const string Name = "DbFeatureManager";

        public DbFeatureManager(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public IAsyncEnumerable<string> GetFeatureNamesAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
            //var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var _context = factory.CreateDbContext();

            return _context.FeatureFlags
                .AsNoTracking()
                .Select(f => f.Name)
                .AsAsyncEnumerable();
        }

        public async Task<bool> IsEnabledAsync(string featureName)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var featureFlag = await _context.FeatureFlags
                .AsNoTracking()
                .FirstOrDefaultAsync(f => 
                               f.Name.ToUpper() == featureName.ToUpper());

            return featureFlag?.Enabled ?? false;
        }

        public async Task<bool> IsEnabledAsync<TContext>(string featureName, TContext context)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var featureFlag = await _context.FeatureFlags
                .AsNoTracking()
                .FirstOrDefaultAsync(f => 
                               f.Name.ToUpper() == featureName.ToUpper());

            return featureFlag?.Enabled ?? false;
        }

        public async Task<bool> IsEnabledAsync<TContext, TArgs>(string featureName, TContext context, TArgs args)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var featureFlag = await _context.FeatureFlags
                .FirstOrDefaultAsync(f => 
                               f.Name.ToUpper() == featureName.ToUpper());

            return featureFlag?.Enabled ?? false;
        }

        public async Task<bool> IsEnabledAsync<TContext>(string featureName, TContext context, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var featureFlag = await _context.FeatureFlags
                .AsNoTracking()
                .FirstOrDefaultAsync(f => 
                               f.Name.ToUpper() == featureName.ToUpper());

            return featureFlag?.Enabled ?? false;
        }

        public async Task<bool> IsEnabledAsync<TContext, TArgs>(string featureName, TContext context, TArgs args, CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var featureFlag = await _context.FeatureFlags
                .AsNoTracking()
                .FirstOrDefaultAsync(f => 
                               f.Name.ToUpper() == featureName.ToUpper());

            return featureFlag?.Enabled ?? false;
        }
    }
}
