using FluentValidation;
using Persistence.Options;

namespace Api.Options.Database;

public class DatabaseSettingsValidator : AbstractValidator<DatabaseSettings>
{
    public DatabaseSettingsValidator()
    {
        RuleFor(x => x.ApplicationDb).NotEmpty();

        RuleFor(x => x.ApplicationDb.ConnectionString).NotEmpty();

        RuleFor(x => x.MongoDb).NotEmpty();

        RuleFor(x => x.MongoDb.ConnectionString).NotEmpty();

        RuleFor(x => x.MongoDb.DatabaseName).NotEmpty();
    }
}
