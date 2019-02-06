using Moq;
using TrafficComet.Abstracts;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Tests.Mock
{
	internal class IIpAddressReaderMockFactory
	{
		private const string DefaultMockIpAddress = "127.0.0.1:1111";

		internal static IIpAddressReader CreateMockObject(string clientIp = null, string serverIp = null)
		{
			if (string.IsNullOrEmpty(clientIp))
				clientIp = DefaultMockIpAddress;

			if (string.IsNullOrEmpty(serverIp))
				serverIp = DefaultMockIpAddress;

			var mockObject = new Mock<IIpAddressReader>();
			mockObject.Setup(x => x.GetClientIpAddress()).Returns(clientIp).Verifiable();
			mockObject.Setup(x => x.GetServerIpAddress()).Returns(serverIp).Verifiable();
			return mockObject.Object;
		}
	}
}