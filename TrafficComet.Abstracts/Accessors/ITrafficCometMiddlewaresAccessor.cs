using System;
using System.Collections.Generic;

namespace TrafficComet.Abstracts.Accessors
{
	public interface ITrafficCometMiddlewaresAccessor
	{
		dynamic RequestBody { get; }
		dynamic ResponseBody { get; }
		DateTime StartDateUtc { get; }
		DateTime EndDateUtc { get; }
        DateTime StartDateLocal { get;}
        DateTime EndDateLocal { get; }
        string TraceId { get; }
		string ClientId { get; }
		string ApplicationId { get; }
		bool IgnoreWholeRequest { get; }
		bool IgnoreRequest { get; }
		bool IgnoreResponse { get; }
		IDictionary<string, string> CustomParams { get; set; }
		IDictionary<string, string> RequestCustomParams { get; set; }
		IDictionary<string, string> ResponseCustomParams { get; set; }
	}
}