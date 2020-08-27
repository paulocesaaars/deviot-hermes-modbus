using Deviot.Hermes.Modbus.Application.ModelViews;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Interfaces
{
    public interface IBrokerSettingsService
    {
        Task<MosquittoBrokerViewModel> GetAsync();

        Task UpdateAsync(MosquittoBrokerViewModel device);

        Task ResetAsync();
    }
}
