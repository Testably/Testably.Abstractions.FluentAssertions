using System;
using System.Text.RegularExpressions;

namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Match a <see langword="string" /> against a pattern.
/// </summary>
public abstract class Match
{
	/// <summary>
	///     Matches the <paramref name="value" /> against the given match pattern.
	/// </summary>
	/// <param name="value">The value to match against the given pattern.</param>
	/// <returns>
	///     <see langword="true" />, if the <paramref name="value" /> matches the pattern, otherwise
	///     <see langword="false" />.
	/// </returns>
	public abstract bool Matches(string? value);

	/// <summary>
	///     Implicitly converts the <see langword="string" /> to a <see cref="Wildcard" /> pattern.<br />
	///     Supports * to match zero or more characters and ? to match exactly one character.
	/// </summary>
	public static implicit operator Match(string? pattern) => Wildcard(pattern ?? "");

	/// <summary>
	///     A wildcard match.<br />
	///     Supports * to match zero or more characters and ? to match exactly one character.
	/// </summary>
	/// <param name="pattern">The wildcard pattern to match against.</param>
	/// <param name="ignoreCase">
	///     (optional) Flag indicating if the match should be performed case sensitive or not.
	///     <para />
	///     Defaults to <see langword="false" />
	/// </param>
	public static Match Wildcard(string pattern, bool ignoreCase = false)
		=> new WildcardMatch(pattern, ignoreCase);

	private sealed class WildcardMatch : Match
	{
		private readonly bool _ignoreCase;
		private readonly string _originalPattern;
		private readonly string _pattern;

		internal WildcardMatch(string pattern, bool ignoreCase)
		{
			_originalPattern = pattern;
			_ignoreCase = ignoreCase;
			_pattern = WildcardToRegularExpression(pattern);
		}

		/// <inheritdoc cref="Match.Matches(string)" />
		public override bool Matches(string? value)
		{
			if (value == null)
			{
				return false;
			}

			RegexOptions options = _ignoreCase
				? RegexOptions.IgnoreCase
				: RegexOptions.None;
			return Regex.IsMatch(value, _pattern, options, TimeSpan.FromMilliseconds(1000));
		}

		/// <inheritdoc cref="object.ToString()" />
		public override string ToString()
			=> _originalPattern;

		/// <remarks>
		///     <see href="https://stackoverflow.com/a/30300521" />
		/// </remarks>
		private static string WildcardToRegularExpression(string value)
		{
			string regex = Regex.Escape(value)
				.Replace("\\?", ".")
				.Replace("\\*", ".*");
			return $"^{regex}$";
		}
	}
}
