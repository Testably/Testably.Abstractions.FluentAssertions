namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on <see cref="IFileSystemInfo" />.
/// </summary>
public abstract class
	FileSystemInfoAssertions<TFileSystemInfo, TAssertion>
	: ReferenceTypeAssertions<TFileSystemInfo, TAssertion>
	where TFileSystemInfo : IFileSystemInfo
	where TAssertion : ReferenceTypeAssertions<TFileSystemInfo, TAssertion>
{
	/// <summary>
	///     Initializes a new instance of <see cref="FileSystemInfoAssertions{TFileSystemInfo,TAssertion}" />
	/// </summary>
	protected FileSystemInfoAssertions(TFileSystemInfo subject) : base(subject)
	{
	}
}
