using System.Collections.Generic;

namespace TrafficComet.Core.Configs
{
	public class BaseMiddlewareConfig
	{
		public bool StopLogging { get; set; }
		public bool StartLoggingFileRequest { get; set; }
		public IEnumerable<string> IgnoreUrls { get; set; }
	} 
}