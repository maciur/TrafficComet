namespace TrafficComet.JsonLogWriter
{
	public class JsonTrafficLogWriterConfig
	{
		public string PathToLogFolder { get; set; }
		public string DateTimeFormatFileLog { get; set; }
		public bool IgnoreClientAddressIp { get; set; }
	}
}