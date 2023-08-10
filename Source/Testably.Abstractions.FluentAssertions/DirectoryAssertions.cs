﻿namespace Testably.Abstractions.FluentAssertions;

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
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(!string.IsNullOrEmpty(searchPattern))
			.FailWith(
				"You can't assert a file exist in the directory if you don't pass a proper name")
			.Then
			.Given(() => Subject.GetFiles(searchPattern))
			.ForCondition(fileInfos => fileInfos.Length > 0)
			.FailWith(
				"Expected {context} {1} to contain at least one file matching {0}{reason}, but none was found.",
				_ => searchPattern, _ => Subject.Name);

		return new AndConstraint<DirectoryAssertions>(this);
	}
}
