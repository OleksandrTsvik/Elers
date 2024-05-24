namespace Infrastructure.SupabaseSetup;

public class SupabaseSettingsOptions
{
    public required string Url { get; init; }
    public required string Key { get; init; }
    public required string BucketName { get; init; }
}
