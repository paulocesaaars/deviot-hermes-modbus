using Deviot.Hermes.Common.Entities;

namespace Deviot.Hermes.Modbus.Common.Entities
{
    public class ModbusInformationText : ModbusInformation
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationText;

        public override DeviceDataType DataType => DeviceDataType.Text;
    }
}