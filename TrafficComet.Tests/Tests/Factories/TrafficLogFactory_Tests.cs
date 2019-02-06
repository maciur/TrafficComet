using Moq;
using NUnit.Framework;
using System;
using TrafficComet.Abstracts;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Logs.Request;
using TrafficComet.Abstracts.Logs.Response;
using TrafficComet.Abstracts.Readers;
using TrafficComet.Abstractss.Factories;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Tests.Factories
{
	[TestFixture(Category = "LogFactory Tests")]
	public class TrafficLogFactory_Tests
	{


		[Test]
		public void ThrowException_TrafficLogParamsNull()
		{
			var trafficLogFactory = MockServicesHelper.CreateTrafficLog();

			var startDateUtc = DateTime.UtcNow;
			var endDateUtc = DateTime.UtcNow;

            Assert.Throws<ArgumentNullException>(() => trafficLogFactory.Create());
		}

		[Test]
		public void ThrowException_ClientIpAddressNull()
		{
			var mockIpAddressReader = new Mock<IIpAddressReader>();
			mockIpAddressReader.Setup(x => x.GetClientIpAddress()).Returns((string)null);

			var trafficLogFactory = MockServicesHelper.CreateTrafficLog(ipAddressReader: mockIpAddressReader.Object);

			var mockTrafficLogParams =
				MockServicesHelper.CreateTrafficLogParams(out DateTime startDateUtc, out DateTime endDateUtc);

			Assert.Throws<NullReferenceException>(() => trafficLogFactory.Create(mockTrafficLogParams));
		}

		[Test]
		public void ThrowException_ServerIpAddressNull()
		{
			var mockIpAddressReader = new Mock<IIpAddressReader>();
			mockIpAddressReader.Setup(x => x.GetClientIpAddress()).Returns($"{MockStaticData.IPAddress1}:{MockStaticData.Port1}");
			mockIpAddressReader.Setup(x => x.GetServerIpAddress()).Returns((string)null);

			var trafficLogFactory = MockServicesHelper.CreateTrafficLog(ipAddressReader: mockIpAddressReader.Object);

			var mockTrafficLogParams =
				MockServicesHelper.CreateTrafficLogParams(out DateTime startDateUtc, out DateTime endDateUtc);

			Assert.Throws<NullReferenceException>(() => trafficLogFactory.Create(mockTrafficLogParams));
		}

		[Test]
		public void ThrowException_RequestLogNull()
		{
			var mockRequestFactory = new Mock<IRequestLogFactory>();
			mockRequestFactory.Setup(x => x.Create(It.IsAny<string>())).Returns((IRequestLog)null);

			var trafficLogFactory = MockServicesHelper.CreateTrafficLog(requestLogFactory: mockRequestFactory.Object);

			var mockTrafficLogParams =
				MockServicesHelper.CreateTrafficLogParams(out DateTime startDateUtc, out DateTime endDateUtc);

			Assert.Throws<NullReferenceException>(() => trafficLogFactory.Create(mockTrafficLogParams));
		}

		[Test]
		public void ThrowException_ResponseLogNull()
		{
			var mockResponseFactory = new Mock<IResponseLogFactory>();
			mockResponseFactory.Setup(x => x.Create(It.IsAny<string>())).Returns((IResponseLog)null);

			var trafficLogFactory = MockServicesHelper.CreateTrafficLog(responseLogFactory: mockResponseFactory.Object);

			var mockTrafficLogParams =
				MockServicesHelper.CreateTrafficLogParams(out DateTime startDateUtc, out DateTime endDateUtc);

			Assert.Throws<NullReferenceException>(() => trafficLogFactory.Create(mockTrafficLogParams));
		}

		[Test]
		public void ThrowException_MachineNameNull()

		{
			var mockMachineReader = new Mock<IMachineNameReader>();
			mockMachineReader.Setup(x => x.GetMachineName()).Returns((string)null);

			var trafficLogFactory = MockServicesHelper.CreateTrafficLog(machineNameReader: mockMachineReader.Object);

			var mockTrafficLogParams =
				MockServicesHelper.CreateTrafficLogParams(out DateTime startDateUtc, out DateTime endDateUtc);

			Assert.Throws<NullReferenceException>(() => trafficLogFactory.Create(mockTrafficLogParams));
		}

		[Test]
		public void RequestLogShouldBeNull()
		{
			var trafficLogFactory = MockServicesHelper.CreateTrafficLog();

			var mockTrafficLogParams =
				MockServicesHelper.CreateTrafficLogParams(out DateTime startDateUtc, out DateTime endDateUtc);

			var trafficLog = trafficLogFactory.Create(mockTrafficLogParams, ignoreRequest: true);
			Assert.NotNull(trafficLog);
			Assert.Null(trafficLog.Request);
		}

		[Test]
		public void ResponseLogShouldBeNull()
		{
			var trafficLogFactory = MockServicesHelper.CreateTrafficLog();

			var mockTrafficLogParams =
				MockServicesHelper.CreateTrafficLogParams(out DateTime startDateUtc, out DateTime endDateUtc);

			var trafficLog = trafficLogFactory.Create(mockTrafficLogParams, ignoreResponse: true);
			Assert.NotNull(trafficLog);
			Assert.Null(trafficLog.Response);
		}
	}
}