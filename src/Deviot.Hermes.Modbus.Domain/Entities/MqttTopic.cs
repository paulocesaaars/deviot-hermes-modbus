namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class MqttTopic
    {
        public string Topic { get; private set; }

        public string IdInformation { get; private set; }

        public byte QosLevel { get; private set; }
    }
}
