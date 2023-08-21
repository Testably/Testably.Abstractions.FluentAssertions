using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using Testably.Abstractions.Testing;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class DirectoryAssertionsTests
{
	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HasFileMatching_InvalidFileName_ShouldThrow(string? invalidFileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

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
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

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
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasFileMatching(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least one file matching \"{fileName}\" {because}, but none was found.");
	}

	[Theory]
	[InlineAutoData(3, 5)]
	[InlineAutoData(1, 2)]
	public void HasFilesMatching_WithoutTooLittleFiles_ShouldThrow(
		int matchingCount,
		int expectedCount,
		string directoryName,
		string fileNamePrefix,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName);
		for (int i = 0; i < matchingCount; i++)
		{
			fileSystem.File.WriteAllText(
				fileSystem.Path.Combine(directoryName, $"{fileNamePrefix}-{i}.txt"),
				"some content");
		}

		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasFilesMatching($"{fileNamePrefix}*", expectedCount, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least {expectedCount} files matching \"{fileNamePrefix}*\" {because}, but only {matchingCount} were found.");
	}
}
