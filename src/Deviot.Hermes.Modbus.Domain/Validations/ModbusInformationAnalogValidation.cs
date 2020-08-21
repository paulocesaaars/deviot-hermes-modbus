using Deviot.Hermes.Modbus.Domain.Entities;
using FluentValidation;

namespace Deviot.Hermes.Modbus.Domain.Validations
{
    public class ModbusInformationAnalogValidation : ModbusInformationValidation<ModbusInformationAnalog>
    {
        public ModbusInformationAnalogValidation()
        {
            RuleFor(x => x.NumberOfAddresses)
                .Custom((numberOfAddresses, context) => {
                    if (numberOfAddresses > 2)
                        context.AddFailure("Só é permitido até 2 endereços para informações modbus analógica.");
                });

            RuleFor(x => x)
                .Custom((information, context) => {
                    if (information.MinValue >= information.MaxValue)
                        context.AddFailure("O valor mínimo deve ser menor que o valor máximo.");
                    else if (information.MinRawValue >= information.MaxRawValue)
                        context.AddFailure("O valor mínimo bruto deve ser menor que o valor máximo bruto.");
                });

            RuleFor(x => x.DecimalPlaces)
                .Custom((decimalPlaces, context) => {
                    if (decimalPlaces > 6)
                        context.AddFailure("O número de casas decimais deve ser inferior a 6.");
                });
        }
    }
}
