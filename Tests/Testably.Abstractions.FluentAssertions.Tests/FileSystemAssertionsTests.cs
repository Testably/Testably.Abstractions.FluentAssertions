using AutoFixture.Xunit2;
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
		exception!.Message.Should()
			.Be(
				$"Expected filesystem to contain directory \"{path}\" {because}, but it did not exist.");
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
		exception!.Message.Should()
			.Be($"Expected filesystem to contain file \"{path}\" {because}, but it did not exist.");
	}

	[Theory]
	[AutoData]
	public void NotHaveDirectory_And_WithOnlyOneDirectory_ShouldThrow(string path1, string path2)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(path2);

		Exception? exception = Record.Exception(() =>
		{
			fileSystem.Should().NotHaveDirectory(path1).And.NotHaveDirectory(path2);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain(path2);
		exception.Message.Should().NotContain(path1);
	}

	[Theory]
	[AutoData]
	public void NotHaveDirectory_And_WithoutAnyDirectories_ShouldNotThrow(string path1,
		string path2)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();

		fileSystem.Should().NotHaveDirectory(path1).And.NotHaveDirectory(path2);
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void NotHaveDirectory_InvalidPath_ShouldThrow(string? invalidPath, string because)
	{
		MockFileSystem fileSystem = new();
		FileSystemAssertions sut = fileSystem.Should();

		Exception? exception = Record.Exception(() =>
		{
			sut.NotHaveDirectory(invalidPath!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void NotHaveDirectory_WithDirectory_ShouldThrow(
		string path,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithSubdirectory(path);

		Exception? exception = Record.Exception(() =>
		{
			fileSystem.Should().NotHaveDirectory(path, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected filesystem to not contain directory \"{path}\" {because}, but it did exist.");
	}

	[Theory]
	[AutoData]
	public void NotHaveDirectory_WithoutDirectory_ShouldNotThrow(string path)
	{
		MockFileSystem fileSystem = new();

		fileSystem.Should().NotHaveDirectory(path);
	}

	[Theory]
	[AutoData]
	public void NotHaveFile_And_WithOnlyOneFile_ShouldThrow(string path1, string path2)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(path2);

		Exception? exception = Record.Exception(() =>
		{
			fileSystem.Should().NotHaveFile(path1).And.NotHaveFile(path2);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain(path2);
		exception.Message.Should().NotContain(path1);
	}

	[Theory]
	[AutoData]
	public void NotHaveFile_And_WithoutAnyFiles_ShouldNotThrow(string path1, string path2)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();

		fileSystem.Should().NotHaveFile(path1).And.NotHaveFile(path2);
	}

	[Theory]
	[InlineAutoData(null)]
	[InlineAutoData("")]
	public void NotHaveFile_InvalidPath_ShouldThrow(string? invalidPath, string because)
	{
		MockFileSystem fileSystem = new();
		FileSystemAssertions sut = fileSystem.Should();

		Exception? exception = Record.Exception(() =>
		{
			sut.NotHaveFile(invalidPath!, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().NotBeNullOrEmpty();
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void NotHaveFile_WithFile_ShouldThrow(string path, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(path);

		Exception? exception = Record.Exception(() =>
		{
			fileSystem.Should().NotHaveFile(path, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be($"Expected filesystem to not contain file \"{path}\" {because}, but it did exist.");
	}

	[Theory]
	[AutoData]
	public void NotHaveFile_WithoutFile_ShouldNotThrow(string path)
	{
		MockFileSystem fileSystem = new();

		fileSystem.Should().NotHaveFile(path);
	}
}
