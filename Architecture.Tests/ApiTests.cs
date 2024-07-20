using Api.Controllers;

namespace Architecture.Tests;

public class ApiTests : BaseTest
{
    [Fact]
    public void Controllers_Should_InheritFromApiControllerBase()
    {
        // Arrange

        // Act
        TestResult result = Types.InAssembly(ApiAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .And()
            .DoNotHaveName("FallbackController")
            .Should()
            .Inherit(typeof(ApiControllerBase))
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }

    [Fact]
    public void Controllers_Should_Not_HaveDependencyOnRepositories()
    {
        // Arrange

        // Act
        TestResult result = Types.InAssembly(ApiAssembly)
            .That()
            .HaveNameEndingWith("Controller")
            .ShouldNot()
            .HaveDependencyOn($"{PersistenceNamespace}.Repositories")
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
