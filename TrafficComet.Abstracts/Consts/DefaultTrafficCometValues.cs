using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TrafficComet.Core")]
[assembly: InternalsVisibleTo("TrafficComet.Core.Tests")]
[assembly: InternalsVisibleTo("TrafficComet.JsonFile.LogWriter")]
[assembly: InternalsVisibleTo("TrafficComet.ElasticLogWriter")]
[assembly: InternalsVisibleTo("TrafficComet.Splunk.LogWriter")]
[assembly: InternalsVisibleTo("TrafficComet.JsonLogWriter.Tests")]
namespace TrafficComet.Core.Consts
{
	internal class DefaultTrafficCometValues
	{
		internal const string TRACE_ID_HEADER = "TraffiComet-Trafic-Id";
		internal const string CLIENT_ID_HEADER = "TraffiComet-Client-Id";
        internal const string CLIENT_ID_COOKIE_NAME = "TrafficComet_UCI";
	}
}