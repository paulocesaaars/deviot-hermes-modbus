using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Domain.Entities;
using FluentValidation;

namespace Deviot.Hermes.Modbus.Domain.Validations
{
    public abstract class ModbusInformationValidation<EntityType> : AbstractValidator<EntityType> where EntityType : ModbusInformation
    {
        public ModbusInformationValidation()
        {
            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("O id da informação não foi informado.");

            RuleFor(x => x.Topic)
               .NotEmpty().WithMessage("O tópico para o broker não foi informado.")
               .Custom((topic, context) => {
                    if (!Utility.ValidateTopic(topic))
                        context.AddFailure("O tópico informado é inválido.");
               });

            RuleFor(x => x.ModbusInformationType)
                .Custom((type, context) => {
                    if (type.Equals(ModbusInformationType.Undefined))
                        context.AddFailure("O tipo da informação informado é inválido.");
                });
        }
    }
}
