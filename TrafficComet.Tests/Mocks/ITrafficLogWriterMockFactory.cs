using Moq;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstracts.Writers;

namespace TrafficComet.Core.Tests.Mocks
{
    internal static class ITrafficLogWriterMockFactory
	{
		internal static ITrafficLogWriter CreateMockObject()
		{
			var mockObject = new Mock<ITrafficLogWriter>();
			mockObject.Setup(x => x.SaveLog(It.IsAny<ITrafficLog>()))
				.Returns(true).Verifiable();
			return mockObject.Object;
		}
	}
}