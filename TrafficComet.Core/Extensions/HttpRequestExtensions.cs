using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrafficComet.Abstracts.Consts;

namespace TrafficComet.Core.Extensions
{
    internal static class HttpRequestExtensions
    {
        internal static string GetFullUrl(this HttpRequest request)
        {
            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.ToString(),
                Path = $"{request.PathBase.ToString()}{request.Path.ToString()}",
                Query = request.QueryString.ToString()
            };
            return uriBuilder.ToString();
        }

        internal static IDictionary<string, string> GetHeaders(this HttpRequest request, IEnumerable<string> ignoreHeaders = null)
        {
            var headers = new ConcurrentDictionary<string, string>();

            Parallel.ForEach(request.Headers, header =>
            {
                if (!ignoreHeaders.SafeContains(header.Key) 
				&& header.Key != RequestHeadersToIgnoreConsts.UserClient
				&& header.Key != RequestHeadersToIgnoreConsts.Cookies)
                {
					headers.TryAdd(header.Key, header.Value);
                }
            });

            return headers;
        }

        internal static IDictionary<string, string> GetCookies(this HttpRequest request, IEnumerable<string> ignoreCookies = null)
        {
            var cookies = new ConcurrentDictionary<string, string>();

            Parallel.ForEach(request.Cookies, cookie =>
            {
                if (!ignoreCookies.SafeContains(cookie.Key))
                {
                    cookies.TryAdd(cookie.Key, cookie.Value);
                }
            });

            return cookies;
        }

        internal static IDictionary<string, string> GetQueryParams(this HttpRequest request)
        {
            var queryParams = new ConcurrentDictionary<string, string>();

            Parallel.ForEach(request.Query, queryParam =>
            {
                queryParams.TryAdd(queryParam.Key, queryParam.Value);
            });

            return queryParams;
        }
    }
}