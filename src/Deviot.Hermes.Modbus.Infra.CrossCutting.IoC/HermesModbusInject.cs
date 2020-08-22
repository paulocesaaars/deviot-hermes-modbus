using AutoMapper;
using Deviot.Hermes.Common;
using Deviot.Hermes.Modbus.Application.Interfaces;
using Deviot.Hermes.Modbus.Application.Mappings;
using Deviot.Hermes.Modbus.Application.Services;
using Deviot.Hermes.Modbus.Domain.Contracts;
using Deviot.Hermes.Modbus.Infra.CrossCutting.Driver;
using Deviot.Hermes.Modbus.Infra.Data;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Deviot.Hermes.Modbus.Infra.CrossCutting.IoC
{
    public static class HermesModbusInject
    {
        public static void RegisterServices(IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Injeções - Deviot Common
            services.AddScoped<INotifier, Notifier>();

            // Injeções  - Automapper
            services.AddAutoMapper(typeof(ModelViewsMapping), typeof(DataMapping));

            // Injeções - Device
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IDeviceSettingsService, DeviceSettingsService>();
            services.AddScoped<IDeviceSettingsRepository, DeviceSettingsRepository>();

            services.AddSingleton<IDeviceDriverService, DeviceDriverService>();

            services.AddHostedService<DeviceBackgroundService>();
        }
    }
}
