using System.Text;

namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IFileInfo" />.
/// </summary>
public class FileInfoAssertions :
	FileSystemInfoAssertions<IFileInfo, FileInfoAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "file";

	internal FileInfoAssertions(IFileInfo instance)
		: base(instance)
	{
	}

	/// <summary>
	///     Asserts that the current file is not read-only.
	/// </summary>
	public AndConstraint<FileInfoAssertions> IsNotReadOnly(
		string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.Given(() => Subject)
			.ForCondition(fileInfo => !fileInfo.IsReadOnly)
			.FailWith(
				"Expected {context} {0} not to be read-only{reason}, but it was.",
				_ => Subject.Name);

		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current file is read-only.
	/// </summary>
	public AndConstraint<FileInfoAssertions> IsReadOnly(
		string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.Given(() => Subject)
			.ForCondition(fileInfo => fileInfo.IsReadOnly)
			.FailWith(
				"Expected {context} {0} to be read-only{reason}, but it was not.",
				_ => Subject.Name);

		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the string content of the current file matches the <paramref name="pattern" />.
	/// </summary>
	public AndConstraint<FileInfoAssertions> HasContentMatching(
		Match pattern, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.Given(() => Subject)
			.ForCondition(fileInfo => pattern.Matches(
				fileInfo.FileSystem.File.ReadAllText(fileInfo.FullName)))
			.FailWith(
				"Expected {context} {0} to match '{1}'{reason}, but it did not.",
				_ => Subject.Name, _ => pattern);

		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the string content of the current file using the given <paramref name="encoding" />
	///     matches the <paramref name="pattern" />.
	/// </summary>
	public AndConstraint<FileInfoAssertions> HasContentMatching(
		Match pattern, Encoding encoding, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.Given(() => Subject)
			.ForCondition(fileInfo => pattern.Matches(
				fileInfo.FileSystem.File.ReadAllText(fileInfo.FullName, encoding)))
			.FailWith(
				"Expected {context} {0} to match '{1}'{reason}, but it did not.",
				_ => Subject.Name, _ => pattern);

		return new AndConstraint<FileInfoAssertions>(this);
	}
}
