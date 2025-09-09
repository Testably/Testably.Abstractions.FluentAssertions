namespace Testably.Abstractions.AwesomeAssertions;

/// <summary>
///     Assertions on <see cref="IFileSystem" />.
/// </summary>
public class FileSystemAssertions :
	ReferenceTypeAssertions<IFileSystem, FileSystemAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "filesystem";

	internal FileSystemAssertions(IFileSystem instance, AssertionChain currentAssertionChain)
		: base(instance, currentAssertionChain)
	{
	}

	/// <summary>
	///     Asserts that a directory at <paramref name="path" /> exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, DirectoryAssertions> HaveDirectory(
		string path, string because = "", params object[] becauseArgs)
	{
	 	CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(path))
			.FailWith("You can't assert that a directory exists if you don't pass a proper path.")
			.Then
			.Given(() => Subject.DirectoryInfo.New(path))
			.ForCondition(directoryInfo => directoryInfo.Exists)
			.FailWith(
				"Expected {context} to contain directory {0}{reason}, but it did not exist.",
				_ => path, directoryInfo => directoryInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, DirectoryAssertions>(this,
			new DirectoryAssertions(Subject.DirectoryInfo.New(path), CurrentAssertionChain));
	}

	/// <summary>
	///     Asserts that a file at <paramref name="path" /> exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, FileAssertions> HaveFile(
		string path, string because = "", params object[] becauseArgs)
	{
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(path))
			.FailWith("You can't assert that a file exists if you don't pass a proper path.")
			.Then
			.Given(() => Subject.FileInfo.New(path))
			.ForCondition(fileInfo => fileInfo.Exists)
			.FailWith(
				"Expected {context} to contain file {0}{reason}, but it did not exist.",
				_ => path, fileInfo => fileInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, FileAssertions>(this,
			new FileAssertions(Subject.FileInfo.New(path), CurrentAssertionChain));
	}

	/// <summary>
	///     Asserts that no directory at <paramref name="path" /> exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, DirectoryAssertions> NotHaveDirectory(
		string path, string because = "", params object[] becauseArgs)
	{
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(path))
			.FailWith(
				"You can't assert that a directory does not exist if you don't pass a proper path.")
			.Then
			.Given(() => Subject.DirectoryInfo.New(path))
			.ForCondition(directoryInfo => !directoryInfo.Exists)
			.FailWith(
				"Expected {context} to not contain directory {0}{reason}, but it did exist.",
				_ => path, directoryInfo => directoryInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, DirectoryAssertions>(this,
			new DirectoryAssertions(Subject.DirectoryInfo.New(path), CurrentAssertionChain));
	}

	/// <summary>
	///     Asserts that no file at <paramref name="path" /> exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, FileAssertions> NotHaveFile(
		string path, string because = "", params object[] becauseArgs)
	{
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(path))
			.FailWith(
				"You can't assert that a file does not exist if you don't pass a proper path.")
			.Then
			.Given(() => Subject.FileInfo.New(path))
			.ForCondition(fileInfo => !fileInfo.Exists)
			.FailWith(
				"Expected {context} to not contain file {0}{reason}, but it did exist.",
				_ => path, fileInfo => fileInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, FileAssertions>(this,
			new FileAssertions(Subject.FileInfo.New(path), CurrentAssertionChain));
	}
}
