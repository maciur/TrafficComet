using System.Threading.Tasks;
using TrafficComet.Abstracts.Logs;

namespace TrafficComet.Abstracts.Writers
{
	public interface ITrafficLogWriter
	{
		bool SaveLog(ITrafficLog trafficLog);
	}
}