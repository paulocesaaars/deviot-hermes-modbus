using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusInformationDigitalModelView : ModbusInformationModelView
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationDigital;

        public override DeviceDataType DataType => DeviceDataType.Digital;
    }
}
