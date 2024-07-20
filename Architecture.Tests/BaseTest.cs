using System.Reflection;

namespace Architecture.Tests;

public class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(Domain.AssemblyReference).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(Application.AssemblyReference).Assembly;
    protected static readonly Assembly PersistenceAssembly = typeof(Persistence.AssemblyReference).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.AssemblyReference).Assembly;
    protected static readonly Assembly ApiAssembly = typeof(Api.AssemblyReference).Assembly;

    protected static readonly string DomainNamespace = "Domain";
    protected static readonly string ApplicationNamespace = "Application";
    protected static readonly string PersistenceNamespace = "Persistence";
    protected static readonly string InfrastructureNamespace = "Infrastructure";
    protected static readonly string ApiNamespace = "Api";
}
