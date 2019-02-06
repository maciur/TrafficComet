using TrafficComet.Abstracts.Logs.Response;

namespace TrafficComet.Abstractss.Factories
{
    public interface IResponseLogFactory
	{
		IResponseLog Create(dynamic requestBody);
	}
}