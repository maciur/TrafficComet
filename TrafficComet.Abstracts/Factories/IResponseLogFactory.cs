using TrafficComet.Abstracts.Logs.Response;

namespace TrafficComet.Abstracts.Factories
{
    public interface IResponseLogFactory
	{
		IResponseLog Create(dynamic requestBody);
	}
}