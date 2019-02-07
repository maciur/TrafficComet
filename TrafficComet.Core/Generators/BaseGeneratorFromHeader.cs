using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;

namespace TrafficComet.Core.Generators
{
	public abstract class BaseGeneratorFromHeader
	{
		protected IHttpContextAccessor HttpContextAccessor { get; }

		public BaseGeneratorFromHeader(IHttpContextAccessor httpContextAccessor)
		{
			HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}

		protected bool TryGetHeaderValue(string headerName, out string headerValue)
		{
			headerValue = string.Empty;
			if (HttpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerName, out StringValues headerValues))
			{
				headerValue = headerValues.FirstOrDefault();
			}
			return !string.IsNullOrEmpty(headerValue);
		}
	}
}