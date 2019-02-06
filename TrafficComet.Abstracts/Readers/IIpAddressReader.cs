namespace TrafficComet.Abstracts.Readers
{
    public interface IIpAddressReader
	{
		string GetClientIpAddress();
		string GetServerIpAddress();
	}
}