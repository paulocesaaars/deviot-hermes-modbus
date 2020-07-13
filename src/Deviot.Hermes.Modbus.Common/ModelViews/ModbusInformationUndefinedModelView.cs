using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;

namespace Deviot.Hermes.Modbus.Common.ModelViews
{
    public class ModbusInformationUndefinedModelView : ModbusInformationModelView
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.Undefined;

        public override DeviceDataType DataType => DeviceDataType.Undefined;
    }
}
