using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusInformationUndefinedViewModel : ModbusInformationViewModel
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.Undefined;

        public override DeviceDataType DataType => DeviceDataType.Undefined;
    }
}
