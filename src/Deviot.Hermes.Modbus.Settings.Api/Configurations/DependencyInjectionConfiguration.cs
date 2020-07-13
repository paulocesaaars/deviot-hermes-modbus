using AutoMapper;
using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Common.Mappings;
using Deviot.Hermes.Modbus.Core.Contracts;
using Deviot.Hermes.Modbus.Core.Services;
using Deviot.Hermes.Modbus.Data;
using Deviot.Hermes.Modbus.Driver;
using Microsoft.Extensions.DependencyInjection;

namespace Deviot.Hermes.Modbus.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Injeções - Deviot Common
            services.AddScoped<INotifier, Notifier>();

            // Injeções  Common
            services.AddAutoMapper(typeof(ModelViewsMapping));

            // Injeções - Core
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceSettingsService, DeviceSettingsService>();
            services.AddScoped<IDeviceSettingsRepository, DeviceSettingsRepository>();
            services.AddSingleton<IDeviceDriverService, DeviceDriverService>();

            //services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IBrokerSettingsService, BrokerSettingsService>();
            services.AddScoped<IBrokerSettingsRepository, BrokerSettingsRepository>();
            //services.AddSingleton<IDeviceDriverService, DeviceDriverService>();

            services.AddHostedService<DeviceBackgroundService>();

            return services;
        }
    }
}
