namespace DbDrivenFM.App.Extensions;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class FeatureGateAttribute : Attribute
{
    public string Name { get; private set; }

    public FeatureGateAttribute(string name)
    {
        Name = name;
    }
}