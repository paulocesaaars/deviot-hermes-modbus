using Deviot.Hermes.Modbus.Application.ModelViews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Interfaces
{
    public interface IDeviceService
    {
        Task Start();

        void Stop();

        ModbusDeviceStatusViewModel GetDeviceStatus();

        IEnumerable<DeviceDataViewModel> GetData();

        IEnumerable<DeviceDataViewModel> GetData(IEnumerable<string> idInformations);

        void SendData(string idInformation, string data);
    }
}
