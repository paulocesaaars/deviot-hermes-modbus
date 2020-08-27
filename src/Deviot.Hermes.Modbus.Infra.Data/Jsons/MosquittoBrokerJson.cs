namespace Deviot.Hermes.Modbus.Infra.Data.Jsons
{
    public class MosquittoBrokerJson
    {
        public string Description { get; set; }

        public bool Active { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        public int Timeout { get; set; }
    }
}
