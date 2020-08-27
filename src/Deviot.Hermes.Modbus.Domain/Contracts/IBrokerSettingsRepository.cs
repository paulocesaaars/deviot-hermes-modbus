using Deviot.Hermes.Modbus.Domain.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IBrokerSettingsRepository
    {
        Task<MosquittoBroker> GetAsync();

        Task UpdateAsync(MosquittoBroker device);

        Task<MosquittoBroker> ResetAsync();
    }
}
