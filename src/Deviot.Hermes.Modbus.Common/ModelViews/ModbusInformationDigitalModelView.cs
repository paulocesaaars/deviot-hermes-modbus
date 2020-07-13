using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;

namespace Deviot.Hermes.Modbus.Common.ModelViews
{
    public class ModbusInformationDigitalModelView : ModbusInformationModelView
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationDigital;

        public override DeviceDataType DataType => DeviceDataType.Digital;
    }
}
