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
	///     Asserts that the current file or directory exists.
	/// </summary>
	public AndConstraint<TFileSystemInfo> Exist(
		string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.Given(() => Subject)
			.ForCondition(fileInfo => fileInfo.Exists)
			.FailWith(
				"Expected {context} {0} to exist{reason}, but it did not.",
				_ => Subject.Name);

		return new AndConstraint<TFileSystemInfo>(Subject);
	}
}
