using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.IO.Abstractions;
using Testably.Abstractions.Testing;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class FileSystemInfoAssertionsTests
{
	[Theory]
	[AutoData]
	public void Exist_ForDirectoryInfo_WithCorrectDirectory_ShouldNotThrow(string directoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectories(directoryName);

		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().Exist();
	}

	[Theory]
	[AutoData]
	public void Exist_ForDirectoryInfo_WithoutCorrectDirectory_ShouldThrow(
		string directoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().Exist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be($"Expected directory \"{directoryName}\" to exist {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void Exist_ForDirectoryInfo_WithSameFile_ShouldThrow(
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, "some content");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(fileName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().Exist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be($"Expected directory \"{fileName}\" to exist {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void Exist_ForFileInfo_WithCorrectFile_ShouldNotThrow(string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);

		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		sut.Should().Exist();
	}

	[Theory]
	[AutoData]
	public void Exist_ForFileInfo_WithoutCorrectFile_ShouldThrow(
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().Exist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be($"Expected file \"{fileName}\" to exist {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void Exist_ForFileInfo_WithSameDirectory_ShouldThrow(
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.Directory.CreateDirectory(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().Exist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be($"Expected file \"{fileName}\" to exist {because}, but it did not.");
	}
}
