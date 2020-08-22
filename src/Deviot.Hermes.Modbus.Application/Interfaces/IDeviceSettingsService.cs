using Deviot.Hermes.Modbus.Application.ModelViews;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Interfaces
{
    public interface IDeviceSettingsService
    {
        Task<ModbusDeviceModelView> GetAsync();

        Task UpdateAsync(ModbusDeviceModelView device);

        Task ResetAsync();
    }
}
