using Newtonsoft.Json.Linq;
using System;
using System.IO;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Readers
{
	public class DynamicBodyReader : IDynamicBodyReader
	{
		protected IStringBodyReader StringBodyReader { get; }

		public DynamicBodyReader(IStringBodyReader stringBodyReader)
		{
			StringBodyReader = stringBodyReader
				?? throw new ArgumentNullException(nameof(stringBodyReader));
		}

		public dynamic ReadBody(Stream stream, bool isCompressed, string compressionType)
		{
			var stringBody = StringBodyReader.ReadBody(stream, isCompressed, compressionType);

			return !string.IsNullOrEmpty(stringBody) ? ParseStringToDynamicObject(stringBody) : null;
		}

		protected dynamic ParseStringToDynamicObject(string requestBody)
		{
			if (!string.IsNullOrEmpty(requestBody))
			{
				if (requestBody[0] == '[')
					return JArray.Parse(requestBody);

				return JObject.Parse(requestBody);
			}
			return null;
		}
	}
}