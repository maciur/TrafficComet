using Moq;
using TrafficComet.Abstracts;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Tests.Mock
{
	internal static class IMachineNameReaderMockFactory
	{
		internal static IMachineNameReader CreateMockObject(string machineName = null)
		{
			if (string.IsNullOrEmpty(machineName))
				machineName = "test-machine-name";

			var mockObject = new Mock<IMachineNameReader>();
			mockObject.Setup(x => x.GetMachineName()).Returns(machineName).Verifiable();
			return mockObject.Object;
		}
	}
}