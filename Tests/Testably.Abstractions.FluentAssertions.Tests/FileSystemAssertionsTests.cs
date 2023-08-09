﻿using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using Testably.Abstractions.Testing;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class FileSystemAssertionsTests
{
	[Theory]
	[AutoData]
	public void HaveDirectory_And_WithMultipleDirectories_ShouldNotThrow(string path1, string path2)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(path1)
			.WithSubdirectory(path2);

		fileSystem.Should().HaveDirectory(path1).And.HaveDirectory(path2);
	}

	[Theory]
	[AutoData]
	public void HaveDirectory_And_WithOnlyOneDirectory_ShouldThrow(string path1, string path2)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(path1);

		Exception? exception = Record.Exception(() =>
		{
			fileSystem.Should().HaveDirectory(path1).And.HaveDirectory(path2);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain(path2);
		exception.Message.Should().NotContain(path1);
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HaveDirectory_InvalidPath_ShouldThrow(string? invalidPath, string because)
	{
		MockFileSystem fileSystem = new();
		FileSystemAssertions sut = fileSystem.Should();

		Exception? exception = Record.Exception(() =>
		{
			sut.HaveDirectory(invalidPath!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveDirectory_WithDirectory_ShouldNotThrow(string path)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(path);

		fileSystem.Should().HaveDirectory(path);
	}

	[Theory]
	[AutoData]
	public void HaveDirectory_WithoutDirectory_ShouldThrow(
		string path,
		string because)
	{
		MockFileSystem fileSystem = new();

		Exception? exception = Record.Exception(() =>
		{
			fileSystem.Should().HaveDirectory(path, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain(path);
		exception.Message.Should().Contain(because);
	}

	[Theory]
	[AutoData]
	public void HaveFile_And_WithMultipleFiles_ShouldNotThrow(string path1, string path2)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(path1)
			.WithFile(path2);

		fileSystem.Should().HaveFile(path1).And.HaveFile(path2);
	}

	[Theory]
	[AutoData]
	public void HaveFile_And_WithOnlyOneFile_ShouldThrow(string path1, string path2)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(path1);

		Exception? exception = Record.Exception(() =>
		{
			fileSystem.Should().HaveFile(path1).And.HaveFile(path2);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain(path2);
		exception.Message.Should().NotContain(path1);
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void HaveFile_InvalidPath_ShouldThrow(string? invalidPath, string because)
	{
		MockFileSystem fileSystem = new();
		FileSystemAssertions sut = fileSystem.Should();

		Exception? exception = Record.Exception(() =>
		{
			sut.HaveFile(invalidPath!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveFile_WithFile_ShouldNotThrow(string path)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(path);

		fileSystem.Should().HaveFile(path);
	}

	[Theory]
	[AutoData]
	public void HaveFile_WithoutFile_ShouldThrow(string path, string because)
	{
		MockFileSystem fileSystem = new();

		Exception? exception = Record.Exception(() =>
		{
			fileSystem.Should().HaveFile(path, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain(path);
		exception.Message.Should().Contain(because);
	}
}
