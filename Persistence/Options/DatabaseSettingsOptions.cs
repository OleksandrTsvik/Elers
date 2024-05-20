namespace Persistence.Options;

public class DatabaseSettingsOptions
{
    public required ApplicationDbSettings ApplicationDb { get; init; }
    public required MongoDbSettings MongoDb { get; init; }
}
