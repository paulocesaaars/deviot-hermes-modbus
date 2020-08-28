namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class DeviceTransferData
    {
        public string IdInformation { get; private set; }

        public string Value { get; private set; }

        public DeviceTransferData(string idInformation, string value)
        {
            IdInformation = idInformation;
            Value = value;
        }
    }
}
