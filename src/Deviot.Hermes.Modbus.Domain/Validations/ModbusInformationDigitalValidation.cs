using Deviot.Hermes.Modbus.Domain.Entities;
using FluentValidation;

namespace Deviot.Hermes.Modbus.Domain.Validations
{
    public class ModbusInformationDigitalValidation : ModbusInformationValidation<ModbusInformationDigital>
    {
        public ModbusInformationDigitalValidation()
        {
            RuleFor(x => x.NumberOfAddresses)
                .Custom((numberOfAddresses, context) => {
                    if (numberOfAddresses > 1)
                        context.AddFailure("Só é permitido 1 endereço para informações modbus digital.");
                });
        }
    }
}
