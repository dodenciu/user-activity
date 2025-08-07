using NetArchTest.Rules;

namespace UserActivity.ArchitectureTests;

public class LayerTests
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApplicationLayer()
    {
        TestResult? result = Types.InAssembly(typeof(Domain.IAssemblyMarker).Assembly)
            .Should()
            .NotHaveDependencyOn(typeof(Application.IAssemblyMarker).Assembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult? result = Types.InAssembly(typeof(Domain.IAssemblyMarker).Assembly)
            .Should()
            .NotHaveDependencyOn(typeof(Infrastructure.IAssemblyMarker).Assembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult? result = Types.InAssembly(typeof(Application.IAssemblyMarker).Assembly)
            .Should()
            .NotHaveDependencyOn(typeof(Infrastructure.IAssemblyMarker).Assembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult? result = Types.InAssembly(typeof(Application.IAssemblyMarker).Assembly)
            .Should()
            .NotHaveDependencyOn(typeof(Api.IAssemblyMarker).Assembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_PresentationLayer()
    {
        TestResult? result = Types.InAssembly(typeof(Infrastructure.IAssemblyMarker).Assembly)
            .Should()
            .NotHaveDependencyOn(typeof(Api.IAssemblyMarker).Assembly.GetName().Name)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
