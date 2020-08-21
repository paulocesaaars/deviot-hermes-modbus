using Deviot.Hermes.Common;

namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public class ModbusFunctionType : Enumeration
    {
        public static ModbusFunctionType Undefined = new ModbusFunctionType(0, false, "Função indefinida");
        public static ModbusFunctionType CoilStatus = new ModbusFunctionType(1, true, "CoilStatus");
        public static ModbusFunctionType InputStatus = new ModbusFunctionType(2, true, "InputStatus");
        public static ModbusFunctionType HoldingRegister = new ModbusFunctionType(3, true, "HoldingRegister");
        public static ModbusFunctionType InputRegister = new ModbusFunctionType(4, true, "InputRegister");

        public ModbusFunctionType(ushort id, bool available, string name) : base(id, available, name)
        {

        }
    }
}
