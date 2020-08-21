using Deviot.Hermes.Modbus.Domain.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Domain.Contracts
{
    public interface IBrokerService
    {
        public Task Start();

        public void Stop();

        public void UpdateBroker(BrokerDevice brokerDevice);

        public ModbusStatusDevice GetStatusDevice();
    }
}
