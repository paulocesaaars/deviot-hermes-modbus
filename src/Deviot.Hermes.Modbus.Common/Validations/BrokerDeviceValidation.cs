using Deviot.Hermes.Modbus.Common.Entities;
using FluentValidation;
using System.Net;

namespace Deviot.Hermes.Modbus.Common.Validations
{
    public class BrokerDeviceValidation : AbstractValidator<BrokerDevice>
    {
        public BrokerDeviceValidation()
        {
            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("A descrição do dispositivo não foi informada.");

            RuleFor(x => x.Ip)
                .NotEmpty().WithMessage("A ip do dispositivo não foi informado.")
                .Custom((ip, context) => {

                    var valid = IPAddress.TryParse(ip, out IPAddress address);
                    if (!valid)
                        context.AddFailure("O ip do dispositivo modbus é inválido.");
                });
        }
    }
}
