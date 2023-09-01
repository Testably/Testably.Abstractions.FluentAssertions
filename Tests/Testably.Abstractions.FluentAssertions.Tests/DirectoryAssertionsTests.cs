﻿using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using Testably.Abstractions.Testing;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class DirectoryAssertionsTests
{
	[Theory]
	[InlineAutoData(3, 5)]
	[InlineAutoData(1, 2)]
	public void HasDirectoriesMatching_WithoutTooLittleDirectories_ShouldThrow(
		int matchingCount,
		int expectedCount,
		string directoryName,
		string directoryNamePrefix,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName);
		for (int i = 0; i < matchingCount; i++)
		{
			fileSystem.Directory.CreateDirectory(
				fileSystem.Path.Combine(directoryName, $"{directoryNamePrefix}-{i}"));
		}

		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasDirectoriesMatching($"{directoryNamePrefix}*", expectedCount, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least {expectedCount} directories matching \"{directoryNamePrefix}*\" {because}, but only {matchingCount} were found.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HasDirectoryMatching_InvalidDirectoryName_ShouldThrow(string? invalidDirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasDirectoryMatching(invalidDirectoryName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HasDirectoryMatching_WithMatchingDirectory_ShouldNotThrow(
		string directoryName,
		string subdirectoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory(subdirectoryName));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		sut.HasDirectoryMatching(subdirectoryName);
	}

	[Theory]
	[AutoData]
	public void HasDirectoryMatching_WithoutMatchingDirectory_ShouldThrow(
		string directoryName,
		string subdirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory("not-matching-directory"));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasDirectoryMatching(subdirectoryName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least one directory matching \"{subdirectoryName}\" {because}, but none was found.");
	}

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

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HasSingleDirectoryMatching_InvalidDirectoryName_ShouldThrow(
		string? invalidDirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasSingleDirectoryMatching(invalidDirectoryName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HasSingleDirectoryMatching_WithMatchingDirectory_ShouldNotThrow(
		string directoryName,
		string subdirectoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory(subdirectoryName));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		sut.HasSingleDirectoryMatching(subdirectoryName);
	}

	[Theory]
	[AutoData]
	public void HasSingleDirectoryMatching_WithMultipleMatchingDirectory_ShouldThrow(
		string directoryName,
		string subdirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory($"{subdirectoryName}-1.txt")
				.WithSubdirectory($"{subdirectoryName}-2.txt"));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasSingleDirectoryMatching($"{subdirectoryName}*", because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one directory matching \"{subdirectoryName}*\" {because}, but found 2.");
	}

	[Theory]
	[AutoData]
	public void HasSingleDirectoryMatching_WithoutMatchingDirectory_ShouldThrow(
		string directoryName,
		string subdirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory("not-matching-directory"));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasSingleDirectoryMatching(subdirectoryName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one directory matching \"{subdirectoryName}\" {because}, but found 0.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HasSingleFileMatching_InvalidFileName_ShouldThrow(string? invalidFileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasSingleFileMatching(invalidFileName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HasSingleFileMatching_WithMatchingFile_ShouldNotThrow(
		string directoryName,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile(fileName));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		sut.HasSingleFileMatching(fileName);
	}

	[Theory]
	[AutoData]
	public void HasSingleFileMatching_WithMultipleMatchingFile_ShouldThrow(
		string directoryName,
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile($"{fileName}-1.txt")
				.WithFile($"{fileName}-2.txt"));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasSingleFileMatching($"{fileName}*", because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one file matching \"{fileName}*\" {because}, but found 2.");
	}

	[Theory]
	[AutoData]
	public void HasSingleFileMatching_WithoutMatchingFile_ShouldThrow(
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
			sut.HasSingleFileMatching(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one file matching \"{fileName}\" {because}, but found 0.");
	}
}
