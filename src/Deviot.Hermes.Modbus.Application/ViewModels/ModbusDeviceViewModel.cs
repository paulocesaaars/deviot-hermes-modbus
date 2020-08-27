using System.Collections.Generic;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusDeviceViewModel
    {
        public string Description { get; set; }

        public bool Active { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        public int Scan { get; set; }

        public int Timeout { get; set; }

        public int MaximumNumberOfReadingAttempts { get; set; }

        public IEnumerable<ModbusInformationViewModel> ModbusInformations { get; set; }
    }
}
