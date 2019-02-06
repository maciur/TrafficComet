using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Consts;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Logs.Client;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Factories
{
    public class ClientLogFactory : IClientLogFactory
    {
        protected HttpRequest Request => HttpContextAccessor.HttpContext.Request;
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected ITrafficCometMiddlewaresAccessor TrafficCometAccessor { get; }
        protected IIpAddressReader AddressIpReader { get; }

        protected const string EMPTY_HEADER_VALUE = "no-value";

        public ClientLogFactory(IHttpContextAccessor httpContextAccessor,
            ITrafficCometMiddlewaresAccessor trafficCometAccessor, IIpAddressReader addressIpReader)
        {
            HttpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            TrafficCometAccessor = trafficCometAccessor
                ?? throw new ArgumentNullException(nameof(trafficCometAccessor));

            AddressIpReader = addressIpReader
                ?? throw new ArgumentNullException(nameof(addressIpReader));
        }

        public IClientTrafficLog Create()
        {
            return new ClientTrafficLog
            {
                Id = TrafficCometAccessor.ClientUniqueId,
                IpAddress = AddressIpReader.GetClientIpAddress(),
                UserAgent = GetUserAgentHeader()
            };
        }

        protected string GetUserAgentHeader()
        {
            if (Request.Headers.TryGetValue(RequestHeadersToIgnoreConsts.UserClient, out StringValues headerValues))
            {
                return headerValues.FirstOrDefault() ?? EMPTY_HEADER_VALUE;
            }
            return EMPTY_HEADER_VALUE;
        }
    }
}