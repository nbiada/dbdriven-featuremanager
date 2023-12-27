using DbDrivenFM.App.Data;

namespace DbDrivenFM.App.Extensions;

public static class ProjectExtensions
{
    public static string Status(this FeatureFlag feature)
    {
        return feature.Enabled switch
        {
            true => "<span class='badge bg-success text-white fs-6'>ENABLED</span>",
            false => "<span class='badge bg-secondary text-white fs-6'>DISABLED</span>"
        };
    }
}