using Deviot.Hermes.Modbus.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Deviot.Hermes.Modbus.Application.ModelViews
{
    [JsonConverter(typeof(ModbusInformationJsonSerializer))]
    public abstract class ModbusInformationViewModel
    {
        [Required(ErrorMessage = "O id da informação não foi informado.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "A descrição da informação não foi informada.")]
        public string Description { get; set; }

        public bool Active { get; set; }

        public bool EnableWrite { get; set; }

        public ushort ModbusInformationTypeId { get; set; }

        public abstract ModbusInformationType ModbusInformationType { get; }

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

        [Required(ErrorMessage = "O tópico para o broker não foi informado.")]
        public string Topic { get; set; }

        public byte QosLevel { get; set; }

        public bool Retain { get; set; }
    }
}
