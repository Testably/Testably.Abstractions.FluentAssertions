namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IDirectoryInfo" />.
/// </summary>
public class DirectoryAssertions :
	ReferenceTypeAssertions<IDirectoryInfo, DirectoryAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "directory";

	internal DirectoryAssertions(IDirectoryInfo instance)
		: base(instance)
	{
	}

	/// <summary>
	///     Asserts that the current directory has at least one file which matches the <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryAssertions> HasFileMatching(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
	{
		Subject.Should().HaveFileMatching(searchPattern, because, becauseArgs);
		return new AndConstraint<DirectoryAssertions>(this);
	}
}
