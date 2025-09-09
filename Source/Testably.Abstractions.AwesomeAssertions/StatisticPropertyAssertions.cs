using System;
using System.Collections.Generic;
using System.Linq;
using Testably.Abstractions.Testing.Statistics;

namespace Testably.Abstractions.AwesomeAssertions;

/// <summary>
///     Assertions on property statistics.
/// </summary>
public class StatisticPropertyAssertions<TType, TAssertions>
	: StatisticAssertionsCount<TType, TAssertions>
	where TAssertions : StatisticAssertions<TType>
{
	/// <inheritdoc cref="StatisticAssertionsCount{TType, TAssertions}.IsSubjectNull" />
	protected override bool IsSubjectNull { get; }

	/// <inheritdoc cref="StatisticAssertionsCount{TType, TAssertions}.StatisticName" />
	protected override string StatisticName { get; }

	/// <inheritdoc cref="StatisticAssertionsCount{TType, TAssertions}.StatisticType" />
	protected override string StatisticType => "property";

	/// <inheritdoc cref="StatisticAssertionsCount{TType, TAssertions}.StatisticTypeVerb" />
	protected override string StatisticTypeVerb => "accessed";

	private readonly IEnumerable<PropertyStatistic> _properties;

	internal StatisticPropertyAssertions(TAssertions assertions, string propertyName)
		: base(assertions)
	{
		IsSubjectNull = true;
		StatisticName = propertyName;
		_properties = Array.Empty<PropertyStatistic>();
	}

	internal StatisticPropertyAssertions(TAssertions assertions, string propertyName,
		IEnumerable<PropertyStatistic> properties)
		: base(assertions)
	{
		IsSubjectNull = false;
		StatisticName = propertyName;
		_properties = properties;
	}

	/// <inheritdoc cref="StatisticAssertionsCount{TType, TAssertions}.GetCount()" />
	protected override int GetCount()
		=> _properties.Count();
}
