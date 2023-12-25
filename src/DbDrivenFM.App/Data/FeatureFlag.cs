namespace DbDrivenFM.App.Data
{
    public class FeatureFlag
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool Enabled { get; set; } = false;
    }
}
