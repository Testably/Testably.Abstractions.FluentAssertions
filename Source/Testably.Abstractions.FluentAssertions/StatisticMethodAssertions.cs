using System;
using System.Collections.Generic;
using System.Linq;
using Testably.Abstractions.Testing.Statistics;

namespace Testably.Abstractions.FluentAssertions;

public class StatisticMethodAssertions<TType, TAssertions>
	: StatisticAssertionsCount<TType, TAssertions>
	where TAssertions : StatisticAssertions<TType>
{
	/// <inheritdoc />
	protected override bool IsSubjectNull { get; }

	/// <inheritdoc />
	protected override string StatisticName { get; }

	/// <inheritdoc />
	protected override string StatisticType => "method";

	/// <inheritdoc />
	protected override string StatisticTypeVerb => "called";

	private readonly IEnumerable<MethodStatistic> _methods;

	public StatisticMethodAssertions(TAssertions assertions, string methodName)
		: base(assertions)
	{
		IsSubjectNull = true;
		StatisticName = methodName;
		_methods = Array.Empty<MethodStatistic>();
	}

	public StatisticMethodAssertions(TAssertions assertions, string methodName, IEnumerable<MethodStatistic> methods)
		: base(assertions)
	{
		IsSubjectNull = false;
		StatisticName = methodName;
		_methods = methods;
	}

	/// <inheritdoc />
	protected override int GetCount()
		=> _methods.Count();
}
