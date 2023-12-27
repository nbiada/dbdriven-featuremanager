using System.Reflection;
using DbDrivenFM.App.Extensions;
using Microsoft.FeatureManagement;

namespace DbDrivenFM.App.Services;

public class FeatureGateService
{
    private readonly IFeatureManager _featureManager;

    public FeatureGateService(IFeatureManager featureManager)
    {
        _featureManager = featureManager;
    }

    public async Task<bool> AuthorizeAsync(Type componentType)
    {
        // Retrieve the attribute from the component
        var attribute = componentType.GetCustomAttribute<FeatureGateAttribute>();

        if (attribute == null)
        {
            // No attribute found, default to allowed
            return true;
        }

        // Check authorization
        var authorized = await _featureManager.IsEnabledAsync(attribute.Name);

        return authorized;
    }
}