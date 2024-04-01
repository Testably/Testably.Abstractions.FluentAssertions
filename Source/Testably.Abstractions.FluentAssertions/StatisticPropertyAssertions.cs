using System;
using System.Collections.Generic;
using System.Linq;
using Testably.Abstractions.Testing.Statistics;

namespace Testably.Abstractions.FluentAssertions;

public class StatisticPropertyAssertions<TType, TAssertions>
	: StatisticAssertionsCount<TType, TAssertions>
	where TAssertions : StatisticAssertions<TType>
{
	/// <inheritdoc />
	protected override bool IsSubjectNull { get; }

	/// <inheritdoc />
	protected override string StatisticName { get; }

	/// <inheritdoc />
	protected override string StatisticType => "property";

	/// <inheritdoc />
	protected override string StatisticTypeVerb => "accessed";

	private readonly IEnumerable<PropertyStatistic> _properties;

	public StatisticPropertyAssertions(TAssertions assertions, string propertyName)
		: base(assertions)
	{
		IsSubjectNull = true;
		StatisticName = propertyName;
		_properties = Array.Empty<PropertyStatistic>();
	}

	public StatisticPropertyAssertions(TAssertions assertions, string propertyName,
		IEnumerable<PropertyStatistic> properties)
		: base(assertions)
	{
		IsSubjectNull = false;
		StatisticName = propertyName;
		_properties = properties;
	}

	/// <inheritdoc />
	protected override int GetCount()
		=> _properties.Count();
}
