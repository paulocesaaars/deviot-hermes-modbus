using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Infra.Data.Jsons
{
    public class ModbusInformationUndefinedJson : ModbusInformationJson
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.Undefined;

        public override DeviceDataType DataType => DeviceDataType.Undefined;
    }
}
