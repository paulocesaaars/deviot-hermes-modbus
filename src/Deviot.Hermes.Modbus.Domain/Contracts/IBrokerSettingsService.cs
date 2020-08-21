using Deviot.Hermes.Modbus.Domain.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IBrokerSettingsService
    {
        Task<BrokerDevice> GetAsync();

        Task UpdateAsync(BrokerDevice device);

        Task ResetAsync();
    }
}
