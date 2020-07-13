using Deviot.Hermes.Modbus.Common.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Device.Core.Contracts
{
    public interface IModbusSettingsRepository
    {
        public Task<ModbusDevice> GetAsync();
    }
}
