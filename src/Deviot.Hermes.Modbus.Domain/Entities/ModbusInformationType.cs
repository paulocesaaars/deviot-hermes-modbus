using Deviot.Hermes.Common;

namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class ModbusInformationType : Enumeration
    {
        public static ModbusInformationType Undefined = new ModbusInformationType(0, false, "Informação com tipo indefinido");
        public static ModbusInformationType ModbusInformationDigital = new ModbusInformationType(1, true, "Informação modbus do tipo digital");
        public static ModbusInformationType ModbusInformationAnalog = new ModbusInformationType(2, true, "Informação modbus do tipo analógica");
        public static ModbusInformationType ModbusInformationText = new ModbusInformationType(3, true, "Informação modbus do tipo texto");

        public ModbusInformationType(ushort id, bool available, string name) : base(id, available, name)
        {

        }
    }
}
