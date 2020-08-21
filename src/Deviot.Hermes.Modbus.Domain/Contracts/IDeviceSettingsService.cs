using Deviot.Hermes.Modbus.Domain.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IDeviceSettingsService
    {
        Task<ModbusDevice> GetAsync();

        Task UpdateAsync(ModbusDevice device);

        Task ResetAsync();
    }
}
