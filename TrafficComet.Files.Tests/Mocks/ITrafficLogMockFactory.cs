using System;
using System.Collections.Generic;
using TrafficComet.Abstracts.Logs.Client;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstracts.Logs.Request;
using TrafficComet.Abstracts.Logs.Response;

namespace TrafficComet.JsonLogWriter.Tests.Mocks
{
    public class TrafficLogMock : ITrafficLog
    {
        public string TraceId { get; set; }
        public string ApplicationId { get; set; }
        public IServerTrafficLog Server { get; set; }
        public IDatesTrafficLog Dates { get; set; }
        public IClientTrafficLog Client { get; set; }
        public IRequestLog Request { get; set; }
        public IResponseLog Response { get; set; }
        public IDictionary<string, string> CustomParams { get; set; }
    }

    public class RequestLogMock : IRequestLog
    {
        public IDictionary<string, string> Cookies { get; set; }
        public string FullUrl { get; set; }
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> QueryParams { get; set; }
        public dynamic Body { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, string> CustomParams { get; set; }
    }

    public class DatesTrafficLogMock : IDatesTrafficLog
    {
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class ClientTrafficLogMock : IClientTrafficLog
    {
        public string Id { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }

    public static class ITrafficLogMockFactory
    {
        public static ITrafficLog CreateMockObject(DateTime StartDate, DateTime EndDate)
        {
            var mockObject = new TrafficLogMock
            {
                Client = new ClientTrafficLogMock
                {
                    Id = MockStaticData.UserUniqueId,
                    IpAddress = $"{MockStaticData.IPAddress1}:{MockStaticData.Port1}"
                },
                Dates = new DatesTrafficLogMock
                {
                    Start = StartDate,
                    End = EndDate,
                    StartUtc = StartDate,
                    EndUtc = EndDate,
                },
                Request = new RequestLogMock
                {
                    Path = "/test-path"
                }
            };
            return mockObject;
        }
    }
}