using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstracts.Writers;

namespace TrafficComet.WebApiTest.Mocks
{
	public class MockTrafficLogWriter : ITrafficLogWriter
	{
		public bool SaveLog(ITrafficLog trafficLog)
		{
			return true;
		}
	}
}
