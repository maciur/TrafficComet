namespace TrafficComet.Abstracts
{
	public interface IClientIdGenerator
	{
        bool TryGenerateClientId(out string clientId);
	}
}