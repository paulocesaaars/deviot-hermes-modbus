using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Entities;
using System;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class DeviceDataViewModel
    {
        public string IdInformation { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public object Value { get; set; }

        public bool Quality { get; set; }
    }
}
