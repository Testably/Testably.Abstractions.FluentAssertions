using AutoFixture.Xunit2;
using AwesomeAssertions;
using System;
using System.IO.Abstractions;
using Testably.Abstractions.Testing;
using Testably.Abstractions.Testing.Statistics;
using Xunit;

namespace Testably.Abstractions.AwesomeAssertions.Tests;

public class StatisticPropertyAssertionsTests
{
	[Theory]
	[AutoData]
	public void HaveAccessed_Never_WhenAccessed_ShouldThrow(
		string because)
	{
		MockFileSystem fileSystem = new();
		_ = fileSystem.Path.DirectorySeparatorChar;
		IStatistics<IPath> sut = fileSystem.Statistics.Path;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveAccessed(nameof(IPath.DirectorySeparatorChar)).Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected property `DirectorySeparatorChar` to never be accessed {because}, but it was once.");
	}

	[Fact]
	public void HaveAccessed_Never_WhenNotAccessed_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		IStatistics<IPath> sut = fileSystem.Statistics.Path;

		sut.Should().HaveAccessed(nameof(IPath.DirectorySeparatorChar)).Never();
	}
}
