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
	public void HasDirectories_InvalidDirectoryName_ShouldThrow(string? invalidDirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasDirectories(invalidDirectoryName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HasDirectories_WithMatchingDirectory_ShouldNotThrow(
		string directoryName,
		string subdirectoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory(subdirectoryName));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		sut.HasDirectories(subdirectoryName);
	}

	[Theory]
	[AutoData]
	public void HasDirectories_WithoutMatchingDirectory_ShouldThrow(
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
			sut.HasDirectories(subdirectoryName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least one directory matching \"{subdirectoryName}\" {because}, but none was found.");
	}

	[Theory]
	[InlineAutoData(3, 5)]
	[InlineAutoData(1, 2)]
	public void HasDirectories_WithoutTooLittleDirectories_ShouldThrow(
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
			sut.HasDirectories($"{directoryNamePrefix}*", expectedCount, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least {expectedCount} directories matching \"{directoryNamePrefix}*\" {because}, but only {matchingCount} were found.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HasDirectory_InvalidDirectoryName_ShouldThrow(
		string? invalidDirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasDirectory(invalidDirectoryName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HasDirectory_WithMatchingDirectory_ShouldNotThrow(
		string directoryName,
		string subdirectoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory(subdirectoryName));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		sut.HasDirectory(subdirectoryName);
	}

	[Theory]
	[AutoData]
	public void HasDirectory_WithMultipleMatchingDirectory_ShouldThrow(
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
			sut.HasDirectory($"{subdirectoryName}*", because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one directory matching \"{subdirectoryName}*\" {because}, but found 2.");
	}

	[Theory]
	[AutoData]
	public void HasDirectory_WithoutMatchingDirectory_ShouldThrow(
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
			sut.HasDirectory(subdirectoryName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one directory matching \"{subdirectoryName}\" {because}, but found 0.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HasFile_InvalidFileName_ShouldThrow(string? invalidFileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasFile(invalidFileName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HasFile_WithMatchingFile_ShouldNotThrow(
		string directoryName,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile(fileName));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		sut.HasFile(fileName);
	}

	[Theory]
	[AutoData]
	public void HasFile_WithMultipleMatchingFile_ShouldThrow(
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
			sut.HasFile($"{fileName}*", because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one file matching \"{fileName}*\" {because}, but found 2.");
	}

	[Theory]
	[AutoData]
	public void HasFile_WithoutMatchingFile_ShouldThrow(
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
			sut.HasFile(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one file matching \"{fileName}\" {because}, but found 0.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HasFiles_InvalidFileName_ShouldThrow(string? invalidFileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory("foo").Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasFiles(invalidFileName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HasFiles_WithMatchingFile_ShouldNotThrow(
		string directoryName,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile(fileName));
		DirectoryAssertions? sut = fileSystem.Should().HaveDirectory(directoryName).Which;

		sut.HasFiles(fileName);
	}

	[Theory]
	[AutoData]
	public void HasFiles_WithoutMatchingFile_ShouldThrow(
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
			sut.HasFiles(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least one file matching \"{fileName}\" {because}, but none was found.");
	}

	[Theory]
	[InlineAutoData(3, 5)]
	[InlineAutoData(1, 2)]
	public void HasFiles_WithoutTooLittleFiles_ShouldThrow(
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
			sut.HasFiles($"{fileNamePrefix}*", expectedCount, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least {expectedCount} files matching \"{fileNamePrefix}*\" {because}, but only {matchingCount} were found.");
	}
}
