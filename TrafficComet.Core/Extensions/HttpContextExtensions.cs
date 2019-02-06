using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Linq;

namespace TrafficComet.Core.Extensions
{
	public static class HttpContextExtensions
	{
		public static bool ResponseBodyIsCompressed(this HttpContext httpContext, out string compressionType,
			string contentEncodingHeader = "Content-Encoding")
		{
			return httpContext.Response.Headers.BodyIsCompressed(out compressionType, contentEncodingHeader);
		}

		public static bool RequestBodyIsCompressed(this HttpContext httpContext, out string compressionType,
			string contentEncodingHeader = "Content-Encoding")
		{
			return httpContext.Request.Headers.BodyIsCompressed(out compressionType, contentEncodingHeader);
		}

		public static bool BodyIsCompressed(this IHeaderDictionary headers, out string compressionType,
			string contentEncodingHeader = "Content-Encoding")
		{
			compressionType = string.Empty;
			if (headers.TryGetValueInvariantKey(contentEncodingHeader, out StringValues headerValue))
			{
				compressionType = headerValue.FirstOrDefault();
				return true;
			}
			return false;
		}
	}
}