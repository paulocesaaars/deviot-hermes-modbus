using Deviot.Hermes.Modbus.Infra.CrossCutting.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Deviot.Hermes.Modbus.Service.Api
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            HermesModbusInject.RegisterServices(services);
        }
    }
}
