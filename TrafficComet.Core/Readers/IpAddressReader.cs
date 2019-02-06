using Microsoft.AspNetCore.Http;
using System;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Readers
{
    public class IpAddressReader : IIpAddressReader
	{
		protected IHttpContextAccessor HttpContextAccessor { get; }
		protected const string CorrectLocalIpAddress = "127.0.0.1";
		protected const string WrongLocalIpAddress = "::1";

		public IpAddressReader(IHttpContextAccessor httpContextAccessor)
		{
			HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
		}

		public string GetClientIpAddress()
		{
			var clientIpAddress = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
			if (clientIpAddress.Equals(WrongLocalIpAddress))
			{
				clientIpAddress = CorrectLocalIpAddress;
			}
			clientIpAddress += $":{HttpContextAccessor.HttpContext.Connection.RemotePort}";
			return clientIpAddress;
		}

		public string GetServerIpAddress()
		{
			var serverIpAddress = HttpContextAccessor.HttpContext.Connection.LocalIpAddress.ToString();
			if (serverIpAddress.Equals(WrongLocalIpAddress))
			{
				serverIpAddress = CorrectLocalIpAddress;
			}
			serverIpAddress += $":{HttpContextAccessor.HttpContext.Connection.LocalPort}";
			return serverIpAddress;
		}
	}
}