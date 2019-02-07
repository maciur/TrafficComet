using Moq;
using TrafficComet.Abstracts.Logs.Response;
using TrafficComet.Abstracts.Factories;

namespace TrafficComet.Core.Tests.Mock
{
    internal static class IResponseLogFactoryMockFactory
	{
		internal static IResponseLogFactory CreateMOckObject(IResponseLog responseLog)
		{
			var mockObject = new Mock<IResponseLogFactory>();
			mockObject.Setup(x => x.Create(It.IsAny<string>())).Returns(responseLog);
			return mockObject.Object;
		}
	}
}