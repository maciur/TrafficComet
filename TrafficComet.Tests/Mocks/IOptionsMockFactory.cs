using Microsoft.Extensions.Options;
using Moq;

namespace TrafficComet.Core.Tests.Mock
{
	internal static class IOptionsMockFactory
	{
		internal static IOptions<TOptions> CreateMockObject<TOptions>(TOptions optionsValue) 
			where TOptions : class, new()
		{
			var mockObject = new Mock<IOptions<TOptions>>();
			mockObject.SetupProperty(x => x.Value, optionsValue);
			return mockObject.Object;
		}
	}
}