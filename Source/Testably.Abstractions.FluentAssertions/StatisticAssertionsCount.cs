namespace Testably.Abstractions.FluentAssertions;

/// <summary>
///     Assertions on statistics.
/// </summary>
public abstract class StatisticAssertionsCount<TType, TAssertions>(TAssertions assertions)
	where TAssertions : StatisticAssertions<TType>
{
	/// <summary>
	///     Flag indicating if the subject of the assertion is null.
	/// </summary>
	protected abstract bool IsSubjectNull { get; }

	/// <summary>
	///     The name of the statistic.
	/// </summary>
	protected abstract string StatisticName { get; }

	/// <summary>
	///     The type of the statistic.
	/// </summary>
	protected abstract string StatisticType { get; }

	/// <summary>
	///     The verb to interact with the statistic type.
	/// </summary>
	protected abstract string StatisticTypeVerb { get; }

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is at least <paramref name="count" /> times.
	/// </summary>
	public AndConstraint<TAssertions> AtLeast(int count,
		string because = "", params object[] becauseArgs)
	{
		int actualCount = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(actualCount >= count)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} at least {CountToString(count)}{{reason}}, but it was {CountToString(actualCount)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is at least 1.
	/// </summary>
	public AndConstraint<TAssertions> AtLeastOnce(
		string because = "", params object[] becauseArgs)
	{
		int count = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(count >= 1)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} at least once{{reason}}, but it was {CountToString(count)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is at least 2.
	/// </summary>
	public AndConstraint<TAssertions> AtLeastTwice(
		string because = "", params object[] becauseArgs)
	{
		int count = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(count >= 2)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} at least twice{{reason}}, but it was {CountToString(count)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is at most <paramref name="count" /> times.
	/// </summary>
	public AndConstraint<TAssertions> AtMost(int count,
		string because = "", params object[] becauseArgs)
	{
		int actualCount = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(actualCount <= count)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} at most {CountToString(count)}{{reason}}, but it was {CountToString(actualCount)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is at most 1.
	/// </summary>
	public AndConstraint<TAssertions> AtMostOnce(
		string because = "", params object[] becauseArgs)
	{
		int count = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(count <= 1)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} at most once{{reason}}, but it was {CountToString(count)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is at most 2.
	/// </summary>
	public AndConstraint<TAssertions> AtMostTwice(
		string because = "", params object[] becauseArgs)
	{
		int count = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(count <= 2)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} at most twice{{reason}}, but it was {CountToString(count)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is exactly <paramref name="count" /> times.
	/// </summary>
	public AndConstraint<TAssertions> Exactly(int count,
		string because = "", params object[] becauseArgs)
	{
		int actualCount = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(actualCount == count)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} {CountToString(count)}{{reason}}, but it was {CountToString(actualCount)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

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
	///     Asserts that the number of calls on the filtered methods/properties is 1.
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

	/// <summary>
	///     Asserts that the number of calls on the filtered methods/properties is 2.
	/// </summary>
	public AndConstraint<TAssertions> Twice(
		string because = "", params object[] becauseArgs)
	{
		int count = GetCount();
		Execute.Assertion
			.WithDefaultIdentifier("Statistic")
			.BecauseOf(because, becauseArgs)
			.ForCondition(!IsSubjectNull)
			.FailWith("You can't assert a statistic if it is null.")
			.Then
			.ForCondition(count == 2)
			.FailWith(
				$"Expected {StatisticType} `{StatisticName}` to be {StatisticTypeVerb} twice{{reason}}, but it was {CountToString(count)}.");

		return new AndConstraint<TAssertions>(assertions);
	}

	/// <summary>
	///     Get the count of matching statistic values.
	/// </summary>
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
