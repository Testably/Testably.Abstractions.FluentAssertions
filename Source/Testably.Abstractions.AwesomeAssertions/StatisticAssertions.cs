using System.Linq;
using Testably.Abstractions.Testing.Statistics;

namespace Testably.Abstractions.AwesomeAssertions;

/// <summary>
///     Assertions on <see cref="IFileInfo" />.
/// </summary>
public class StatisticAssertions<TType> :
	ReferenceTypeAssertions<IStatistics?, StatisticAssertions<TType>>
{
	/// <inheritdoc cref="ReferenceTypeAssertions{TSubject,TAssertions}.Identifier" />
	protected override string Identifier => "statistics";

	internal StatisticAssertions(IStatistics? instance, AssertionChain currentAssertionChain)
		: base(instance, currentAssertionChain)
	{
	}

	/// <summary>
	///     Returns a <see cref="StatisticPropertyAssertions{TType,TAssertions}" /> object that can be used to assert that the
	///     property named <paramref name="propertyName" /> was accessed a certain number of times.
	/// </summary>
	public StatisticPropertyAssertions<TType, StatisticAssertions<TType>> HaveAccessed(
		string propertyName)
	{
		if (Subject == null)
		{
			return new StatisticPropertyAssertions<TType, StatisticAssertions<TType>>(this,
				propertyName, CurrentAssertionChain);
		}

		return new StatisticPropertyAssertions<TType, StatisticAssertions<TType>>(this, propertyName,
			Subject.Properties.Where(p => p.Name == propertyName), CurrentAssertionChain);
	}

	/// <summary>
	///     Returns a <see cref="StatisticMethodAssertions{TType,TAssertions}" /> object that can be used to assert that the
	///     method named <paramref name="methodName" /> was called a certain number of times.
	/// </summary>
	public StatisticMethodAssertions<TType, StatisticAssertions<TType>> HaveCalled(
		string methodName)
	{
		if (Subject == null)
		{
			return new StatisticMethodAssertions<TType, StatisticAssertions<TType>>(this, methodName, CurrentAssertionChain);
		}

		return new StatisticMethodAssertions<TType, StatisticAssertions<TType>>(this, methodName,
			Subject.Methods.Where(m => m.Name == methodName), CurrentAssertionChain);
	}
}
