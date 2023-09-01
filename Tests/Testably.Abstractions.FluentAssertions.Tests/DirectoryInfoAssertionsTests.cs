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
	public void HaveDirectoryMatching_InvalidDirectoryName_ShouldThrow(string? invalidDirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New("foo");

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectoryMatching(invalidDirectoryName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveDirectoryMatching_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveDirectoryMatching(because: because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveDirectoryMatching_WithMatchingDirectory_ShouldNotThrow(
		string directoryName,
		string subdirectoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory(subdirectoryName));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().HaveDirectoryMatching(subdirectoryName);
	}

	[Theory]
	[AutoData]
	public void HaveDirectoryMatching_WithoutMatchingDirectory_ShouldThrow(
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
			sut.Should().HaveDirectoryMatching(subdirectoryName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain at least one directory matching \"{subdirectoryName}\" {because}, but none was found.");
	}

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

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HaveSingleDirectory_InvalidDirectoryName_ShouldThrow(string? invalidDirectoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New("foo");

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveSingleDirectory(invalidDirectoryName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveSingleDirectory_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveSingleDirectory(because: because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveSingleDirectory_WithMatchingDirectory_ShouldNotThrow(
		string directoryName,
		string subdirectoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithSubdirectory(subdirectoryName));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().HaveSingleDirectory(subdirectoryName);
	}

	[Theory]
	[AutoData]
	public void HaveSingleDirectory_WithMultipleMatchingDirectory_ShouldThrow(
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
			sut.Should().HaveSingleDirectory("directory*.txt", because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one directory matching \"directory*.txt\" {because}, but found 2.");
	}

	[Theory]
	[AutoData]
	public void HaveSingleDirectory_WithoutMatchingDirectory_ShouldThrow(
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
			sut.Should().HaveSingleDirectory(subdirectoryName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one directory matching \"{subdirectoryName}\" {because}, but found 0.");
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HaveSingleFile_InvalidFileName_ShouldThrow(string? invalidFileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory("foo");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New("foo");

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveSingleFile(invalidFileName!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveSingleFile_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveSingleFile(because: because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveSingleFile_WithMatchingFile_ShouldNotThrow(
		string directoryName,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(directoryName).Initialized(d => d
				.WithFile(fileName));
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().HaveSingleFile(fileName);
	}

	[Theory]
	[AutoData]
	public void HaveSingleFile_WithMultipleMatchingFile_ShouldThrow(
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
			sut.Should().HaveSingleFile("file*.txt", because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one file matching \"file*.txt\" {because}, but found 2.");
	}

	[Theory]
	[AutoData]
	public void HaveSingleFile_WithoutMatchingFile_ShouldThrow(
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
			sut.Should().HaveSingleFile(fileName, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected directory \"{directoryName}\" to contain exactly one file matching \"{fileName}\" {because}, but found 0.");
	}
}
