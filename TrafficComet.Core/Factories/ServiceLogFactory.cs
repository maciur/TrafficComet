using System;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstracts.Readers;

namespace TrafficComet.Core.Factories
{
    public class ServiceLogFactory : IServerLogFactory
    {
        protected IMachineNameReader MachineNameReader { get; }
        protected IIpAddressReader AddressIpReader { get; }

        public ServiceLogFactory(IMachineNameReader machineNameReader,
            IIpAddressReader addressIpReader)
        {
            MachineNameReader = machineNameReader
                ?? throw new ArgumentNullException(nameof(machineNameReader));

            AddressIpReader = addressIpReader
                ?? throw new ArgumentNullException(nameof(addressIpReader));
        }

        public IServerTrafficLog Create()
        {
            return new ServerTrafficLog
            {
                IpAddress = AddressIpReader.GetServerIpAddress(),
                MachineName = MachineNameReader.GetMachineName()
            };
        }
    }
}