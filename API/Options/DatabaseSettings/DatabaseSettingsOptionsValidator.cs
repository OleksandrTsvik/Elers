using FluentValidation;
using Persistence.Options;

namespace API.Options.DatabaseSettings;

public class DatabaseSettingsOptionsValidator : AbstractValidator<DatabaseSettingsOptions>
{
    public DatabaseSettingsOptionsValidator()
    {
        RuleFor(x => x.ApplicationDb).NotEmpty();

        RuleFor(x => x.ApplicationDb.ConnectionString).NotEmpty();

        RuleFor(x => x.MongoDb).NotEmpty();

        RuleFor(x => x.MongoDb.ConnectionString).NotEmpty();

        RuleFor(x => x.MongoDb.DatabaseName).NotEmpty();
    }
}
