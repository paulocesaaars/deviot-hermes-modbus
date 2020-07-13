using Deviot.Hermes.Common.Entities;

namespace Deviot.Hermes.Modbus.Common.Entities
{
    public class ModbusInformationUndefined : ModbusInformation
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.Undefined;

        public override DeviceDataType DataType => DeviceDataType.Undefined;
    }
}
