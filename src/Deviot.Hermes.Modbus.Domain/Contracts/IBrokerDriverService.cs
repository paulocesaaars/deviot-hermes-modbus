using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Entities;
using System;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IBrokerDriverService : IDisposable
    {
        public void Start(MosquittoBroker brokerDevice, ModbusDevice modbusDevice);

        public void Stop();

        public void UpdateDevice(MosquittoBroker brokerDevice, ModbusDevice modbusDevice);

        public MosquittoBrokerStatus GetBrokerStatus();

        public void SendData(string topic, DeviceData data);

        public void SendData(string topic, ModbusDeviceStatus modbusDevice);

        event ReceivedWriteDataHandler ReceivedWriteDataEvent;
    }

    public delegate void ReceivedWriteDataHandler(object sender, DeviceData data);
}
