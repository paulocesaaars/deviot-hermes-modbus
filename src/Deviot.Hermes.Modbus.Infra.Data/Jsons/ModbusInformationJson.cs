using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Entities;
using System.Text.Json.Serialization;

namespace Deviot.Hermes.Modbus.Infra.Data.Jsons
{
    [JsonConverter(typeof(ModbusInformationJsonSerializer))]
    public abstract class ModbusInformationJson
    {
        public string Id { get; set; }

        public string Topic { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        public bool EnableWrite { get; set; }

        public ushort ModbusInformationTypeId { get; set; }

        public abstract ModbusInformationType ModbusInformationType { get; }

        public abstract DeviceDataType DataType { get; }

        public byte DeviceId { get; set; }

        public ushort ModbusFunctionTypeId { get; set; }

        public ModbusFunctionType ModbusFunctionType
        {
            get
            {
                if (ModbusFunctionTypeId == ModbusFunctionType.InputStatus.Id)
                    return ModbusFunctionType.InputStatus;
                else if (ModbusFunctionTypeId == ModbusFunctionType.CoilStatus.Id)
                    return ModbusFunctionType.CoilStatus;
                else if (ModbusFunctionTypeId == ModbusFunctionType.HoldingRegister.Id)
                    return ModbusFunctionType.HoldingRegister;
                else if (ModbusFunctionTypeId == ModbusFunctionType.InputRegister.Id)
                    return ModbusFunctionType.InputRegister;
                else
                    return ModbusFunctionType.Undefined;
            }
        }

        public ushort StartAddress { get; set; }

        public ushort NumberOfAddresses { get; set; }
    }
}
