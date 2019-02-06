using Moq;
using System;
using System.IO;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Tests.Mock
{
    internal static class IStringBodyReaderMockFactory
	{
		internal static IStringBodyReader CreateMockObject(string returnValue = null)
		{
			if (string.IsNullOrEmpty(returnValue))
				returnValue = "test return value";

			var mockObject = new Mock<IStringBodyReader>();
			mockObject.Setup(x => x.ReadBody(It.IsAny<Stream>(), false, string.Empty)).Returns(returnValue).Verifiable();
			return mockObject.Object;
		}

		internal static IStringBodyReader CreateMockObjectWithException()
		{
			var mockObject = new Mock<IStringBodyReader>();
			mockObject.Setup(x => x.ReadBody(It.IsAny<Stream>(), false, string.Empty)).Returns((Stream s) => { throw new Exception(); });
			return mockObject.Object;
		}
	}
}