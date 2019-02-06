using System;
using TrafficComet.Abstracts.Logs;

namespace TrafficComet.Core
{
    public class DatesTrafficLog : IDatesTrafficLog
    {
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}