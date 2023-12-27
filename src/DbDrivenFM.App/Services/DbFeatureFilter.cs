using DbDrivenFM.App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement;

namespace DbDrivenFM.App.Services
{
    public class DbFeatureFilter : IFeatureFilter
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbFeatureFilter(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<bool> EvaluateAsync(FeatureFilterEvaluationContext context)
        {
            using var scope = _scopeFactory.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var featureName = context.FeatureName;
            var featureFlag = await _context.FeatureFlags
                .FirstOrDefaultAsync(f => 
                f.Name.ToUpper() == featureName.ToUpper());

            return featureFlag?.Enabled ?? false;
        }
    }
}

