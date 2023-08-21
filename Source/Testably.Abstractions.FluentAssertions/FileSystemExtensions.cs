namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertion extensions on <see cref="IFileSystem" />.
/// </summary>
public static class FileSystemExtensions
{
	/// <summary>
	///     Returns a <see cref="DirectoryAssertions" /> object that can be used to
	///     assert the current <see cref="IDirectoryInfo" />.
	/// </summary>
	public static DirectoryInfoAssertions Should(this IDirectoryInfo? instance)
		=> new(instance);

	/// <summary>
	///     Returns a <see cref="FileAssertions" /> object that can be used to
	///     assert the current <see cref="IFileInfo" />.
	/// </summary>
	public static FileInfoAssertions Should(this IFileInfo? instance)
		=> new(instance);

	/// <summary>
	///     Returns a <see cref="FileSystemAssertions" /> object that can be used to
	///     assert the current <see cref="IFileSystem" />.
	/// </summary>
	public static FileSystemAssertions Should(this IFileSystem instance)
		=> new(instance);
}
