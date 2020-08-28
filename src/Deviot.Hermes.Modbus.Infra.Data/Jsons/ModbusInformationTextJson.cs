using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Infra.Data.Jsons
{
    public class ModbusInformationTextJson : ModbusInformationJson
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationText;
    }
}