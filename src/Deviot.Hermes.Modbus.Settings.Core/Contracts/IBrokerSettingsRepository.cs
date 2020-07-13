using Deviot.Hermes.Modbus.Common.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Core.Contracts
{
    public interface IBrokerSettingsRepository
    {
        Task<BrokerDevice> GetAsync();

        Task UpdateAsync(BrokerDevice device);

        Task<BrokerDevice> ResetAsync();
    }
}
