using Microsoft.Extensions.Options;
using Moq;

namespace TrafficComet.JsonLogWriter.Tests.Mocks
{
	public static class IOptionsMockFactory
	{
		public static IOptions<TOptions> CreateMockObject<TOptions>(TOptions options) where TOptions : class, new()
		{
			var mockObject = new Mock<IOptions<TOptions>>();
			mockObject.SetupGet(x => x.Value).Returns(options);
			return mockObject.Object;
		}
	}
}