using Deviot.Hermes.Modbus.Common.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Core.Contracts
{
    public interface IDeviceSettingsService
    {
        Task<ModbusDevice> GetAsync();

        Task UpdateAsync(ModbusDevice device);

        Task ResetAsync();
    }
}
