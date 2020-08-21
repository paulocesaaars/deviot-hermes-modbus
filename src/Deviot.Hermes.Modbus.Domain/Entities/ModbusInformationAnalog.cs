using Deviot.Hermes.Common.Entities;

namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class ModbusInformationAnalog : ModbusInformation
    {
        public bool Signed { get; private set; }

        public bool Swap { get; private set; }

        public double MinValue { get; private set; }

        public double MaxValue { get; private set; }

        public double MinRawValue { get; private set; }

        public double MaxRawValue { get; private set; }

        public int DecimalPlaces { get; private set; }

        public override ModbusInformationType ModbusInformationType => ModbusInformationType.ModbusInformationAnalog;

        public override DeviceDataType DataType => DeviceDataType.Decimal;
    }
}
