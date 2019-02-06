using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TrafficComet.Core")]
[assembly: InternalsVisibleTo("TrafficComet.Core.Tests")]
[assembly: InternalsVisibleTo("TrafficComet.JsonLogWriter")]
[assembly: InternalsVisibleTo("TrafficComet.ElasticLogWriter")]
[assembly: InternalsVisibleTo("TrafficComet.Splunk.LogWriter")]
[assembly: InternalsVisibleTo("TrafficComet.JsonLogWriter.Tests")]
namespace TrafficComet.Core.Consts
{
	internal class TrafficCometConstValues
	{
		internal const string TraceIdHeader = "TraffiComet-Trafic-Id";
		internal const string ClientUniqueIdHeader = "TraffiComet-Client-Unique-Id";
		internal const string DefaultClientUniqueIdCookieName = "TrafficComet_UCI";
		internal const string RootConfigName = "TrafficComet";
	}
}