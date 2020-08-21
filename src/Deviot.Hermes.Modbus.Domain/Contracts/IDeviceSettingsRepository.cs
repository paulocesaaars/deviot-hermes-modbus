using Deviot.Hermes.Modbus.Domain.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IDeviceSettingsRepository
    {
        Task<ModbusDevice> GetAsync();

        Task UpdateAsync(ModbusDevice device);

        Task<ModbusDevice> ResetAsync();
    }
}
