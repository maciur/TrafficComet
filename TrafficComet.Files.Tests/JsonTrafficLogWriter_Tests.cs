using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using TrafficComet.Abstracts.Logs;
using TrafficComet.JsonLogWriter.Tests.Mocks;

namespace TrafficComet.JsonLogWriter.Tests
{
    [TestFixture(Category = "JsonTrafficLogWriter Tests")]
	public class JsonTrafficLogWriter_Tests
	{
		[Test]
		public void SuccessfullyLogSaved()
		{
			var config = new JsonTrafficLogWriterConfig
			{
				PathToLogFolder = @"C:\\logs"
			};

			var startDate = DateTime.Now;
			var testLogToWrite = ITrafficLogMockFactory.CreateMockObject(startDate, DateTime.Now);
			var mockWriter = new JsonTrafficLogWriterMock(IOptionsMockFactory.CreateMockObject(config));

			var fileName = $"{testLogToWrite.Dates.StartUtc.ToString(mockWriter.LogDateFormat)}" +
				$"{testLogToWrite.Request.Path.Replace("/", "-")}.json";

            var folderLogName = Path.Combine(config.PathToLogFolder,
                startDate.ToString("yyyy-MM-dd"), testLogToWrite.Client.Id);

			mockWriter.SaveLog(testLogToWrite);
			Assert.NotNull(mockWriter.LogMessage);
			Assert.AreEqual(mockWriter.LogMessage, TransformLogToString(testLogToWrite));
			Assert.AreEqual(Path.GetFileName(mockWriter.PathToLogFile), fileName);
			Assert.AreEqual(folderLogName, mockWriter.PathToLogFolder);
		}

		[Test]
		public void ThrowException_NullTrafficLog()
		{
			var config = new JsonTrafficLogWriterConfig
			{
				PathToLogFolder = @"C:\\logs"
			};
			var mockWriter = new JsonTrafficLogWriterMock(IOptionsMockFactory.CreateMockObject(config));
			Assert.Throws<ArgumentNullException>(() => { mockWriter.SaveLog(null); });
		}

		private string TransformLogToString(ITrafficLog log)
		{
			return JsonConvert.SerializeObject(log, new JsonSerializerSettings
			{
				NullValueHandling = NullValueHandling.Ignore
			});
		}
	}
}