﻿using AutoFixture.Xunit2;
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
	public void Exist_ForDirectoryInfo_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().Exist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void Exist_ForDirectoryInfo_WithExistingDirectory_ShouldNotThrow(string directoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectories(directoryName);

		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().Exist();
	}

	[Theory]
	[AutoData]
	public void Exist_ForDirectoryInfo_WithoutExistingDirectory_ShouldThrow(
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
	public void Exist_ForFileInfo_Null_ShouldThrow(string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().Exist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void Exist_ForFileInfo_WithExistingFile_ShouldNotThrow(string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);

		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		sut.Should().Exist();
	}

	[Theory]
	[AutoData]
	public void Exist_ForFileInfo_WithoutExistingFile_ShouldThrow(
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

#if NET
	[SkippableTheory]
	[AutoData]
	public void Exist_ForFileSystemInfo_WithExistingFile_ShouldNotThrow(
		string path, string pathToTarget, string because)
	{
		MockFileSystem fileSystem = new();
		string targetFullPath = fileSystem.Path.GetFullPath(pathToTarget);
		fileSystem.Directory.CreateDirectory(pathToTarget);
		fileSystem.Directory.CreateSymbolicLink(path, targetFullPath);
		IFileSystemInfo? sut =
			fileSystem.Directory.ResolveLinkTarget(path, false);

		sut.Should().Exist(because);
	}

	[SkippableTheory]
	[AutoData]
	public void Exist_ForFileSystemInfo_WithoutExistingFile_ShouldThrow(
		string path, string pathToTarget, string because)
	{
		MockFileSystem fileSystem = new();
		string targetFullPath = fileSystem.Path.GetFullPath(pathToTarget);
		fileSystem.Directory.CreateSymbolicLink(path, targetFullPath);
		IFileSystemInfo? sut =
			fileSystem.Directory.ResolveLinkTarget(path, false);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().Exist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file system info \"{pathToTarget}\" to exist {because}, but it did not.");
	}
#endif

	[Theory]
	[AutoData]
	public void NotExist_ForDirectoryInfo_Null_ShouldThrow(string because)
	{
		IDirectoryInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotExist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void NotExist_ForDirectoryInfo_WithExistingDirectory_ShouldThrow(
		string directoryName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectories(directoryName);

		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotExist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be($"Expected directory \"{directoryName}\" not to exist {because}, but it did.");
	}

	[Theory]
	[AutoData]
	public void NotExist_ForDirectoryInfo_WithoutExistingDirectory_ShouldNotThrow(
		string directoryName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(directoryName);

		sut.Should().NotExist();
	}

	[Theory]
	[AutoData]
	public void NotExist_ForDirectoryInfo_WithSameFile_ShouldNotThrow(
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, "some content");
		IDirectoryInfo sut = fileSystem.DirectoryInfo.New(fileName);

		sut.Should().NotExist(because);
	}

	[Theory]
	[AutoData]
	public void NotExist_ForFileInfo_Null_ShouldThrow(string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotExist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void NotExist_ForFileInfo_WithExistingFile_ShouldThrow(
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotExist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be($"Expected file \"{fileName}\" not to exist {because}, but it did.");
	}

	[Theory]
	[AutoData]
	public void NotExist_ForFileInfo_WithoutExistingFile_ShouldNotThrow(string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();

		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		sut.Should().NotExist();
	}

	[Theory]
	[AutoData]
	public void NotExist_ForFileInfo_WithSameDirectory_ShouldNotThrow(
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.Directory.CreateDirectory(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		sut.Should().NotExist(because);
	}

#if NET
	[SkippableTheory]
	[AutoData]
	public void NotExist_ForFileSystemInfo_WithExistingFile_ShouldThrow(
		string path, string pathToTarget, string because)
	{
		MockFileSystem fileSystem = new();
		string targetFullPath = fileSystem.Path.GetFullPath(pathToTarget);
		fileSystem.Directory.CreateDirectory(pathToTarget);
		fileSystem.Directory.CreateSymbolicLink(path, targetFullPath);
		IFileSystemInfo? sut =
			fileSystem.Directory.ResolveLinkTarget(path, false);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotExist(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file system info \"{pathToTarget}\" not to exist {because}, but it did.");
	}

	[SkippableTheory]
	[AutoData]
	public void NotExist_ForFileSystemInfo_WithoutExistingFile_ShouldNotThrow(
		string path, string pathToTarget, string because)
	{
		MockFileSystem fileSystem = new();
		string targetFullPath = fileSystem.Path.GetFullPath(pathToTarget);
		fileSystem.Directory.CreateSymbolicLink(path, targetFullPath);
		IFileSystemInfo? sut =
			fileSystem.Directory.ResolveLinkTarget(path, false);

		sut.Should().NotExist(because);
	}
#endif
}
