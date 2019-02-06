using System.Collections.Generic;

namespace TrafficComet.Core.Configs
{
	public class RequestLogFactoryConfig : BaseLogFactoryConfig
	{
		public IEnumerable<string> IgnoreCookies { get; set; }
	}
}