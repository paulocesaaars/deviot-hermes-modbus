using Deviot.Hermes.Modbus.Application.ModelViews;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Interfaces
{
    public interface IBrokerService
    {
        Task Start();

        void Stop();

        MosquittoBrokerStatusViewModel GetDeviceStatus();

    }
}
