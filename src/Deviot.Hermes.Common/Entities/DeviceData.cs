using System;

namespace Deviot.Hermes.Common.Entities
{
    public class DeviceData
    {
        public string IdInformation { get; private set; }

        public string Description { get; private set; }

        public DateTime DateTime { get; private set; }

        public DeviceDataType DataType { get; private set; }

        public object Value { get; private set; }

        public bool Quality { get; private set; }

        public DeviceData(string idInformation, string description, DeviceDataType dataType, object value)
        {
            IdInformation = idInformation;
            Description = description;
            DateTime = DateTime.Now;
            DataType = dataType;
            Value = value;
            Quality = true;
        }

        public DeviceData(string idInformation, string description, DeviceDataType dataType, object value, bool quality)
        {
            IdInformation = idInformation;
            Description = description;
            DateTime = DateTime.Now;
            DataType = dataType;
            Value = value;
            Quality = quality;
        }

        public void SetValue(object value)
        {
            SetValue(value, true);
        }

        public void SetValue(object value, bool quality)
        {
            Value = value;
            Quality = quality;
        }

        public void BadValue()
        {
            Value = null;
            Quality = false;
        }
    }
}
