using Moq;
using System;
using TrafficComet.Abstracts;

namespace TrafficComet.Core.Tests.Mock
{
	internal static class IClientUniqueIdGeneratorMockFactory
	{
		internal static IClientIdGenerator CreateMockObject(string clientUniqueId = null)
		{
			if (string.IsNullOrEmpty(clientUniqueId))
				clientUniqueId = Guid.NewGuid().ToString();

			var mockObject = new Mock<IClientIdGenerator>();
			mockObject.Setup(x => x.GenerateClientId()).Returns(clientUniqueId).Verifiable();
			return mockObject.Object;
		}
	}
}