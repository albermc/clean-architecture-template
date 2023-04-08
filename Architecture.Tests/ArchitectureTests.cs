using FluentAssertions;
using NetArchTest.Rules;

namespace Architecture.Tests;

public class ArchitectureTests
{
	private const string DomainNamespace = "Domain";
	private const string ApplicationNamespace = "Application";
	private const string InfrastructureNamespace = "Infrastructure";
	private const string PresentationNamespace = "Presentation";
	private const string WebApiNamespace = "WebApi";

	[Fact]
	public void Domain_Should_Not_Have_DependencyOnOtherProjects()
	{
		// Arrange
		var assembly = Domain.AssemblyReference.Assembly;

		var otherProjects = new[]
		{
			ApplicationNamespace,
			InfrastructureNamespace,
			PresentationNamespace,
			WebApiNamespace
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects)
			.GetResult();

		// Assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Application_Should_Not_Have_DependencyOnOtherProjects()
	{
		// Arrange
		var assembly = Application.AssemblyReference.Assembly;

		var otherProjects = new[]
		{
			InfrastructureNamespace,
			PresentationNamespace,
			WebApiNamespace
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects)
			.GetResult();

		// Assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Handlers_Should_Have_DependencyOnDomain()
	{
		// Arrange
		var assembly = Application.AssemblyReference.Assembly;

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.That()
			.HaveNameEndingWith("Handler")
			.Should()
			.HaveDependencyOn(DomainNamespace)
			.GetResult();

		// Assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Infrastructure_Should_Not_Have_DependencyOnOtherProjects()
	{
		// Arrange
		var assembly = Infrastructure.AssemblyReference.Assembly;

		var otherProjects = new[]
		{
			PresentationNamespace,
			WebApiNamespace
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects)
			.GetResult();

		// Assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Presentation_Should_Not_Have_DependencyOnOtherProjects()
	{
		// Arrange
		var assembly = Presentation.AssemblyReference.Assembly;

		var otherProjects = new[]
		{
			InfrastructureNamespace,
			WebApiNamespace
		};

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.ShouldNot()
			.HaveDependencyOnAll(otherProjects)
			.GetResult();

		// Assert
		testResult.IsSuccessful.Should().BeTrue();
	}

	[Fact]
	public void Controllers_Should_Have_DependencyOnMediatR()
	{
		// Arrange
		var assembly = Presentation.AssemblyReference.Assembly;

		// Act
		var testResult = Types
			.InAssembly(assembly)
			.That()
			.HaveNameEndingWith("Controller")
			.Should()
			.HaveDependencyOn("MediatR")
			.GetResult();

		// Assert
		testResult.IsSuccessful.Should().BeTrue();
	}
}
