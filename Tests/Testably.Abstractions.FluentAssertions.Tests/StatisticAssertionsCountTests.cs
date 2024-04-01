using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.IO.Abstractions;
using Testably.Abstractions.Testing;
using Testably.Abstractions.Testing.Statistics;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class StatisticAssertionsCountTests
{
	[Theory]
	[InlineAutoData(1, "once")]
	[InlineAutoData(2, "twice")]
	[InlineAutoData(3, "3 times")]
	public void Never_WhenCalled_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it was {text}.");
	}

	[Fact]
	public void Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Never();
	}

	[Theory]
	[InlineAutoData(0, "never")]
	[InlineAutoData(2, "twice")]
	[InlineAutoData(3, "3 times")]
	public void Once_WhenCalled_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Once(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called once {because}, but it was {text}.");
	}

	[Fact]
	public void Once_WhenCalledOnce_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Once();
	}
}
