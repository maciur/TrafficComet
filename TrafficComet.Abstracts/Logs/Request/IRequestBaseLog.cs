using System.Collections.Generic;

namespace TrafficComet.Abstracts.Logs.Request
{
	public interface IRequestBaseLog
	{
		dynamic Body { get; set; }
		IDictionary<string, string> Headers { get; set; }
		IDictionary<string, string> CustomParams { get; set; }
	}
}