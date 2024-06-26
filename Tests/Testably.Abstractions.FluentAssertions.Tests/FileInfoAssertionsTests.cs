﻿using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using Testably.Abstractions.Testing;
using Testably.Abstractions.Testing.Initializer;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class FileInfoAssertionsTests
{
	[Theory]
	[AutoData]
	public void BeReadOnly_Null_ShouldThrow(string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().BeReadOnly(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void BeReadOnly_WithReadOnlyFile_ShouldNotThrow(FileDescription fileDescription)
	{
		fileDescription.IsReadOnly = true;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		IFileInfo sut = fileSystem.FileInfo.New(fileDescription.Name);

		sut.Should().BeReadOnly();
	}

	[Theory]
	[AutoData]
	public void BeReadOnly_WithWritableFile_ShouldThrow(
		FileDescription fileDescription,
		string because)
	{
		fileDescription.IsReadOnly = false;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		IFileInfo sut = fileSystem.FileInfo.New(fileDescription.Name);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().BeReadOnly(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileDescription.Name}\" to be read-only {because}, but it was not.");
	}

	[Theory]
	[AutoData]
	public void HaveAttribute_Null_ShouldThrow(string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveAttribute(FileAttributes.ReadOnly, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveAttribute_WithAttribute_ShouldNotThrow(FileDescription fileDescription)
	{
		fileDescription.IsReadOnly = true;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		IFileInfo sut = fileSystem.FileInfo.New(fileDescription.Name);

		sut.Should().HaveAttribute(FileAttributes.ReadOnly);
	}

	[Theory]
	[AutoData]
	public void HaveAttribute_WithoutAttribute_ShouldThrow(
		FileDescription fileDescription,
		string because)
	{
		fileDescription.IsReadOnly = false;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		IFileInfo sut = fileSystem.FileInfo.New(fileDescription.Name);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveAttribute(FileAttributes.ReadOnly, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileDescription.Name}\" to have attribute {FileAttributes.ReadOnly} {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void HaveContent_Bytes_FullContent_ShouldNotThrow(
		byte[] bytes, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasBytesContent(bytes));
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		sut.Should().HaveContent(bytes);
	}

	[Theory]
	[AutoData]
	public void HaveContent_Bytes_Null_ShouldThrow(byte[] bytes, string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveContent(bytes, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveContent_Bytes_OnlyPartOfContent_ShouldNotThrow(
		byte[] bytes, string fileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasBytesContent(bytes));
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		byte[] checkedBytes = bytes.Skip(1).ToArray();

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveContent(checkedBytes, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to match '{{{string.Join(", ", checkedBytes.Select(b => "0x" + b.ToString("X2")))}}}' {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void HaveContent_FullContent_ShouldNotThrow(
		string content, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasStringContent(content));
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		sut.Should().HaveContent(content);
	}

	[Theory]
	[AutoData]
	public void HaveContent_FullContent_WithEncoding_ShouldNotThrow(
		Encoding encoding, string content, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, content, encoding);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		sut.Should().HaveContent(content, encoding);
	}

	[Theory]
	[AutoData]
	public void HaveContent_OnlyPartOfContentWithoutWildcards_ShouldThrow(
		string content, string fileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasStringContent(content));
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		string pattern = content.Substring(1);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveContent(pattern, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to match '{pattern}' {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void HaveContent_OnlyPartOfContentWithoutWildcards_WithEncoding_ShouldThrow(
		Encoding encoding, string content, string fileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, content, encoding);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		string pattern = content.Substring(1);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveContent(pattern, encoding, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to match '{pattern}' {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void HaveContent_OnlyPartOfContentWithWildcard_ShouldNotThrow(
		string content, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasStringContent(content));
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		string pattern = "*" + content.Substring(1);

		sut.Should().HaveContent(pattern);
	}

	[Theory]
	[AutoData]
	public void HaveContent_OnlyPartOfContentWithWildcard_WithEncoding_ShouldNotThrow(
		Encoding encoding, string content, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, content, encoding);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		string pattern = "*" + content.Substring(1);

		sut.Should().HaveContent(pattern);
	}

	[Theory]
	[AutoData]
	public void HaveContent_StringContent_Null_ShouldThrow(string content, string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveContent(content, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveContent_StringContent_WithEncoding_Null_ShouldThrow(
		Encoding encoding, string content, string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveContent(content, encoding, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void HaveContent_WithEncodingMismatch_ShouldThrow(
		string fileName, string because)
	{
		string content = "Dans mes rêves";
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, content, Encoding.Default);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		string pattern = content;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveContent(pattern, Encoding.ASCII, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to match '{pattern}' {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void HaveFileShare_Null_ShouldThrow(FileShare fileShare, string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFileShare(fileShare, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
		exception.Message.Should().Contain(fileShare.ToString());
	}

	[Theory]
	[InlineAutoData(FileShare.Read)]
	[InlineAutoData(FileShare.Write)]
	[InlineAutoData(FileShare.ReadWrite)]
	[InlineAutoData(FileShare.Delete)]
	[InlineAutoData(FileShare.None)]
	[InlineAutoData(FileShare.Inheritable)]
	public void HaveFileShare_UnlockedFile_ShouldHaveAllFileShares(
		FileShare fileShare,
		string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		sut.Should().HaveFileShare(fileShare);
	}

	[Theory]
	[InlineAutoData(FileShare.Read)]
	[InlineAutoData(FileShare.Write)]
	[InlineAutoData(FileShare.ReadWrite)]
	public void HaveFileShare_WhenLockDoesNotShare_ShouldThrow(
		FileShare fileShare,
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		using FileSystemStream stream = fileSystem.File.Open(
			fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFileShare(fileShare, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to have file share '{fileShare}' {because}, but it did not.");
	}

	[Theory]
	[InlineAutoData(FileShare.Read)]
	[InlineAutoData(FileShare.Write)]
	public void HaveFileShare_WhenLockSharesFile_ShouldNotThrow(
		FileShare fileShare,
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		using FileSystemStream stream = fileSystem.File.Open(
			fileName, FileMode.Open, FileAccess.ReadWrite, fileShare);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveFileShare(fileShare, because);
		});

		exception.Should().BeNull();
	}

	[Theory]
	[AutoData]
	public void NotBeReadOnly_Null_ShouldThrow(string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotBeReadOnly(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void NotBeReadOnly_WithReadOnlyFile_ShouldThrow(
		FileDescription fileDescription,
		string because)
	{
		fileDescription.IsReadOnly = true;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		IFileInfo sut = fileSystem.FileInfo.New(fileDescription.Name);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotBeReadOnly(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileDescription.Name}\" not to be read-only {because}, but it was.");
	}

	[Theory]
	[AutoData]
	public void NotBeReadOnly_WithWritableFile_ShouldNotThrow(FileDescription fileDescription)
	{
		fileDescription.IsReadOnly = false;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		IFileInfo sut = fileSystem.FileInfo.New(fileDescription.Name);

		sut.Should().NotBeReadOnly();
	}

	[Theory]
	[AutoData]
	public void NotHaveAttribute_Null_ShouldThrow(string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotHaveAttribute(FileAttributes.ReadOnly, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
	}

	[Theory]
	[AutoData]
	public void NotHaveAttribute_WithAttribute_ShouldThrow(
		FileDescription fileDescription,
		string because)
	{
		fileDescription.IsReadOnly = true;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		IFileInfo sut = fileSystem.FileInfo.New(fileDescription.Name);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotHaveAttribute(FileAttributes.ReadOnly, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileDescription.Name}\" not to have attribute {FileAttributes.ReadOnly} {because}, but it did.");
	}

	[Theory]
	[AutoData]
	public void NotHaveAttribute_WithoutAttribute_ShouldNotThrow(FileDescription fileDescription)
	{
		fileDescription.IsReadOnly = false;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		IFileInfo sut = fileSystem.FileInfo.New(fileDescription.Name);

		sut.Should().NotHaveAttribute(FileAttributes.ReadOnly);
	}

	[Theory]
	[AutoData]
	public void NotHaveFileShare_Null_ShouldThrow(FileShare fileShare, string because)
	{
		IFileInfo? sut = null;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotHaveFileShare(fileShare, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should().Contain("null");
		exception.Message.Should().NotContain(because);
		exception.Message.Should().Contain(fileShare.ToString());
	}

	[Theory]
	[InlineAutoData(FileShare.Read)]
	[InlineAutoData(FileShare.Write)]
	[InlineAutoData(FileShare.ReadWrite)]
	[InlineAutoData(FileShare.Delete)]
	[InlineAutoData(FileShare.None)]
	[InlineAutoData(FileShare.Inheritable)]
	public void NotHaveFileShare_UnlockedFile_ShouldThrow(
		FileShare fileShare,
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotHaveFileShare(fileShare, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" not to have file share '{fileShare}' {because}, but it did.");
	}

	[Theory]
	[InlineAutoData(FileShare.Read)]
	[InlineAutoData(FileShare.Write)]
	public void NotHaveFileShare_WhenLockDoesNotShare_ShouldNotThrow(
		FileShare fileShare,
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		using FileSystemStream stream = fileSystem.File.Open(
			fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.None);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotHaveFileShare(fileShare, because);
		});

		exception.Should().BeNull();
	}

	[Theory]
	[InlineAutoData(FileShare.Read)]
	[InlineAutoData(FileShare.Write)]
	[InlineAutoData(FileShare.ReadWrite)]
	public void NotHaveFileShare_WhenLockSharesFile_ShouldThrow(
		FileShare fileShare,
		string fileName,
		string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName);
		IFileInfo sut = fileSystem.FileInfo.New(fileName);
		using FileSystemStream stream = fileSystem.File.Open(
			fileName, FileMode.Open, FileAccess.ReadWrite, fileShare);

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().NotHaveFileShare(fileShare, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" not to have file share '{fileShare}' {because}, but it did.");
	}
}
