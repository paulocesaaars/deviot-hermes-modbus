using Deviot.Hermes.Common;
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

        event InformationChangedHandler ChangedDataEvent;        
    }

    public delegate void InformationChangedHandler(object sender, DeviceData data);
}
