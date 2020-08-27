using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusInformationDigitalViewModel : ModbusInformationViewModel
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationDigital;

        public override DeviceDataType DataType => DeviceDataType.Digital;
    }
}
