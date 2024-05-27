namespace Infrastructure.SupabaseSetup;

public class SupabaseSettings
{
    public required string Url { get; init; }
    public required string Key { get; init; }
    public required string BucketName { get; init; }
}
