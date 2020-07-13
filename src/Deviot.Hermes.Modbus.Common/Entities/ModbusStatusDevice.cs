namespace Deviot.Hermes.Modbus.Common.Entities
{
    public class ModbusStatusDevice
    {
        public string Description { get; private set; }

        public bool Active { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }

        public int Scan { get; private set; }

        public int Timeout { get; private set; }

        public bool Status { get; private set; }

        public ModbusStatusDevice(string description, bool active, string ip, int port, int scan, int timeout, bool status)
        {
            Description = description;
            Active = active;
            Ip = ip;
            Port = port;
            Scan = scan;
            Timeout = timeout;
            Status = status;
        }
    }
}
