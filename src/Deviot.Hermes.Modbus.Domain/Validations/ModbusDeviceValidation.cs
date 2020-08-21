using Deviot.Hermes.Modbus.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;
using System.Net;

namespace Deviot.Hermes.Modbus.Domain.Validations
{
    public class ModbusDeviceValidation : AbstractValidator<ModbusDevice>
    {
        private ValidationResult Validate(ModbusInformation modbusInformation)
        {
            if (modbusInformation.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationDigital))
                return new ModbusInformationDigitalValidation().Validate(modbusInformation as ModbusInformationDigital);
            else if (modbusInformation.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationAnalog))
                return new ModbusInformationAnalogValidation().Validate(modbusInformation as ModbusInformationAnalog);
            else if (modbusInformation.ModbusInformationType.Equals(ModbusInformationType.ModbusInformationText))
                return new ModbusInformationTextValidation().Validate(modbusInformation as ModbusInformationText);
            else
                return new ModbusInformationUndefinedValidation().Validate(modbusInformation as ModbusInformationUndefined);
        }

        public ModbusDeviceValidation()
        {
            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("A descrição do dispositivo não foi informada.");

            RuleFor(x => x.ModbusInformations)
                .Custom((deviceInformation, context) => {

                    if (deviceInformation.Count() > deviceInformation.Select(x => x.Id).Distinct().Count())
                        context.AddFailure($"Existe informações com ids duplicados.");
                });

            RuleFor(x => x.Ip)
                .NotEmpty().WithMessage("A ip do dispositivo não foi informado.")
                .Custom((ip, context) => {

                    var valid = IPAddress.TryParse(ip, out IPAddress address);
                    if (!valid)
                        context.AddFailure("O ip do dispositivo modbus é inválido.");
                });

            RuleFor(x => x.Scan)
                .Custom((scan, context) => {
                    if (scan < 500)
                        context.AddFailure("O scan deve ser superior a 500 milisegundos.");
                });

            RuleFor(x => x.Timeout)
                .Custom((timeout, context) => {
                    if (timeout > 10000)
                        context.AddFailure("O timeout deve ser inferior a 10 segundos.");
                });

            RuleFor(x => x.MaximumNumberOfReadingAttempts)
                .Custom((maximumNumberOfReadingAttempts, context) => {
                    if (maximumNumberOfReadingAttempts > 10)
                        context.AddFailure("O número máximo de tentativas de leitura deve ser inferior a 10.");
                });

            RuleFor(x => x.ModbusInformations)
                .Custom((modbusInformations, context) => {
                    if (modbusInformations.Count() > modbusInformations.Select(x => x.Id).Distinct().Count())
                        context.AddFailure($"Existe informações com ids duplicados.");

                    foreach (var information in modbusInformations)
                    {
                        var validationResult = Validate(information);
                        if (!validationResult.IsValid)
                            foreach (var error in validationResult.Errors)
                                context.AddFailure(error);
                    }
                });
        }
    }
}
