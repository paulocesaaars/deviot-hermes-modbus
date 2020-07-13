﻿using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Deviot.Hermes.Modbus.Common.ModelViews
{
    [JsonConverter(typeof(ModbusInformationJsonSerializer))]
    public abstract class ModbusInformationModelView
    {
        [Required(ErrorMessage = "O id da informação não foi informado.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "O tópico para o broker não foi informado.")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "A descrição da informação não foi informada.")]
        public string Description { get; set; }

        public bool Active { get; set; }

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
