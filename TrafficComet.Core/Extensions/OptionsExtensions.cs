using Microsoft.Extensions.Options;

namespace TrafficComet.Core.Extensions
{
	public static class OptionsExtensions
	{
		/// <summary>
		/// Use this method to check if instance of IOptions was created and value is not null 
		/// </summary>
		/// <typeparam name="TOptions"></typeparam>
		/// <param name="options"></param>
		/// <returns></returns>
		public static bool IsValid<TOptions>(this IOptions<TOptions> options) where TOptions : class, new()
		{
			return options != null && options.Value != null;
		}
	}
}