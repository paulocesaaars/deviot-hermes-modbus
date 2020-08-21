using System.Collections.Generic;

namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class ModbusDevice
    {
        public string Description { get; private set; }

        public bool Active { get; private set; }

        public string Ip { get; private set; }

        public int Port { get; private set; }

        public int Scan { get; private set; }

        public int Timeout { get; private set; }

        public int MaximumNumberOfReadingAttempts { get; private set; }

        public IEnumerable<ModbusInformation> ModbusInformations { get; private set; }

        public ModbusDevice()
        {

        }

        public ModbusDevice(string description, bool active, string ip, int port, int scan, int timeout, int maximumNumberOfReadingAttempts)
        {
            Description = description;
            Active = active;
            Ip = ip;
            Port = port;
            Scan = scan;
            Timeout = timeout;
            MaximumNumberOfReadingAttempts = maximumNumberOfReadingAttempts;
        }
    }
}
