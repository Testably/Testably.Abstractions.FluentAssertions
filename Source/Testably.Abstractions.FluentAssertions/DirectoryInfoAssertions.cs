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
	///     Asserts that the current directory has at least one directory which matches the <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryInfoAssertions> HaveDirectories(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
	{
		new DirectoryAssertions(Subject).HasDirectories(searchPattern, because, becauseArgs);
		return new AndConstraint<DirectoryInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the directory contains exactly one directory matching the given <paramref name="searchPattern" />.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, DirectoryAssertions> HaveDirectory(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
	{
		return new DirectoryAssertions(Subject).HasDirectory(searchPattern, because,
			becauseArgs);
	}

	/// <summary>
	///     Asserts that the directory contains exactly one file matching the given <paramref name="searchPattern" />.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, FileAssertions> HaveFile(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
	{
		return new DirectoryAssertions(Subject).HasFile(searchPattern, because,
			becauseArgs);
	}

	/// <summary>
	///     Asserts that the current directory has at least one file which matches the <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryInfoAssertions> HaveFiles(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
	{
		new DirectoryAssertions(Subject).HasFiles(searchPattern, because, becauseArgs);
		return new AndConstraint<DirectoryInfoAssertions>(this);
	}
}
