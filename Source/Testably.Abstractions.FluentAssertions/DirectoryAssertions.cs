namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IDirectoryInfo" />.
/// </summary>
public class DirectoryAssertions :
	ReferenceTypeAssertions<IDirectoryInfo?, DirectoryAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "directory";

	internal DirectoryAssertions(IDirectoryInfo? instance)
		: base(instance)
	{
	}

	/// <summary>
	///     Asserts that the current directory has at least one file which matches the <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryAssertions> HasFileMatching(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
		=> HasFilesMatching(searchPattern, 1, because, becauseArgs);

	/// <summary>
	///     Asserts that the current directory has at least <paramref name="minimumCount"/> files which match the <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryAssertions> HasFilesMatching(
		string searchPattern = "*", int minimumCount = 1, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert a directory having files if the DirectoryInfo is null")
			.Then
			.ForCondition(!string.IsNullOrEmpty(searchPattern))
			.FailWith(
				"You can't assert a directory having files if you don't pass a proper name")
			.Then
			.Given(() => Subject!)
			.ForCondition(directoryInfo => directoryInfo.GetFiles(searchPattern).Length > 0)
			.FailWith(
				$"Expected {{context}} {{1}} to contain at least {(minimumCount == 1 ? "one file" : $"{minimumCount} files")} matching {{0}}{{reason}}, but none was found.",
				_ => searchPattern, directoryInfo => directoryInfo.Name);

		return new AndConstraint<DirectoryAssertions>(this);
	}
}
