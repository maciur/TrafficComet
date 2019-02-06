using System;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Logs;

namespace TrafficComet.Core.Factories
{
    public class DatesTrafficFactory : IDatesTrafficFactory
    {
        protected ITrafficCometMiddlewaresAccessor TrafficCometMiddlewaresAccessor { get; }

        public DatesTrafficFactory(ITrafficCometMiddlewaresAccessor trafficCometMiddlewaresAccessor)
        {
            TrafficCometMiddlewaresAccessor = trafficCometMiddlewaresAccessor
                ?? throw new ArgumentNullException(nameof(trafficCometMiddlewaresAccessor));
        }

        public IDatesTrafficLog Create()
        {
            return new DatesTrafficLog
            {
                Start = TrafficCometMiddlewaresAccessor.StartDateLocal,
                End = TrafficCometMiddlewaresAccessor.EndDateLocal,
                StartUtc = TrafficCometMiddlewaresAccessor.StartDateUtc,
                EndUtc = TrafficCometMiddlewaresAccessor.EndDateUtc,
            };
        }
    }
}