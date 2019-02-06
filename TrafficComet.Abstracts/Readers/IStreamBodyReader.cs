using System.IO;

namespace TrafficComet.Abstracts.Readers
{
	public interface IStreamBodyReader<TValue>
	{
		TValue ReadBody(Stream stream, bool isCompressed, string compressionType);
	}
}