namespace Architecture.Tests;

public class PersistenceTests : BaseTest
{
    [Fact]
    public void Repositories_Should_HaveDependencyOnDomain()
    {
        // Arrange

        // Act
        TestResult result = Types.InAssembly(PersistenceAssembly)
            .That()
            .HaveNameEndingWith("Repository")
            .Should()
            .HaveDependencyOn(DomainNamespace)
            .GetResult();

        // Assert
        result.IsSuccessful.Should().BeTrue();
    }
}
