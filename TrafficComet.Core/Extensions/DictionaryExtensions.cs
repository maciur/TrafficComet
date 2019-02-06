using System.Collections.Generic;

namespace TrafficComet.Core.Extensions
{
	public static class DictionaryExtensions
	{
		public static bool TryGetValueInvariantKey<TValue>(this IDictionary<string, TValue> dict, string key, out TValue value)
		{
			value = default(TValue);

			if (!dict.TryGetValue(key, out value))
			{
				return dict.TryGetValue(key.ToLowerInvariant(), out value);
			}

			return true;
		}
	}
}