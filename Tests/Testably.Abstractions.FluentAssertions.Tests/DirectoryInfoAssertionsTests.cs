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
	public void HaveDirectories_InvalidDirectoryName_ShouldThrow(string? invalidDirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New("foo");

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectories(invalidDirectoryName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveDirectories_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectories(because: because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveDirectories_WithMatchingDirectory_ShouldNotThrow(
		string directoryName,
		string subdirectoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory(subdirectoryName));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().HaveDirectories(subdirectoryName);
	}

	[Theory]
	[AutoData]
	public void HaveDirectories_WithoutMatchingDirectory_ShouldThrow(
		string directoryName,
		string subdirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory("not-matching-directory"));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectories(subdirectoryName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least one directory matching \"{subdirectoryName}\" {because}, but none was found.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HaveDirectory_InvalidDirectoryName_ShouldThrow(string? invalidDirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New("foo");

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectory(invalidDirectoryName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveDirectory_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectory(because: because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveDirectory_WithMatchingDirectory_ShouldNotThrow(
		string directoryName,
		string subdirectoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory(subdirectoryName));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().HaveDirectory(subdirectoryName);
	}

	[Theory]
	[AutoData]
	public void HaveDirectory_WithMultipleMatchingDirectory_ShouldThrow(
		string directoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory("directory1.txt")
				.WithSubdirectory("directory2.txt"));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectory("directory*.txt", because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one directory matching \"directory*.txt\" {because}, but found 2.");
	}

	[Theory]
	[AutoData]
	public void HaveDirectory_WithoutMatchingDirectory_ShouldThrow(
		string directoryName,
		string subdirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory("not-matching-directory"));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectory(subdirectoryName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one directory matching \"{subdirectoryName}\" {because}, but found 0.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HaveFile_InvalidFileName_ShouldThrow(string? invalidFileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New("foo");

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFile(invalidFileName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveFile_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFile(because: because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveFile_WithMatchingFile_ShouldNotThrow(
		string directoryName,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile(fileName));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().HaveFile(fileName);
	}

	[Theory]
	[AutoData]
	public void HaveFile_WithMultipleMatchingFile_ShouldThrow(
		string directoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile("file1.txt")
				.WithFile("file2.txt"));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFile("file*.txt", because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one file matching \"file*.txt\" {because}, but found 2.");
	}

	[Theory]
	[AutoData]
	public void HaveFile_WithoutMatchingFile_ShouldThrow(
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
			sut.Should().HaveFile(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one file matching \"{fileName}\" {because}, but found 0.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HaveFiles_InvalidFileName_ShouldThrow(string? invalidFileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New("foo");

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFiles(invalidFileName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveFiles_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFiles(because: because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveFiles_WithMatchingFile_ShouldNotThrow(
		string directoryName,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile(fileName));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().HaveFiles(fileName);
	}

	[Theory]
	[AutoData]
	public void HaveFiles_WithoutMatchingFile_ShouldThrow(
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
			sut.Should().HaveFiles(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least one file matching \"{fileName}\" {because}, but none was found.");
	}
}
