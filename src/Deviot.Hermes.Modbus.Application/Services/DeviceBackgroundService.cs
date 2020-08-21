using Deviot.Hermes.Modbus.Domain.Contracts;
using Deviot.Hermes.Modbus.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Services
{
    public class DeviceBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<DeviceBackgroundService> _logger;
        private readonly IDeviceDriverService _modbusDriverService;

        private async Task<ModbusDevice> GetSettings()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var modbusDeviceRepository = scope.ServiceProvider.GetRequiredService<IDeviceSettingsRepository>();
                    return await modbusDeviceRepository.GetAsync();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return null;
            }
        }

        public DeviceBackgroundService(ILogger<DeviceBackgroundService> logger, IServiceProvider serviceProvider, IDeviceDriverService modbusDriverService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _modbusDriverService = modbusDriverService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var device = await GetSettings();
                _modbusDriverService.Start(device);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

            await Task.Delay(Timeout.Infinite, stoppingToken);

            _modbusDriverService.Dispose();
        }
    }
}
