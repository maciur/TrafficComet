namespace TrafficComet.Abstracts
{
	public interface ITraceIdGenerator
	{
        bool TryGenerateTraceId(out string traceId);
	}
}