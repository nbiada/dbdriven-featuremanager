using System.Reflection;
using DbDrivenFM.App.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using RouteData = Microsoft.AspNetCore.Routing.RouteData;

namespace DbDrivenFM.App.Extensions;

public class FeatureAuthorizeRouteView //: AuthorizeRouteView
{
    [Inject] private FeatureGateService FeatureGateService { get; set; }

    // protected override async Task OnParametersSetAsync()
    // {
    //     await base.OnParametersSetAsync();
    //
    //     var isAuthorized = await FeatureAuthorization(RouteData.);
    // }

    private async Task<bool> FeatureAuthorization(Type componentType)
    {
        var featureAttribute = componentType.GetCustomAttribute<FeatureGateAttribute>();
        if (featureAttribute != null)
        {
            return await FeatureGateService.AuthorizeAsync(componentType);
        }

        // If no FeatureGate attribute, return true
        return true;
    }
}