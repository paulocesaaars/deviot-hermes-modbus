using Deviot.Hermes.Modbus.Common.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Core.Contracts
{
    public interface IDeviceSettingsRepository
    {
        Task<ModbusDevice> GetAsync();

        Task UpdateAsync(ModbusDevice device);

        Task<ModbusDevice> ResetAsync();
    }
}
