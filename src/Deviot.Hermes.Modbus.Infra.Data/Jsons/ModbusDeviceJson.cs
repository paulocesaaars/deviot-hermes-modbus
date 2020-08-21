﻿using System.Collections.Generic;

namespace Deviot.Hermes.Modbus.Infra.Data.Jsons
{
    public class ModbusDeviceJson
    {
        public string Description { get; set; }

        public bool Active { get; set; }

        public string Ip { get; set; }

        public int Port { get; set; }

        public int Scan { get; set; }

        public int Timeout { get; set; }

        public int MaximumNumberOfReadingAttempts { get; set; }

        public IEnumerable<ModbusInformationJson> ModbusInformations { get; set; }
    }
}
