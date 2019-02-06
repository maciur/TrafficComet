using System.IO;
using System.IO.Compression;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Readers
{
    public class JsonStreamReader : IStringBodyReader
    {
        public string ReadBody(Stream stream, bool isCompressed, string compressionType)
        {
            var jsonBody = string.Empty;
            if (stream.CanRead && stream.Length > 0)
            {
                return isCompressed ?
                    CompressedStream(stream, compressionType) : UncompressedStream(stream);
            }
            return jsonBody;
        }

        protected virtual string CompressedStream(Stream stream, string compressionType)
        {
            using (var decompressor = compressionType == "gzip" ?
                (Stream)new GZipStream(stream, CompressionMode.Decompress, true)
                : new DeflateStream(stream, CompressionMode.Decompress, true))
            {
                using (var streamReader = new StreamReader(decompressor))
                {
                    stream.Position = 0L;
                    var jsonBody = streamReader.ReadToEnd();
                    stream.Position = 0L;
                    return jsonBody;
                }
            }
        }

        protected virtual string UncompressedStream(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                stream.Position = 0L;
                var jsonBody = streamReader.ReadToEnd();
                stream.Position = 0L;
                return jsonBody;
            }
        }
    }
}