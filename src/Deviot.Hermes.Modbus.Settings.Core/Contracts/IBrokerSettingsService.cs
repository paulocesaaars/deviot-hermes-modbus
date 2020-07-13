using Deviot.Hermes.Modbus.Common.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Core.Contracts
{
    public interface IBrokerSettingsService
    {
        Task<BrokerDevice> GetAsync();

        Task UpdateAsync(BrokerDevice device);

        Task ResetAsync();
    }
}
