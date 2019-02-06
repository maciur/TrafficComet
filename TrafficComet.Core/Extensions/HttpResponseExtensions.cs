using Microsoft.AspNetCore.Http;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrafficComet.Core.Extensions
{
    internal static class HttpResponseExtensions
    {
        internal static IDictionary<string, string> GetHeaders(this HttpResponse response, IEnumerable<string> ignoreHeaders = null)
        {
            var headers = new ConcurrentDictionary<string, string>();

            Parallel.ForEach(response.Headers, header =>
            {
                if (!ignoreHeaders.SafeContains(header.Key))
                {
                    headers.TryAdd(header.Key, header.Value);
                }
            });

            return headers;
        }
    }
}