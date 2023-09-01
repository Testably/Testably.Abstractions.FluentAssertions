namespace Testably.Abstractions.FluentAssertions;

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
		Execute.Assertion
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
}
