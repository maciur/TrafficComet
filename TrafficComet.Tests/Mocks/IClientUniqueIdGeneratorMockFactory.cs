using Moq;
using System;
using TrafficComet.Abstracts;

namespace TrafficComet.Core.Tests.Mock
{
	internal static class IClientUniqueIdGeneratorMockFactory
	{
		internal static IClientUniqueIdGenerator CreateMockObject(string clientUniqueId = null)
		{
			if (string.IsNullOrEmpty(clientUniqueId))
				clientUniqueId = Guid.NewGuid().ToString();

			var mockObject = new Mock<IClientUniqueIdGenerator>();
			mockObject.Setup(x => x.GenerateClientUniqueId()).Returns(clientUniqueId).Verifiable();
			return mockObject.Object;
		}
	}
}