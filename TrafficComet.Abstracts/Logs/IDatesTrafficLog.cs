using System;
using System.Collections.Generic;
using System.Text;

namespace TrafficComet.Abstracts.Logs
{
    public interface IDatesTrafficLog
    {
        DateTime StartUtc { get; set; }
        DateTime EndUtc { get; set; }
        DateTime Start { get; set; }
        DateTime End{ get; set; }
    }
}
