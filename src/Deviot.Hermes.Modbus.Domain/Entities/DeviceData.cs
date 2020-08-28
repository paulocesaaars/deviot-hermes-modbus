using System;

namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class DeviceData
    {
        public string IdInformation { get; private set; }

        public string Description { get; private set; }

        public DateTime DateTime { get; private set; }

        public object Value { get; private set; }

        public bool Quality { get; private set; }

        public DeviceData(string idInformation, string description, object value)
        {
            IdInformation = idInformation;
            Description = description;
            DateTime = DateTime.Now;
            Value = value;
            Quality = true;
        }

        public DeviceData(string idInformation, string description, object value, bool quality)
        {
            IdInformation = idInformation;
            Description = description;
            DateTime = DateTime.Now;
            Value = value;
            Quality = quality;
        }

        public void SetValue(object value)
        {
            SetValue(value, true);
        }

        public void SetValue(object value, bool quality)
        {
            DateTime = DateTime.Now;
            Value = value;
            Quality = quality;
        }

        public void BadValue()
        {
            DateTime = DateTime.Now;
            Value = null;
            Quality = false;
        }
    }
}
