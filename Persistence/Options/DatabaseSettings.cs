namespace Persistence.Options;

public class DatabaseSettings
{
    public required ApplicationDbSettings ApplicationDb { get; init; }
    public required MongoDbSettings MongoDb { get; init; }
}
