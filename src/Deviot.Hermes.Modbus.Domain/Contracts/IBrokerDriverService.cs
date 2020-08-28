using Deviot.Hermes.Modbus.Domain.Entities;
using System;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IBrokerDriverService : IDisposable
    {
        public void Start(MqttBroker mqttBroker);

        public void Stop();

        public void UpdateDevice(MqttBroker mqttBroker);

        public MqttBrokerStatus GetBrokerStatus();

        public void SendData(BrokerTrasferData data);

        event ReceiveTransferDataFromDeviceHandler ReceiveTransferDataFromDeviceEvent;
    }

    public delegate void ReceiveTransferDataFromDeviceHandler(object sender, DeviceTransferData data);
}
