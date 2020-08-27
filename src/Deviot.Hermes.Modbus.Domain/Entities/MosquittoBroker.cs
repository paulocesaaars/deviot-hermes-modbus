namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class MosquittoBroker
    {
        public string Description { get; private set; }

        public bool Active { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }

        public int Timeout { get; private set; }

        public MosquittoBroker()
        {

        }

        public MosquittoBroker(string description, bool active, string ip, int port, int timeout)
        {
            Description = description;
            Active = active;
            Ip = ip;
            Port = port;
            Timeout = timeout;
        }
    }
}
