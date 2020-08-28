using Deviot.Hermes.Modbus.Domain.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IBrokerSettingsRepository
    {
        Task<MqttBroker> GetAsync();

        Task UpdateAsync(MqttBroker device);

        Task<MqttBroker> ResetAsync();
    }
}
