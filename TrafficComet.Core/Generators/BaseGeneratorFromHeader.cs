using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;

namespace TrafficComet.Core.Generators
{
	public abstract class BaseGeneratorFromHeader
	{
		protected IHttpContextAccessor HttpContextAccessor { get; }

		public BaseGeneratorFromHeader(IHttpContextAccessor httpContextAccessor)
		{
			HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}

		protected string GetHeaderValue(string headerName)
		{
			if (HttpContextAccessor.HttpContext.Request.Headers.TryGetValue(headerName, out StringValues headerValue))
			{
				return headerValue;
			}
			throw new NullReferenceException($"Couldn't found TrafficComet Header: {headerName}");
		}
	}
}