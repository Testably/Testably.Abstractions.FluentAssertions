namespace Testably.Abstractions.FluentAssertions;

public abstract class StatisticAssertionsCount<TType, TAssertions>(TAssertions assertions)
	where TAssertions : StatisticAssertions<TType>
{
	protected abstract bool IsSubjectNull { get; }

	protected abstract string StatisticName { get; }

	protected abstract string StatisticType { get; }

	protected abstract string StatisticTypeVerb { get; }

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is 0 (zero).
	/// </summary>
	public AndConstraint<TAssertions> Never(
		string because = "", params object[] becauseArgs)
	{
		int count = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(count == 0)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to never be {StatisticTypeVerb}{{reason}}, but it was {CountToString(count)}.");

		return new AndConstraint<TAssertions>(assertions);
	}


	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is 0 (zero).
	/// </summary>
	public AndConstraint<TAssertions> Once(
		string because = "", params object[] becauseArgs)
	{
		int count = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(count == 1)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} once{{reason}}, but it was {CountToString(count)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

	protected abstract int GetCount();

	private static string CountToString(int count)
		=> count switch
		{
			0 => "never",
			1 => "once",
			2 => "twice",
			_ => $"{count} times"
		};
}
