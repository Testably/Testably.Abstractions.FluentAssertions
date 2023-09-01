using System.IO;
using System.Linq;
using System.Text;

namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IFileInfo" />.
/// </summary>
public class FileAssertions :
	ReferenceTypeAssertions<IFileInfo?, FileAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "file";

	internal FileAssertions(IFileInfo? instance)
		: base(instance)
	{
	}

	/// <summary>
	///     Asserts that the current file does not have the given <paramref name="attribute" />.
	/// </summary>
	public AndConstraint<FileAssertions> DoesNotHaveAttribute(
		FileAttributes attribute, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				$"You can't assert that the file does not have attribute {attribute} if it is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileInfo => !fileInfo.Attributes.HasFlag(attribute))
			.FailWith(
				$"Expected {{context}} {{0}} not to have attribute {attribute}{{reason}}, but it did.",
				fileInfo => fileInfo.Name);

		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current file has the given <paramref name="attribute" />.
	/// </summary>
	public AndConstraint<FileAssertions> HasAttribute(
		FileAttributes attribute, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				$"You can't assert that the file has attribute {attribute} if it is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileInfo => fileInfo.Attributes.HasFlag(attribute))
			.FailWith(
				$"Expected {{context}} {{0}} to have attribute {attribute}{{reason}}, but it did not.",
				fileInfo => fileInfo.Name);

		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the binary content of the current file is equivalent to the given <paramref name="bytes" />.
	/// </summary>
	public AndConstraint<FileAssertions> HasContent(
		byte[] bytes, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert the content of a file if the FileInfo is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileInfo => fileInfo.FileSystem.File
				.ReadAllBytes(fileInfo.FullName)
				.SequenceEqual(bytes))
			.FailWith(
				"Expected {context} {0} to match '{1}'{reason}, but it did not.",
				fileInfo => fileInfo.Name, _ => bytes);

		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the string content of the current file matches the <paramref name="pattern" />.
	/// </summary>
	public AndConstraint<FileAssertions> HasContent(
		Match pattern, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert the content of a file if the FileInfo is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileInfo => pattern.Matches(
				fileInfo.FileSystem.File.ReadAllText(fileInfo.FullName)))
			.FailWith(
				"Expected {context} {0} to match '{1}'{reason}, but it did not.",
				fileInfo => fileInfo.Name, _ => pattern);

		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the string content of the current file using the given <paramref name="encoding" />
	///     matches the <paramref name="pattern" />.
	/// </summary>
	public AndConstraint<FileAssertions> HasContent(
		Match pattern, Encoding encoding, string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert the content of a file if the FileInfo is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileInfo => pattern.Matches(
				fileInfo.FileSystem.File.ReadAllText(fileInfo.FullName, encoding)))
			.FailWith(
				"Expected {context} {0} to match '{1}'{reason}, but it did not.",
				fileInfo => fileInfo.Name, _ => pattern);

		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current file is not read-only.
	/// </summary>
	public AndConstraint<FileAssertions> IsNotReadOnly(
		string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert that the file is not read-only if it is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileInfo => !fileInfo.IsReadOnly)
			.FailWith(
				"Expected {context} {0} not to be read-only{reason}, but it was.",
				fileInfo => fileInfo.Name);

		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current file is read-only.
	/// </summary>
	public AndConstraint<FileAssertions> IsReadOnly(
		string because = "", params object[] becauseArgs)
	{
		Execute.Assertion
			.WithDefaultIdentifier(Identifier)
			.BecauseOf(because, becauseArgs)
			.ForCondition(Subject != null)
			.FailWith(
				"You can't assert that the file is read-only if it is null.")
			.Then
			.Given(() => Subject!)
			.ForCondition(fileInfo => fileInfo.IsReadOnly)
			.FailWith(
				"Expected {context} {0} to be read-only{reason}, but it was not.",
				fileInfo => fileInfo.Name);

		return new AndConstraint<FileAssertions>(this);
	}
}
