using System.Collections.Generic;
using TrafficComet.Abstracts.Logs.Client;
using TrafficComet.Abstracts.Logs.Request;
using TrafficComet.Abstracts.Logs.Response;

namespace TrafficComet.Abstracts.Logs
{
	public interface ITrafficLog
	{
		string ApplicationId { get; set; }
		IClientTrafficLog Client { get; set; }
		IDictionary<string, string> CustomParams { get; set; }
		IDatesTrafficLog Dates { get; set; }
		IRequestLog Request { get; set; }
		IResponseLog Response { get; set; }
		IServerTrafficLog Server { get; set; }
		string TraceId { get; set; }
	}
}