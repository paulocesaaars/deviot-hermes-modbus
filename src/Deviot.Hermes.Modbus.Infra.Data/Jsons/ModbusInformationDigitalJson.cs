using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Infra.Data.Jsons
{
    public class ModbusInformationDigitalJson : ModbusInformationJson
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationDigital;

        public override DeviceDataType DataType => DeviceDataType.Digital;
    }
}
