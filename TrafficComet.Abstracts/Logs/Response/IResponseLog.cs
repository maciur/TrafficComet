using TrafficComet.Abstracts.Logs.Request;

namespace TrafficComet.Abstracts.Logs.Response
{
	public interface IResponseLog : IRequestBaseLog
	{
		int Status { get; set; }
	}
}