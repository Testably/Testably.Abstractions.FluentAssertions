using AutoFixture.Xunit2;
using FluentAssertions;
using System;
using System.IO.Abstractions;
using Testably.Abstractions.Testing;
using Testably.Abstractions.Testing.Statistics;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public class StatisticAssertionsCountTests
{
	[Theory]
	[InlineAutoData(2, 2)]
	[InlineAutoData(4, 3)]
	[InlineAutoData(5, 5)]
	[InlineAutoData(6, 0)]
	public void AtLeast_WhenCalledEnough_ShouldNotThrow(int callCount, int expectedCount)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtLeast(expectedCount);
	}

	[Theory]
	[InlineAutoData(0, "never", 1, "once")]
	[InlineAutoData(0, "never", 8, "8 times")]
	[InlineAutoData(5, "5 times", 6, "6 times")]
	public void AtLeast_WhenCalledLess_ShouldThrow(
		int actualCount, string actualText, int expectedCount, string expectedText,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < actualCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtLeast(expectedCount, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called at least {expectedText} {because}, but it was {actualText}.");
	}

	[Theory]
	[InlineAutoData(1)]
	[InlineAutoData(2)]
	[InlineAutoData(3)]
	public void AtLeastOnce_WhenCalledAtLeastOnce_ShouldNotThrow(int callCount)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtLeastOnce();
	}

	[Theory]
	[InlineAutoData(0, "never")]
	public void AtLeastOnce_WhenCalledLessThanOnce_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtLeastOnce(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called at least once {because}, but it was {text}.");
	}

	[Theory]
	[InlineAutoData(2)]
	[InlineAutoData(3)]
	public void AtLeastTwice_WhenCalledAtLeastTwice_ShouldNotThrow(int callCount)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtLeastTwice();
	}

	[Theory]
	[InlineAutoData(0, "never")]
	[InlineAutoData(1, "once")]
	public void AtLeastTwice_WhenCalledLessThanTwice_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtLeastTwice(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called at least twice {because}, but it was {text}.");
	}

	[Theory]
	[InlineAutoData(2, 2)]
	[InlineAutoData(3, 4)]
	[InlineAutoData(5, 5)]
	[InlineAutoData(0, 6)]
	public void AtMost_WhenCalledEnough_ShouldNotThrow(int callCount, int expectedCount)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtMost(expectedCount);
	}

	[Theory]
	[InlineAutoData(1, "once", 0, "never")]
	[InlineAutoData(8, "8 times", 0, "never")]
	[InlineAutoData(6, "6 times", 5, "5 times")]
	public void AtMost_WhenCalledLess_ShouldThrow(
		int actualCount, string actualText, int expectedCount, string expectedText,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < actualCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtMost(expectedCount, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called at most {expectedText} {because}, but it was {actualText}.");
	}

	[Theory]
	[InlineAutoData(0)]
	[InlineAutoData(1)]
	public void AtMostOnce_WhenCalledAtMostOnce_ShouldNotThrow(int callCount)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtMostOnce();
	}

	[Theory]
	[InlineAutoData(2, "twice")]
	[InlineAutoData(3, "3 times")]
	public void AtMostOnce_WhenCalledMoreThanOnce_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtMostOnce(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called at most once {because}, but it was {text}.");
	}

	[Theory]
	[InlineAutoData(0)]
	[InlineAutoData(1)]
	[InlineAutoData(2)]
	public void AtMostTwice_WhenCalledAtMostTwice_ShouldNotThrow(int callCount)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtMostTwice();
	}

	[Theory]
	[InlineAutoData(3, "3 times")]
	public void AtMostTwice_WhenCalledMoreThanTwice_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).AtMostTwice(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called at most twice {because}, but it was {text}.");
	}

	[Theory]
	[InlineAutoData(0)]
	[InlineAutoData(1)]
	[InlineAutoData(2)]
	[InlineAutoData(3)]
	[AutoData]
	public void Exactly_WhenCalledCorrectTimes_ShouldNotThrow(int count)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < count; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Exactly(count);
	}

	[Theory]
	[InlineAutoData(0, "never", 1, "once")]
	[InlineAutoData(1, "once", 0, "never")]
	[InlineAutoData(5, "5 times", 6, "6 times")]
	[InlineAutoData(8, "8 times", 7, "7 times")]
	public void Exactly_WhenCalledDifferentTimes_ShouldThrow(
		int actualCount, string actualText, int expectedCount, string expectedText,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < actualCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Exactly(expectedCount, because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called {expectedText} {because}, but it was {actualText}.");
	}

	[Theory]
	[InlineAutoData(1, "once")]
	[InlineAutoData(2, "twice")]
	[InlineAutoData(3, "3 times")]
	public void Never_WhenCalled_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Never(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to never be called {because}, but it was {text}.");
	}

	[Fact]
	public void Never_WhenNotCalled_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Never();
	}

	[Theory]
	[InlineAutoData(0, "never")]
	[InlineAutoData(2, "twice")]
	[InlineAutoData(3, "3 times")]
	public void Once_WhenCalled_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Once(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called once {because}, but it was {text}.");
	}

	[Fact]
	public void Once_WhenCalledOnce_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Once();
	}

	[Theory]
	[InlineAutoData(0, "never")]
	[InlineAutoData(1, "once")]
	[InlineAutoData(3, "3 times")]
	public void Twice_WhenCalled_ShouldThrow(
		int callCount, string text,
		string because)
	{
		MockFileSystem fileSystem = new();
		for (int i = 0; i < callCount; i++)
		{
			fileSystem.File.WriteAllText($"foo-{i}", "bar");
		}

		IStatistics<IFile> sut = fileSystem.Statistics.File;

		Exception? exception = Record.Exception(() =>
		{
			sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Twice(because);
		});

		exception.Should().NotBeNull();
		exception!.Message.Should()
			.Be(
				$"Expected method `WriteAllText` to be called twice {because}, but it was {text}.");
	}

	[Fact]
	public void Twice_WhenCalledTwice_ShouldNotThrow()
	{
		MockFileSystem fileSystem = new();
		fileSystem.File.WriteAllText("foo-1", "bar");
		fileSystem.File.WriteAllText("foo-2", "bar");
		IStatistics<IFile> sut = fileSystem.Statistics.File;

		sut.Should().HaveCalled(nameof(IFile.WriteAllText)).Twice();
	}
}
