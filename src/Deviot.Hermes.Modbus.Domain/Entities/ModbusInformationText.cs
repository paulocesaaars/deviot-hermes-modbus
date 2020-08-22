namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class ModbusInformationText : ModbusInformation
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationText;

        public override DeviceDataType DataType => DeviceDataType.Text;
    }
}