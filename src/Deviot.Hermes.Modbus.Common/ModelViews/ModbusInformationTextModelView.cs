using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;

namespace Deviot.Hermes.Modbus.Common.ModelViews
{
    public class ModbusInformationTextModelView : ModbusInformationModelView
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationText;

        public override DeviceDataType DataType => DeviceDataType.Text;
    }
}