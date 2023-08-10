﻿using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.Linq;
using System.Text;
using Testably.Abstractions.Testing;
using Testably.Abstractions.Testing.FileSystemInitializer;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class FileAssertionsTests
{
	[Theory]
	[AutoData]
	public void HasContent_Bytes_FullContent_ShouldNotThrow(
		byte[] bytes, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasBytesContent(bytes));
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;

		sut.HasContent(bytes);
	}

	[Theory]
	[AutoData]
	public void HasContent_Bytes_OnlyPartOfContent_ShouldNotThrow(
		byte[] bytes, string fileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasBytesContent(bytes));
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;
		byte[] checkedBytes = bytes.Skip(1).ToArray();

		Exception? exception = Record.Exception(() =>
		{
			sut.HasContent(checkedBytes, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to match '{{{string.Join(", ", checkedBytes.Select(b => "0x" + b.ToString("X")))}}}' {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void HasContent_FullContent_ShouldNotThrow(
		string content, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasStringContent(content));
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;

		sut.HasContent(content);
	}

	[Theory]
	[AutoData]
	public void HasContent_FullContent_WithEncoding_ShouldNotThrow(
		Encoding encoding, string content, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, content, encoding);
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;

		sut.HasContent(content, encoding);
	}

	[Theory]
	[AutoData]
	public void HasContent_OnlyPartOfContentWithoutWildcards_ShouldThrow(
		string content, string fileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasStringContent(content));
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;
		string pattern = content.Substring(1);

		Exception? exception = Record.Exception(() =>
		{
			sut.HasContent(pattern, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to match '{pattern}' {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void HasContent_OnlyPartOfContentWithoutWildcards_WithEncoding_ShouldThrow(
		Encoding encoding, string content, string fileName, string because)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, content, encoding);
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;
		string pattern = content.Substring(1);

		Exception? exception = Record.Exception(() =>
		{
			sut.HasContent(pattern, encoding, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to match '{pattern}' {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void HasContent_OnlyPartOfContentWithWildcard_ShouldNotThrow(
		string content, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.WithFile(fileName).Which(f => f.HasStringContent(content));
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;
		string pattern = "*" + content.Substring(1);

		sut.HasContent(pattern);
	}

	[Theory]
	[AutoData]
	public void HasContent_OnlyPartOfContentWithWildcard_WithEncoding_ShouldNotThrow(
		Encoding encoding, string content, string fileName)
	{
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, content, encoding);
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;
		string pattern = "*" + content.Substring(1);

		sut.HasContent(pattern);
	}

	[Theory]
	[AutoData]
	public void HasContent_WithEncodingMismatch_ShouldThrow(
		string fileName, string because)
	{
		string content = "Dans mes rêves";
		MockFileSystem fileSystem = new();
		fileSystem.Initialize();
		fileSystem.File.WriteAllText(fileName, content, Encoding.Default);
		FileAssertions? sut = fileSystem.Should().HaveFile(fileName).Which;
		string pattern = content;

		Exception? exception = Record.Exception(() =>
		{
			sut.HasContent(pattern, Encoding.ASCII, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileName}\" to match '{pattern}' {because}, but it did not.");
	}

	[Theory]
	[AutoData]
	public void IsNotReadOnly_WithReadOnlyFile_ShouldThrow(
		FileDescription fileDescription,
		string because)
	{
		fileDescription.IsReadOnly = true;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		FileAssertions? sut = fileSystem.Should().HaveFile(fileDescription.Name).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.IsNotReadOnly(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileDescription.Name}\" not to be read-only {because}, but it was.");
	}

	[Theory]
	[AutoData]
	public void IsNotReadOnly_WithWritableFile_ShouldNotThrow(FileDescription fileDescription)
	{
		fileDescription.IsReadOnly = false;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		FileAssertions? sut = fileSystem.Should().HaveFile(fileDescription.Name).Which;

		sut.IsNotReadOnly();
	}

	[Theory]
	[AutoData]
	public void IsReadOnly_WithReadOnlyFile_ShouldNotThrow(FileDescription fileDescription)
	{
		fileDescription.IsReadOnly = true;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		FileAssertions? sut = fileSystem.Should().HaveFile(fileDescription.Name).Which;

		sut.IsReadOnly();
	}

	[Theory]
	[AutoData]
	public void IsReadOnly_WithWritableFile_ShouldThrow(
		FileDescription fileDescription,
		string because)
	{
		fileDescription.IsReadOnly = false;
		MockFileSystem fileSystem = new();
		fileSystem.Initialize()
			.With(fileDescription);
		FileAssertions? sut = fileSystem.Should().HaveFile(fileDescription.Name).Which;

		Exception? exception = Record.Exception(() =>
		{
			sut.IsReadOnly(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected file \"{fileDescription.Name}\" to be read-only {because}, but it was not.");
	}
}
