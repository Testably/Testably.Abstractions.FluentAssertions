using System.IO;
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

	internal FileInfoAssertions(IFileInfo? instance)
		: base(instance)
	{
	}

	/// <summary>
	///     Asserts that the current file is read-only.
	/// </summary>
	public AndConstraint<FileInfoAssertions> BeReadOnly(
		string because = "", params object[] becauseArgs)
	{
		new FileAssertions(Subject).IsReadOnly(because, becauseArgs);
		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current file has the given <paramref name="attribute" />.
	/// </summary>
	public AndConstraint<FileInfoAssertions> HaveAttribute(
		FileAttributes attribute, string because = "", params object[] becauseArgs)
	{
		new FileAssertions(Subject).HasAttribute(attribute, because, becauseArgs);
		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the binary content of the current file is equivalent to the given <paramref name="bytes" />.
	/// </summary>
	public AndConstraint<FileInfoAssertions> HaveContent(
		byte[] bytes, string because = "", params object[] becauseArgs)
	{
		new FileAssertions(Subject).HasContent(bytes, because, becauseArgs);
		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the string content of the current file matches the <paramref name="pattern" />.
	/// </summary>
	public AndConstraint<FileInfoAssertions> HaveContent(
		Match pattern, string because = "", params object[] becauseArgs)
	{
		new FileAssertions(Subject).HasContent(pattern, because, becauseArgs);
		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the string content of the current file using the given <paramref name="encoding" />
	///     matches the <paramref name="pattern" />.
	/// </summary>
	public AndConstraint<FileInfoAssertions> HaveContent(
		Match pattern, Encoding encoding, string because = "", params object[] becauseArgs)
	{
		new FileAssertions(Subject).HasContent(pattern, encoding, because, becauseArgs);
		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current file is not read-only.
	/// </summary>
	public AndConstraint<FileInfoAssertions> NotBeReadOnly(
		string because = "", params object[] becauseArgs)
	{
		new FileAssertions(Subject).IsNotReadOnly(because, becauseArgs);
		return new AndConstraint<FileInfoAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current file does not have the given <paramref name="attribute" />.
	/// </summary>
	public AndConstraint<FileInfoAssertions> NotHaveAttribute(
		FileAttributes attribute, string because = "", params object[] becauseArgs)
	{
		new FileAssertions(Subject).DoesNotHaveAttribute(attribute, because, becauseArgs);
		return new AndConstraint<FileInfoAssertions>(this);
	}
}
