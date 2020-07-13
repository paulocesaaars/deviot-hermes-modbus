using System.Security.Cryptography;

namespace Deviot.Hermes.Modbus.Common.Entities
{
    public class BrokerDevice
    {
        public string Description { get; private set; }

        public bool Active { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }

        public BrokerDevice()
        {

        }

        public BrokerDevice(string description, bool active, string ip, int port)
        {
            Description = description;
            Active = active;
            Ip = ip;
            Port = port;
        }
    }
}
