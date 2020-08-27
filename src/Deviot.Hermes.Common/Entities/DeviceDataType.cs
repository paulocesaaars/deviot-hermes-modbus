namespace Deviot.Hermes.Common
{
    public class DeviceDataType : Enumeration
    {
        public static DeviceDataType Undefined = new DeviceDataType(0, false, "Indefinido");
        public static DeviceDataType Digital = new DeviceDataType(1, true, "Digital");
        public static DeviceDataType Int = new DeviceDataType(2, true, "Númerico");
        public static DeviceDataType Int64 = new DeviceDataType(3, true, "Númerico de 64 bits");
        public static DeviceDataType Decimal = new DeviceDataType(4, true, "Decimal");
        public static DeviceDataType DateTime = new DeviceDataType(5, true, "Data e hora");
        public static DeviceDataType Text = new DeviceDataType(6, true, "Texto");

        public DeviceDataType(ushort id, bool available, string name) : base(id, available, name)
        {

        }
    }
}
