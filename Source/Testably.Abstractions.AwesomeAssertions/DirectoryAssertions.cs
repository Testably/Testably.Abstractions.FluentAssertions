using System.Linq;

namespace Testably.Abstractions.AwesomeAssertions;

/// <summary>
///     Assertions on <see cref="IDirectoryInfo" />.
/// </summary>
public class DirectoryAssertions :
	ReferenceTypeAssertions<IDirectoryInfo?, DirectoryAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "directory";

	internal DirectoryAssertions(IDirectoryInfo? instance, AssertionChain currentAssertionChain)
		: base(instance, currentAssertionChain)
	{
	}

	/// <summary>
	///     Asserts that the current directory has at least <paramref name="minimumCount" /> directories which match the
	///     <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryAssertions> HasDirectories(
		string searchPattern,
		int minimumCount,
		string because = "",
		params object[] becauseArgs)
	{
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert a directory having directories if the DirectoryInfo is null.")
			.Then
			.ForCondition(!string.IsNullOrEmpty(searchPattern))
			.FailWith(
				"You can't assert a directory having directories if you don't pass a proper search pattern.")
			.Then
			.Given(() => Subject!)
			.ForCondition(directoryInfo
				=> directoryInfo.GetDirectories(searchPattern).Length >= minimumCount)
			.FailWith(
				$"Expected {{context}} {{1}} to contain at least {(minimumCount == 1 ? "one directory" : $"{minimumCount} directories")} matching {{0}}{{reason}}, but {(minimumCount == 1 ? "none was" : "only {2} were")} found.",
				_ => searchPattern,
				directoryInfo => directoryInfo.Name,
				directoryInfo => directoryInfo.GetDirectories(searchPattern).Length);

		return new AndConstraint<DirectoryAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current directory has at least one directory which matches the <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryAssertions> HasDirectories(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
		=> HasDirectories(searchPattern, 1, because, becauseArgs);

	/// <summary>
	///     Asserts that the directory contains exactly one directory matching the given <paramref name="searchPattern" />.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, DirectoryAssertions> HasDirectory(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
	{
		var subdirectory = Subject?.GetDirectories(searchPattern).FirstOrDefault();
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert a directory having a given directory if it is null.")
			.Then
			.ForCondition(!string.IsNullOrEmpty(searchPattern))
			.FailWith(
				"You can't assert a directory having a given directory if you don't pass a proper search pattern.")
			.Then
			.Given(() => Subject!)
			.ForCondition(directoryInfo
				=> directoryInfo.GetDirectories(searchPattern).Length <= 1)
			.FailWith(
				"Expected {context} {1} to contain exactly one directory matching {0}{reason}, but found {2}.",
				_ => searchPattern,
				directoryInfo => directoryInfo.Name,
				directoryInfo => directoryInfo.GetDirectories(searchPattern).Length)
			.Then
			.ForCondition(_ => subdirectory != null)
			.FailWith(
				"Expected {context} {1} to contain exactly one directory matching {0}{reason}, but found none.",
				_ => searchPattern,
				directoryInfo => directoryInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, DirectoryAssertions>(
			new FileSystemAssertions(Subject!.FileSystem),
			new DirectoryAssertions(subdirectory));
	}

	/// <summary>
	///     Asserts that the directory contains exactly one file matching the given <paramref name="searchPattern" />.
	/// </summary>
	public AndWhichConstraint<FileSystemAssertions, FileAssertions> HasFile(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
	{
		var file = Subject?.GetFiles(searchPattern).FirstOrDefault();
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert a directory having a given file if it is null.")
			.Then
			.ForCondition(!string.IsNullOrEmpty(searchPattern))
			.FailWith(
				"You can't assert a directory having a given file if you don't pass a proper search pattern.")
			.Then
			.Given(() => Subject!)
			.ForCondition(directoryInfo
				=> directoryInfo.GetFiles(searchPattern).Length <= 1)
			.FailWith(
				"Expected {context} {1} to contain exactly one file matching {0}{reason}, but found {2}.",
				_ => searchPattern,
				directoryInfo => directoryInfo.Name,
				directoryInfo => directoryInfo.GetFiles(searchPattern).Length)
			.Then
			.ForCondition(_ => file != null)
			.FailWith(
				"Expected {context} {1} to contain exactly one file matching {0}{reason}, but found none.",
				_ => searchPattern,
				directoryInfo => directoryInfo.Name);

		return new AndWhichConstraint<FileSystemAssertions, FileAssertions>(
			new FileSystemAssertions(Subject!.FileSystem),
			new FileAssertions(file));
	}

	/// <summary>
	///     Asserts that the current directory has at least one file which matches the <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryAssertions> HasFiles(
		string searchPattern = "*", string because = "", params object[] becauseArgs)
		=> HasFiles(searchPattern, 1, because, becauseArgs);

	/// <summary>
	///     Asserts that the current directory has at least <paramref name="minimumCount" /> files which match the
	///     <paramref name="searchPattern" />.
	/// </summary>
	public AndConstraint<DirectoryAssertions> HasFiles(
		string searchPattern,
		int minimumCount,
		string because = "",
		params object[] becauseArgs)
	{
		CurrentAssertionChain
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert a directory having files if the DirectoryInfo is null.")
			.Then
			.ForCondition(!string.IsNullOrEmpty(searchPattern))
			.FailWith(
				"You can't assert a directory having files if you don't pass a proper search pattern.")
			.Then
			.Given(() => Subject!)
			.ForCondition(directoryInfo
				=> directoryInfo.GetFiles(searchPattern).Length >= minimumCount)
			.FailWith(
				$"Expected {{context}} {{1}} to contain at least {(minimumCount == 1 ? "one file" : $"{minimumCount} files")} matching {{0}}{{reason}}, but {(minimumCount == 1 ? "none was" : "only {2} were")} found.",
				_ => searchPattern,
				directoryInfo => directoryInfo.Name,
				directoryInfo => directoryInfo.GetFiles(searchPattern).Length);

		return new AndConstraint<DirectoryAssertions>(this);
	}
}
