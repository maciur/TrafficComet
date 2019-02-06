using Microsoft.Extensions.Options;
using System;
using System.Text;

namespace TrafficComet.JsonLogWriter.Tests.Mocks
{
	public class JsonTrafficLogWriterMock : JsonTrafficLogWriter
	{
		internal string PathToLogFile { get; set; }
		internal string PathToLogFolder { get; set; }
		internal string LogMessage { get; set; }
		internal string LogDateFormat => DefaultDateTimeFormat;

		public JsonTrafficLogWriterMock(IOptions<JsonTrafficLogWriterConfig> config) : base(config)
		{
		}

		protected override void HandleLogFolder(string pathToFolder)
		{
			if (string.IsNullOrEmpty(pathToFolder))
				throw new ArgumentNullException(nameof(pathToFolder));

			PathToLogFolder = pathToFolder;
		}

		protected override void WriteLogToFile(string pathToLogFile, string logMessage, Encoding encoding)
		{
			if (string.IsNullOrEmpty(pathToLogFile))
				throw new ArgumentNullException(pathToLogFile);

			if (string.IsNullOrEmpty(logMessage))
				throw new ArgumentNullException(logMessage);

			PathToLogFile = pathToLogFile;
			LogMessage = logMessage;
		}
	}
}