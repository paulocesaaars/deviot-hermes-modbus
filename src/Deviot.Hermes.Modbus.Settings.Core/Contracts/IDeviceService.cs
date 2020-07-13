﻿using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Core.Contracts
{
    public interface IDeviceService
    {
        public Task Start();

        public void Stop();

        public void UpdateDevice(ModbusDevice modbusDevice);

        public ModbusStatusDevice GetStatusDevice();

        public IEnumerable<DeviceData> GetData();

        public IEnumerable<DeviceData> GetData(IEnumerable<string> idInformations);

        public void SendData(string idInformation, string data);
    }
}
