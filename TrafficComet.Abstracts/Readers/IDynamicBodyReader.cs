using System.IO;

namespace TrafficComet.Abstracts.Readers
{
	public interface IDynamicBodyReader
	{
		dynamic ReadBody(Stream stream, bool isCompressed, string compressionType);
	}
}