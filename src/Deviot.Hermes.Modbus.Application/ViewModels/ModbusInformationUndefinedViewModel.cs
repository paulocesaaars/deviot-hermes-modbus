using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusInformationUndefinedViewModel : ModbusInformationViewModel
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.Undefined;
    }
}
