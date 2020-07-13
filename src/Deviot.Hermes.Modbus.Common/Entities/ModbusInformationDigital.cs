using Deviot.Hermes.Common.Entities;

namespace Deviot.Hermes.Modbus.Common.Entities
{
    public class ModbusInformationDigital : ModbusInformation
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationDigital;

        public override DeviceDataType DataType => DeviceDataType.Digital;
    }
}
