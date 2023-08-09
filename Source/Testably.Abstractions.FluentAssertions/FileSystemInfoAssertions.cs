namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IFileSystemInfo" />.
/// </summary>
public abstract class
	FileSystemInfoAssertions<TFileSystemInfo, TAssertion>
	: ReferenceTypeAssertions<TFileSystemInfo, TAssertion>
	where TFileSystemInfo : IFileSystemInfo
	where TAssertion : ReferenceTypeAssertions<TFileSystemInfo, TAssertion>
{
	/// <summary>
	///     Initializes a new instance of <see cref="FileSystemInfoAssertions{TFileSystemInfo,TAssertion}" />
	/// </summary>
	protected FileSystemInfoAssertions(TFileSystemInfo subject) : base(subject)
	{
	}

	/// <summary>
	///     Asserts that the current <see cref="IFileSystemInfo"/> is a directory which exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemInfoAssertions<TFileSystemInfo, TAssertion>, DirectoryInfoAssertions> ShouldBeADirectory(
		string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.BecauseOf(because, becauseArgs)
			.Given(() => Subject)
			.ForCondition(fileSystemInfo => fileSystemInfo is IDirectoryInfo directoryInfo && directoryInfo.Exists)
			.FailWith(
				"Expected '{0}' to be a directory{reason}, but it did not exist.",
				_ => Subject.Name);

		return new AndWhichConstraint<FileSystemInfoAssertions<TFileSystemInfo, TAssertion>, DirectoryInfoAssertions>(this,
			new DirectoryInfoAssertions((IDirectoryInfo)Subject));
	}

	/// <summary>
	///     Asserts that the current <see cref="IFileSystemInfo"/> is a file which exists in the file system.
	/// </summary>
	public AndWhichConstraint<FileSystemInfoAssertions<TFileSystemInfo, TAssertion>, FileInfoAssertions> ShouldBeAFile(
		string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.BecauseOf(because, becauseArgs)
			.Given(() => Subject)
			.ForCondition(fileSystemInfo => fileSystemInfo is IFileInfo fileInfo && fileInfo.Exists)
			.FailWith(
				"Expected '{0}' to be a file{reason}, but it did not exist.",
				_ => Subject.Name);

		return new AndWhichConstraint<FileSystemInfoAssertions<TFileSystemInfo, TAssertion>, FileInfoAssertions>(this,
			new FileInfoAssertions((IFileInfo)Subject));
	}

	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "filesysteminfo";
}
