using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Device.Core.Contracts
{
    public interface IDeviceManagerService
    {
        public interface IDeviceManagerService : IDisposable
        {
            public Task Start();

            public void Stop();

            public void UpdateDevice(ModbusDevice device);

            public IEnumerable<DeviceData> GetData();

            public IEnumerable<DeviceData> GetData(IEnumerable<string> idInformations);

            public void SendData(string idInformation, object value);
        }
    }
}
