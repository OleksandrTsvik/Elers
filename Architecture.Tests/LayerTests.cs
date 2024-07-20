namespace Architecture.Tests;

public class LayerTests : BaseTest
{
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        string[] otherProjects =
        [
            ApplicationNamespace,
            PersistenceNamespace,
            InfrastructureNamespace,
            ApiNamespace
        ];

        // Act
        TestResult result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        string[] otherProjects =
        [
            PersistenceNamespace,
            InfrastructureNamespace,
            ApiNamespace
        ];

        // Act
        TestResult result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Persistence_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        string[] otherProjects =
        [
            InfrastructureNamespace,
            ApiNamespace
        ];

        // Act
        TestResult result = Types.InAssembly(PersistenceAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
    {
        // Arrange
        string[] otherProjects =
        [
            PersistenceNamespace,
            ApiNamespace
        ];

        // Act
        TestResult result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(otherProjects)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
