using System.Collections.Generic;

namespace TrafficComet.Abstracts.Logs.Request
{
	public interface IRequestLog : IRequestBaseLog
	{
        IDictionary<string, string> Cookies { get; set; }
        string FullUrl { get; set; }
        string HttpMethod { get; set; }
        string Path { get; set; }
        IDictionary<string, string> QueryParams { get; set; }
	}
}