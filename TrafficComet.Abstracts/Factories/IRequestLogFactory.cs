using TrafficComet.Abstracts.Logs.Request;

namespace TrafficComet.Abstracts.Factories
{
    public interface IRequestLogFactory
	{
		IRequestLog Create(dynamic requestBody);
	}
}