using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TrafficComet.Core")]
[assembly: InternalsVisibleTo("TrafficComet.Core.Tests")]
[assembly: InternalsVisibleTo("TrafficComet.JsonLogWriter")]
[assembly: InternalsVisibleTo("TrafficComet.ElasticLogWriter")]
[assembly: InternalsVisibleTo("TrafficComet.JsonLogWriter.Tests")]
namespace TrafficComet.Abstracts.Consts
{
    public static class RequestHeadersToIgnoreConsts
    {
        public const string UserClient = "User-Agent";
        public const string Cookies = "Cookie";
    }
}
