namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class ModbusInformationUndefined : ModbusInformation
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.Undefined;

        public override DeviceDataType DataType => DeviceDataType.Undefined;
    }
}
