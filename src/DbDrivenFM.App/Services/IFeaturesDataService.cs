using DbDrivenFM.App.Data;

namespace DbDrivenFM.App.Services
{
    public interface IFeaturesDataService
    {
        Task<IReadOnlyCollection<FeatureFlag>> GetFeatureFlagAsync();
        Task Toggle(int featureId);
    }
}