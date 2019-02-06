using System;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Readers
{
    public class MachineNameReader : IMachineNameReader
	{
		public string GetMachineName()
		{
			return Environment.MachineName;
		}
	}
}