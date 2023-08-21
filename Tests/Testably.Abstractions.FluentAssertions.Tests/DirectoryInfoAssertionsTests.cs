using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.IO.Abstractions;
using Testably.Abstractions.Testing;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class DirectoryInfoAssertionsTests
{
	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HaveFileMatching_InvalidFileName_ShouldThrow(string? invalidFileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New("foo");

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFileMatching(invalidFileName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveFileMatching_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFileMatching(because: because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveFileMatching_WithMatchingFile_ShouldNotThrow(
		string directoryName,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile(fileName));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().HaveFileMatching(fileName);
	}

	[Theory]
	[AutoData]
	public void HaveFileMatching_WithoutMatchingFile_ShouldThrow(
		string directoryName,
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile("not-matching-file"));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFileMatching(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least one file matching \"{fileName}\" {because}, but none was found.");
	}
}
