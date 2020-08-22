using Deviot.Hermes.Modbus.Application.ModelViews;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Interfaces
{
    public interface IDeviceService
    {
        Task Start();

        void Stop();

        ModbusStatusDeviceModelView GetStatusDevice();

        IEnumerable<DeviceDataModelView> GetData();

        IEnumerable<DeviceDataModelView> GetData(IEnumerable<string> idInformations);

        void SendData(string idInformation, string data);
    }
}
