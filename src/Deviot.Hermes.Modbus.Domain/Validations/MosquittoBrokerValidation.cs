﻿using Deviot.Hermes.Modbus.Domain.Entities;
using FluentValidation;

namespace Deviot.Hermes.Modbus.Domain.Validations
{
    public class MosquittoBrokerValidation : AbstractValidator<MqttBroker>
    {
        public MosquittoBrokerValidation()
        {
            
        }
    }
}
