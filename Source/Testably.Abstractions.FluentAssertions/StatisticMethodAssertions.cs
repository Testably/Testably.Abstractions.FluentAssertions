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

	private readonly TAssertions _assertions;

	private readonly IEnumerable<MethodStatistic> _methods;

	public StatisticMethodAssertions(TAssertions assertions, string methodName)
		: base(assertions)
	{
		_assertions = assertions;
		IsSubjectNull = true;
		StatisticName = methodName;
		_methods = Array.Empty<MethodStatistic>();
	}

	public StatisticMethodAssertions(TAssertions assertions, string methodName,
		IEnumerable<MethodStatistic> methods)
		: base(assertions)
	{
		IsSubjectNull = false;
		StatisticName = methodName;
		_assertions = assertions;
		_methods = methods;
	}

	/// <summary>
	///     Filters for methods whose first parameter equals <paramref name="parameterValue" />.
	/// </summary>
	public StatisticMethodAssertions<TType, TAssertions> WithFirstParameter<TParameter>(
		TParameter parameterValue)
		=> WithParameterAt<TParameter>(0,
			p => p == null ? parameterValue == null : p.Equals(parameterValue));

	/// <summary>
	///     Filters for methods whose first parameter matches the <paramref name="predicate" />.
	/// </summary>
	public StatisticMethodAssertions<TType, TAssertions> WithFirstParameter<TParameter>(
		Func<TParameter, bool> predicate)
		=> WithParameterAt(0, predicate);

	/// <summary>
	///     Filters for methods whose parameter at the zero-based <paramref name="index" /> equals
	///     <paramref name="parameterValue" />.
	/// </summary>
	public StatisticMethodAssertions<TType, TAssertions> WithParameterAt<TParameter>(int index,
		TParameter parameterValue)
		=> WithParameterAt<TParameter>(index,
			p => p == null ? parameterValue == null : p.Equals(parameterValue));

	/// <summary>
	///     Filters for methods whose parameter at the zero-based <paramref name="index" />
	///     matches the <paramref name="predicate" />.
	/// </summary>
	public StatisticMethodAssertions<TType, TAssertions> WithParameterAt<TParameter>(int index,
		Func<TParameter, bool> predicate)
		=> new(_assertions, StatisticName,
			_methods.Where(p => p.Parameters[index].Is(predicate)));

	/// <summary>
	///     Filters for methods whose second parameter equals <paramref name="parameterValue" />.
	/// </summary>
	public StatisticMethodAssertions<TType, TAssertions> WithSecondParameter<TParameter>(
		TParameter parameterValue)
		=> WithParameterAt<TParameter>(1,
			p => p == null ? parameterValue == null : p.Equals(parameterValue));

	/// <summary>
	///     Filters for methods whose second parameter matches the <paramref name="predicate" />.
	/// </summary>
	public StatisticMethodAssertions<TType, TAssertions> WithSecondParameter<TParameter>(
		Func<TParameter, bool> predicate)
		=> WithParameterAt(1, predicate);

	/// <summary>
	///     Filters for methods whose third parameter equals <paramref name="parameterValue" />.
	/// </summary>
	public StatisticMethodAssertions<TType, TAssertions> WithThirdParameter<TParameter>(
		TParameter parameterValue)
		=> WithParameterAt<TParameter>(2,
			p => p == null ? parameterValue == null : p.Equals(parameterValue));

	/// <summary>
	///     Filters for methods whose third parameter matches the <paramref name="predicate" />.
	/// </summary>
	public StatisticMethodAssertions<TType, TAssertions> WithThirdParameter<TParameter>(
		Func<TParameter, bool> predicate)
		=> WithParameterAt(2, predicate);

	/// <inheritdoc />
	protected override int GetCount()
		=> _methods.Count();
}
