using Deviot.Hermes.Common;
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
    public class HermesBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<HermesBackgroundService> _logger;
        private readonly IDeviceDriverService _modbusDriverService;
        private readonly IBrokerDriverService _brokerDriverService;

        private async Task<ModbusDevice> GetDeviceSettings()
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

        private async Task<MosquittoBroker> GetBrokerSettings()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var modbusDeviceRepository = scope.ServiceProvider.GetRequiredService<IBrokerSettingsRepository>();
                    return await modbusDeviceRepository.GetAsync();
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return null;
            }
        }

        public HermesBackgroundService(ILogger<HermesBackgroundService> logger, IServiceProvider serviceProvider, IDeviceDriverService modbusDriverService, IBrokerDriverService brokerDriverService)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _modbusDriverService = modbusDriverService;
            _brokerDriverService = brokerDriverService;

            _modbusDriverService.ChangedDataEvent += ChangedData;
            _brokerDriverService.ReceivedWriteDataEvent += ReceivedWriteData;
        }

        public void ChangedData(object sender, DeviceData deviceData)
        {

        }

        public void ReceivedWriteData(object sender, DeviceData deviceData)
        {

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var device = await GetDeviceSettings();
                _modbusDriverService.Start(device);

                var broker = await GetBrokerSettings();
                _brokerDriverService.Start(broker, device);
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
