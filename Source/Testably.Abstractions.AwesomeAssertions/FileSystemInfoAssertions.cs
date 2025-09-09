namespace Testably.Abstractions.AwesomeAssertions;

/// <summary>
///     Assertions on <see cref="IFileSystemInfo" />.
/// </summary>
public class FileSystemInfoAssertions :
	FileSystemInfoAssertions<IFileSystemInfo, FileSystemInfoAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "file system info";

	internal FileSystemInfoAssertions(IFileSystemInfo? instance)
		: base(instance)
	{
	}
}

/// <summary>
///     Assertions on <see cref="IFileSystemInfo" />.
/// </summary>
public abstract class
	FileSystemInfoAssertions<TFileSystemInfo, TAssertion>
	: ReferenceTypeAssertions<TFileSystemInfo?, TAssertion>
	where TFileSystemInfo : IFileSystemInfo
	where TAssertion : ReferenceTypeAssertions<TFileSystemInfo?, TAssertion>
{
	/// <summary>
	///     Initializes a new instance of <see cref="FileSystemInfoAssertions{TFileSystemInfo,TAssertion}" />
	/// </summary>
	protected FileSystemInfoAssertions(TFileSystemInfo? subject) : base(subject)
	{
	}

	/// <summary>
	///     Asserts that the current file or directory exists.
	/// </summary>
	public AndConstraint<TFileSystemInfo> Exist(
		string because = "", params object[] becauseArgs)
	{
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith("You can't assert that the {context} exists if it is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileSystemInfo => fileSystemInfo.Exists)
			.FailWith(
				"Expected {context} {0} to exist{reason}, but it did not.",
				fileSystemInfo => fileSystemInfo.Name);

		return new AndConstraint<TFileSystemInfo>(Subject!);
	}

	/// <summary>
	///     Asserts that the current file or directory does not exist.
	/// </summary>
	public AndConstraint<TFileSystemInfo> NotExist(
		string because = "", params object[] becauseArgs)
	{
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith("You can't assert that the {context} does not exist if it is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileSystemInfo => !fileSystemInfo.Exists)
			.FailWith(
				"Expected {context} {0} not to exist{reason}, but it did.",
				fileSystemInfo => fileSystemInfo.Name);

		return new AndConstraint<TFileSystemInfo>(Subject!);
	}
}
