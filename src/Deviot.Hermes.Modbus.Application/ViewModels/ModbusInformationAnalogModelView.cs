using Deviot.Hermes.Modbus.Domain.Entities;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    public class ModbusInformationAnalogModelView : ModbusInformationModelView
    {
        public bool Signed { get; set; }

        public bool Swap { get; set; }

        public double MinValue { get; set; }

        public double MaxValue { get; set; }

        public double MinRawValue { get; set; }

        public double MaxRawValue { get; set; }

        public int DecimalPlaces { get; set; }

        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationAnalog;

        public override DeviceDataType DataType => DeviceDataType.Decimal;
    }
}
