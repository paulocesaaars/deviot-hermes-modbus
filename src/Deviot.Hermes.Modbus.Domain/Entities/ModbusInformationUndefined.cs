namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class ModbusInformationUndefined : ModbusInformation
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.Undefined;
    }
}
