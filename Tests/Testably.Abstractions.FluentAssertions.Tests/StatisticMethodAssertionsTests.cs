using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.IO.Abstractions;
using Testably.Abstractions.Testing;
using Testably.Abstractions.Testing.Statistics;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class StatisticMethodAssertionsTests
{
	[Theory]
	[AutoData]
	public void HaveCalled_Never_WhenCalled_ShouldThrow(
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it did once.");
	}

	[Fact]
	public void HaveCalled_Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Never();
	}

	[Theory]
	[AutoData]
	public void WithParameterAt_WithPredicate_Never_WhenCalled_ShouldThrow(
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText))
				.WithParameterAt<string>(0, p => p == "foo")
				.Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it did once.");
	}

	[Fact]
	public void WithParameterAt_WithPredicate_Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText))
			.WithParameterAt<string>(0, p => p == "bar")
			.Never();
	}

	[Theory]
	[AutoData]
	public void WithParameterAt_WithValue_Never_WhenCalled_ShouldThrow(
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText))
				.WithParameterAt(0, "foo")
				.Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it did once.");
	}

	[Fact]
	public void WithParameterAt_WithValue_Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText))
			.WithParameterAt(0, "bar")
			.Never();
	}

	[Theory]
	[AutoData]
	public void WithFirstParameter_WithPredicate_Never_WhenCalled_ShouldThrow(
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText))
				.WithFirstParameter<string>(p => p == "foo")
				.Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it did once.");
	}

	[Fact]
	public void WithFirstParameter_WithPredicate_Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText))
			.WithFirstParameter<string>(p => p == "bar")
			.Never();
	}

	[Theory]
	[AutoData]
	public void WithFirstParameter_WithValue_Never_WhenCalled_ShouldThrow(
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText))
				.WithFirstParameter("foo")
				.Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it did once.");
	}

	[Fact]
	public void WithFirstParameter_WithValue_Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText))
			.WithFirstParameter("bar")
			.Never();
	}

	[Theory]
	[AutoData]
	public void WithSecondParameter_WithPredicate_Never_WhenCalled_ShouldThrow(
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText))
				.WithSecondParameter<string>(p => p == "bar")
				.Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it did once.");
	}

	[Fact]
	public void WithSecondParameter_WithPredicate_Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText))
			.WithSecondParameter<string>(p => p == "foo")
			.Never();
	}

	[Theory]
	[AutoData]
	public void WithSecondParameter_WithValue_Never_WhenCalled_ShouldThrow(
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText))
				.WithSecondParameter("bar")
				.Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it did once.");
	}

	[Fact]
	public void WithSecondParameter_WithValue_Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText))
			.WithSecondParameter("foo")
			.Never();
	}
}
