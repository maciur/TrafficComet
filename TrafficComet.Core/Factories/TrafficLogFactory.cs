using System;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstracts.Logs.Request;
using TrafficComet.Abstracts.Logs.Response;
using TrafficComet.Abstracts.Factories;

namespace TrafficComet.Core.Factories
{
    public class TrafficLogFactory : ITrafficLogFactory
    {
        protected IRequestLogFactory RequestLogFactory { get; }
        protected IResponseLogFactory ResponseLogFactory { get; }
        protected IDatesTrafficFactory DatesTrafficFactory { get; }
        protected IClientLogFactory ClientLogFactory { get; }
        protected IServerLogFactory ServerLogFactory { get; }
        protected ITrafficCometMiddlewaresAccessor TrafficCometMiddlewaresAccessor { get; }

        public TrafficLogFactory(IRequestLogFactory requestLogFactory,
            IResponseLogFactory responseLogFactory, IDatesTrafficFactory datesTrafficFactory,
            IClientLogFactory clientLogFactory, IServerLogFactory serverLogFactory,
            ITrafficCometMiddlewaresAccessor trafficCometAccessor)
        {
            RequestLogFactory = requestLogFactory
                ?? throw new ArgumentNullException(nameof(requestLogFactory));

            ResponseLogFactory = responseLogFactory
                ?? throw new ArgumentNullException(nameof(responseLogFactory));

            DatesTrafficFactory = datesTrafficFactory
                ?? throw new ArgumentNullException(nameof(datesTrafficFactory));

            ClientLogFactory = clientLogFactory
                ?? throw new ArgumentNullException(nameof(clientLogFactory));

            ServerLogFactory = serverLogFactory
                ?? throw new ArgumentNullException(nameof(serverLogFactory));

            TrafficCometMiddlewaresAccessor = trafficCometAccessor
                ?? throw new ArgumentNullException(nameof(trafficCometAccessor));
        }

        public ITrafficLog Create()
        {
            IRequestLog requestLog = null;

            if (!TrafficCometMiddlewaresAccessor.IgnoreRequest)
            {
                requestLog = RequestLogFactory.Create(TrafficCometMiddlewaresAccessor.RequestBody);

                if (requestLog == null)
                    throw new NullReferenceException(nameof(requestLog));
            }

            IResponseLog responseLog = null;

            if (!TrafficCometMiddlewaresAccessor.IgnoreResponse)
            {
                responseLog = ResponseLogFactory.Create(TrafficCometMiddlewaresAccessor.ResponseBody);

                if (responseLog == null)
                    throw new NullReferenceException(nameof(responseLog));
            }

            return new TrafficCometLog
            {
                TraceId = TrafficCometMiddlewaresAccessor.TraceId,
                ApplicationId = TrafficCometMiddlewaresAccessor.ApplicationId,
                Server = ServerLogFactory.Create(),
                Dates = DatesTrafficFactory.Create(),
                Client = ClientLogFactory.Create(),
                Request = requestLog,
                Response = responseLog,
                CustomParams = TrafficCometMiddlewaresAccessor.CustomParams,
            };
        }
    }
}