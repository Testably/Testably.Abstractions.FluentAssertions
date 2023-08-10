using FluentAssertions;
using Xunit;

namespace Testably.Abstractions.FluentAssertions.Tests;

public sealed class MatchTests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void ImplicitCast_FromNull_ShouldUseEmptyString(string? pattern)
	{
		Match match = pattern;

		match.Matches("").Should().BeTrue();
		match.Matches(" ").Should().BeFalse();
		match.ToString().Should().Be("");
	}

	[Fact]
	public void Wildcard_MatchesNull_ShouldReturnFalse()
	{
		Match match = Match.Wildcard("*");

		bool matches = match.Matches(null);

		matches.Should().BeFalse();
	}

	[Theory]
	[InlineData("*", "Foo.Bar", true)]
	[InlineData("Foo", "Foo", true)]
	[InlineData("Foo", "xFoo", false)]
	[InlineData("Foo", "xFoo.Bar", false)]
	[InlineData("Foo?Bar", "Foo.Bar", true)]
	[InlineData("Foo?r", "Foo.Bar", false)]
	[InlineData("Foo*", "Foo.Bar", true)]
	[InlineData("Foo*r", "Foo.Bar", true)]
	public void Wildcard_ShouldReturnValidRegexMatchingExpectedResult(
		string wildcard, string testInput, bool expectedResult)
	{
		Match match = Match.Wildcard(wildcard);

		bool matches = match.Matches(testInput);

		matches.Should().Be(expectedResult,
			$"wildcard '{wildcard.Replace("$", "$$")}' should{(expectedResult ? "" : " not")} match '{testInput}'");
	}

	[Theory]
	[InlineData(true)]
	[InlineData(false)]
	public void Wildcard_WithIgnoreCase_ShouldConsiderFlag(
		bool ignoreCase)
	{
		bool expectedResult = ignoreCase;
		Match match = Match.Wildcard("foo", ignoreCase);

		bool matches = match.Matches("FOO");

		matches.Should().Be(expectedResult,
			$"wildcard 'foo' should{(expectedResult ? "" : " not")} match 'FOO' while ignoreCase:{ignoreCase}");
	}
}
