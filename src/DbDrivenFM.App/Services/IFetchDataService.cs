using DbDrivenFM.App.Data;

namespace DbDrivenFM.App.Services
{
    public interface IFetchDataService
    {
        Task<IReadOnlyCollection<FeatureFlag>> GetFeatureFlagAsync();
    }
}