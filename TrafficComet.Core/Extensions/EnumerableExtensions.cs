using System.Collections.Generic;
using System.Linq;

namespace TrafficComet.Core.Extensions
{
	internal static class EnumerableExtensions
	{
		internal static bool SafeContains<TType>(this IEnumerable<TType> items, TType item)
		{
			if (items != null)
			{
				return items.Contains(item);
			}
			return false;
		}

		internal static bool SafeAny<TType>(this IEnumerable<TType> items)
		{
			return items != null && items.Any();
		}
	}
}