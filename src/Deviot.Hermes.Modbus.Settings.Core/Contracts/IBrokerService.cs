using Deviot.Hermes.Modbus.Common.Entities;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Core.Contracts
{
    public interface IBrokerService
    {
        public Task Start();

        public void Stop();

        public void UpdateBroker(BrokerDevice brokerDevice);

        public ModbusStatusDevice GetStatusDevice();
    }
}
