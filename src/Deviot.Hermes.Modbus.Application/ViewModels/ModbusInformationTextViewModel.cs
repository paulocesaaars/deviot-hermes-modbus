using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusInformationTextViewModel : ModbusInformationViewModel
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationText;
    }
}