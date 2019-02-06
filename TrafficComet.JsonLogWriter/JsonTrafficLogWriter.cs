using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstracts.Writers;

namespace TrafficComet.JsonLogWriter
{
    public class JsonTrafficLogWriter : ITrafficLogWriter
    {
        protected const string DefaultDateTimeFormat = "yyyy-MM-dd_HH-mm-ss-fffffff";
        protected IOptions<JsonTrafficLogWriterConfig> Config { get; }

        protected JsonSerializerSettings JsonSerializerSettings { get; }

        public JsonTrafficLogWriter(IOptions<JsonTrafficLogWriterConfig> config)
        {
            if (config == null || config.Value == null)
                throw new ArgumentNullException(nameof(config));

            Config = config;

            JsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public bool SaveLog(ITrafficLog trafficLog)
        {
            if (trafficLog == null)
                throw new ArgumentNullException(nameof(trafficLog));

            string trafficLogAsString = PrepareLog(trafficLog);

            if (string.IsNullOrEmpty(trafficLogAsString))
                throw new NullReferenceException(nameof(trafficLogAsString));

            var pathToUserLogFolder = Path.Combine(Config.Value.PathToLogFolder,
                DateTime.UtcNow.ToString("yyyy-MM-dd"), trafficLog.Client.Id);

            HandleLogFolder(pathToUserLogFolder);

            var trafficLogFileName = GetFileName(trafficLog);

            if (string.IsNullOrEmpty(trafficLogFileName))
                throw new NullReferenceException(nameof(trafficLogFileName));

            var pathToTrafficLogFile = Path.Combine(pathToUserLogFolder, trafficLogFileName);

            WriteLogToFile(pathToTrafficLogFile, PrepareLog(trafficLog), Encoding.UTF8);

            return true;
        }

        internal string GetDateTimeFormatForLogFile()
        {
            return !string.IsNullOrEmpty(Config.Value.DateTimeFormatFileLog)
                ? Config.Value.DateTimeFormatFileLog : DefaultDateTimeFormat;
        }

        protected internal virtual string GetFileName(ITrafficLog trafficLog)
        {
            if (trafficLog == null)
                throw new ArgumentNullException(nameof(trafficLog));

            return $"{trafficLog.Dates.StartUtc.ToString(GetDateTimeFormatForLogFile())}{trafficLog.Request.Path.Replace("/", "-")}.json";
        }

        protected internal virtual void HandleLogFolder(string pathToFolder)
        {
            if (string.IsNullOrEmpty(pathToFolder))
                throw new ArgumentNullException(pathToFolder);

            if (!Directory.Exists(pathToFolder))
                Directory.CreateDirectory(pathToFolder);
        }

        protected internal virtual string PrepareLog(ITrafficLog trafficLog)
        {
            if (trafficLog == null)
                throw new ArgumentNullException(nameof(trafficLog));

            if (Config.Value.IgnoreClientAddressIp)
            {
                trafficLog.Client.IpAddress = null;
            }

            return JsonConvert.SerializeObject(trafficLog, JsonSerializerSettings);
        }

        protected internal virtual void WriteLogToFile(string pathToLogFile, string logMessage, Encoding encoding)
        {
            if (string.IsNullOrEmpty(pathToLogFile))
                throw new ArgumentNullException(pathToLogFile);

            if (string.IsNullOrEmpty(logMessage))
                throw new ArgumentNullException(logMessage);

            File.WriteAllText(pathToLogFile, logMessage, encoding);
        }
    }
}