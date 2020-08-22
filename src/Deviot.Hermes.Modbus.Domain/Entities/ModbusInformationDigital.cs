namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class ModbusInformationDigital : ModbusInformation
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationDigital;

        public override DeviceDataType DataType => DeviceDataType.Digital;
    }
}
