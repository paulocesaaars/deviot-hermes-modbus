using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusInformationTextViewModel : ModbusInformationViewModel
    {
        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationText;

        public override DeviceDataType DataType => DeviceDataType.Text;
    }
}