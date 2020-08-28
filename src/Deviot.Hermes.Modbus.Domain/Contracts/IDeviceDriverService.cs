using Deviot.Hermes.Modbus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IDeviceDriverService : IDisposable
    {
        public void Start(ModbusDevice modbusDevice);

        public void Stop();

        public void UpdateDevice(ModbusDevice modbusDevice);

        public ModbusDeviceStatus GetDeviceStatus();

        public IEnumerable<DeviceData> GetData();

        public IEnumerable<DeviceData> GetData(IEnumerable<string> idInformations);

        public void SendData(string idInformation, string data);

        public void SendData(DeviceTransferData data);

        event ReceiveTransferDataFromBrokerHandler ReceiveTransferDataFromBrokerEvent;        
    }

    public delegate void ReceiveTransferDataFromBrokerHandler(object sender, BrokerTrasferData data);
}
