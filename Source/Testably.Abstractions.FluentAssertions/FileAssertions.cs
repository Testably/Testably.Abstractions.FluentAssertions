using System.Text;

namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IFileInfo" />.
/// </summary>
public class FileAssertions :
	ReferenceTypeAssertions<IFileInfo, FileAssertions>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "file";

	internal FileAssertions(IFileInfo instance)
		: base(instance)
	{
	}

	/// <summary>
	///     Asserts that the current file is not read-only.
	/// </summary>
	public AndConstraint<FileAssertions> IsNotReadOnly(
		string because = "", params object[] becauseArgs)
	{
		Subject.Should().NotBeReadOnly(because, becauseArgs);
		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the current file is read-only.
	/// </summary>
	public AndConstraint<FileAssertions> IsReadOnly(
		string because = "", params object[] becauseArgs)
	{
		Subject.Should().BeReadOnly(because, becauseArgs);
		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the string content of the current file matches the <paramref name="pattern" />.
	/// </summary>
	public AndConstraint<FileAssertions> HasContentMatching(
		Match pattern, string because = "", params object[] becauseArgs)
	{
		Subject.Should().HaveContentMatching(pattern, because, becauseArgs);
		return new AndConstraint<FileAssertions>(this);
	}

	/// <summary>
	///     Asserts that the string content of the current file using the given <paramref name="encoding" />
	///     matches the <paramref name="pattern" />.
	/// </summary>
	public AndConstraint<FileAssertions> HasContentMatching(
		Match pattern, Encoding encoding, string because = "", params object[] becauseArgs)
	{
		Subject.Should().HaveContentMatching(pattern, encoding, because, becauseArgs);
		return new AndConstraint<FileAssertions>(this);
	}
}
