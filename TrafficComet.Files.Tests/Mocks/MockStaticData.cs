using System.Net;

namespace TrafficComet.JsonLogWriter.Tests.Mocks
{
	public static class MockStaticData
	{
		internal const string TraceId = "f08a862c-54e6-489f-853c-ef2aafa46dec";
		internal const string UserUniqueId = "899d3e3a-2691-4f45-9a50-bc81397d8d58";

		internal readonly static IPAddress IPAddress1 = IPAddress.Parse("192.168.135.120");
		internal readonly static IPAddress IPAddress2 = IPAddress.Parse("192.168.135.130");
		internal const int Port1 = 1111;
		internal const int Port2 = 2222;
	}
}