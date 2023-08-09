namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IFileSystem" />.
/// </summary>
public class FileSystemAssertions :
	ReferenceTypeAssertions<IFileSystem, FileSystemAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "filesystem";

	internal FileSystemAssertions(IFileSystem instance)
		: base(instance)
	{
	}

	/// <summary>
	///     Asserts that a directory at <paramref name="path" /> exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, DirectoryInfoAssertions> HaveDirectory(
		string path, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(path))
			.FailWith("You can't assert that a directory exists if you don't pass a proper name.")
			.Then
			.Given(() => Subject.DirectoryInfo.New(path))
			.ForCondition(directoryInfo => directoryInfo.Exists)
			.FailWith(
				"Expected {context} to contain directory {0}{reason}, but it did not exist.",
				_ => path, directoryInfo => directoryInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, DirectoryInfoAssertions>(this,
			new DirectoryInfoAssertions(Subject.DirectoryInfo.New(path)));
	}

	/// <summary>
	///     Asserts that a file at <paramref name="path" /> exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, FileInfoAssertions> HaveFile(
		string path, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(path))
			.FailWith("You can't assert that a file exists if you don't pass a proper name.")
			.Then
			.Given(() => Subject.FileInfo.New(path))
			.ForCondition(fileInfo => fileInfo.Exists)
			.FailWith(
				"Expected {context} to contain file {0}{reason}, but it did not exist.",
				_ => path, fileInfo => fileInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, FileInfoAssertions>(this,
			new FileInfoAssertions(Subject.FileInfo.New(path)));
	}

	/// <summary>
	///     Asserts that a directory at <paramref name="path" /> exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, DirectoryInfoAssertions> NotHaveDirectory(
		string path, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(path))
			.FailWith(
				"You can't assert that a directory does not exist if you don't pass a proper name.")
			.Then
			.Given(() => Subject.DirectoryInfo.New(path))
			.ForCondition(directoryInfo => !directoryInfo.Exists)
			.FailWith(
				"Expected {context} to not contain directory {0}{reason}, but it did exist.",
				_ => path, directoryInfo => directoryInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, DirectoryInfoAssertions>(this,
			new DirectoryInfoAssertions(Subject.DirectoryInfo.New(path)));
	}

	/// <summary>
	///     Asserts that a file at <paramref name="path" /> exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, FileInfoAssertions> NotHaveFile(
		string path, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(path))
			.FailWith(
				"You can't assert that a file does not exist if you don't pass a proper name.")
			.Then
			.Given(() => Subject.FileInfo.New(path))
			.ForCondition(fileInfo => !fileInfo.Exists)
			.FailWith(
				"Expected {context} to not contain file {0}{reason}, but it did exist.",
				_ => path, fileInfo => fileInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, FileInfoAssertions>(this,
			new FileInfoAssertions(Subject.FileInfo.New(path)));
	}
}
