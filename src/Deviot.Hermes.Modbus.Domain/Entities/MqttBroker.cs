using System.Collections.Generic;

namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class MqttBroker
    {
        public string Description { get; private set; }

        public bool Active { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }

        public int Timeout { get; private set; }

        public IEnumerable<MqttTopic> Topics { get; private set; }

        public MqttBroker()
        {

        }

        public MqttBroker(string description, bool active, string ip, int port, int timeout)
        {
            Description = description;
            Active = active;
            Ip = ip;
            Port = port;
            Timeout = timeout;
            Topics = new List<MqttTopic>();
        }
    }
}
