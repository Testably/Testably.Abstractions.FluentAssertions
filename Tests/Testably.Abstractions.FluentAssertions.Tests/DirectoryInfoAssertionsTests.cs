using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using Testably.Abstractions.Testing;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class DirectoryInfoAssertionsTests
{
	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HasFileMatching_InvalidFileName_ShouldThrow(string? invalidFileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryInfoAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasFileMatching(invalidFileName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HasFileMatching_WithMatchingFile_ShouldNotThrow(
		string directoryName,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile(fileName));
		DirectoryInfoAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		sut.HasFileMatching(fileName);
	}

	[Theory]
	[AutoData]
	public void HasFileMatching_WithoutMatchingFile_ShouldThrow(
		string directoryName,
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile("not-matching-file"));
		DirectoryInfoAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasFileMatching(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain(fileName);
		exception.Message.Should().Contain(because);
	}
}
