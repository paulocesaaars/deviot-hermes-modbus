using System.Collections.Generic;

namespace Deviot.Hermes.Modbus.Common.ModelViews
{
    public class ModbusDeviceModelView
    {
        public string Description { get; set; }

        public bool Active { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        public int Scan { get; set; }

        public int Timeout { get; set; }

        public int MaximumNumberOfReadingAttempts { get; set; }

        public IEnumerable<ModbusInformationModelView> ModbusInformations { get; set; }
    }
}
