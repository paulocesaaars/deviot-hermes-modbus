using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;
using System;

namespace Deviot.Hermes.Modbus.Core.Contracts
{
    public interface IBrokerDriverService : IDisposable
    {
        public void Start(BrokerDevice modbusDevice);

        public void Stop();

        public void UpdateBroker(BrokerDevice brokerDevice);

        public BrokerStatusDevice GetStatusDevice();

        public void SendData(string topic, DeviceData data);
    }
}
