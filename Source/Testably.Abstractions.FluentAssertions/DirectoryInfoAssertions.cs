namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IDirectoryInfo" />.
/// </summary>
public class DirectoryInfoAssertions :
	FileSystemInfoAssertions<IDirectoryInfo, DirectoryInfoAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "directory";

	internal DirectoryInfoAssertions(IDirectoryInfo? instance)
		: base(instance)
	{
	}

	/// <summary>
	///     Asserts that the current directory has at least one file which matches the <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryInfoAssertions> HaveFileMatching(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
	{
		new DirectoryAssertions(Subject).HasFileMatching(searchPattern, because, becauseArgs);
		return new AndConstraint<DirectoryInfoAssertions>(this);
	}
}
