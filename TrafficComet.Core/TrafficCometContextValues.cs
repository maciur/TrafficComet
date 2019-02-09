using System;
using System.Collections.Generic;

namespace TrafficComet.Core
{
	internal class TrafficCometContextValues
	{
		internal string ApplicationId { get; set; }

		internal string ClientId { get; set; }

		internal IDictionary<string, string> CustomParams { get; set; }

		internal DateTime EndDateLocal { get; set; }

		internal DateTime EndDateUtc { get; set; }

		internal bool IgnoreRequest { get; set; }

		internal bool IgnoreResponse { get; set; }

		internal bool IgnoreWholeRequest { get; set; }

		internal dynamic RequestBody { get; set; }

		internal IDictionary<string, string> RequestCustomParams { get; set; }

		internal dynamic ResponseBody { get; set; }

		internal IDictionary<string, string> ResponseCustomParams { get; set; }

		internal DateTime StartDateLocal { get; set; }

		internal DateTime StartDateUtc { get; set; }

		internal string TraceId { get; set; }

		public TrafficCometContextValues()
		{
			CustomParams = new Dictionary<string, string>();
			RequestCustomParams = new Dictionary<string, string>();
			ResponseCustomParams = new Dictionary<string, string>();
		}
	}
}