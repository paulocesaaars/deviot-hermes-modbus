﻿namespace Deviot.Hermes.Modbus.Domain.Entities
{
    public abstract class ModbusInformation
    {
        public string Id { get; protected set; }

        

        public string Description { get; protected set; }

        public bool Active { get; protected set; }

        public bool EnableWrite { get; protected set; }

        public ushort ModbusInformationTypeId { get; protected set; }

        public abstract ModbusInformationType ModbusInformationType { get; }

        public byte DeviceId { get; protected set; }

        public ushort ModbusFunctionTypeId { get; protected set; }

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

        public ushort StartAddress { get; protected set; }

        public ushort NumberOfAddresses { get; private set; }

        public string Topic { get; protected set; }

        public byte QosLevel { get; protected set; }

        public bool Retain { get; protected set; }
    }
}
