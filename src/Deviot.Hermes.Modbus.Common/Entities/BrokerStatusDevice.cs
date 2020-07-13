namespace Deviot.Hermes.Modbus.Common.Entities
{
    public class BrokerStatusDevice
    {
        public string Description { get; private set; }

        public bool Active { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }

        public bool Status { get; private set; }

        public BrokerStatusDevice(string description, bool active, string ip, int port, bool status)
        {
            Description = description;
            Active = active;
            Ip = ip;
            Port = port;
            Status = status;
        }
    }
}
