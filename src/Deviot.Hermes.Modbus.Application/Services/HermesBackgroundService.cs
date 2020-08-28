﻿using Deviot.Hermes.Modbus.Domain.Contracts;
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

        private async Task<MqttBroker> GetBrokerSettings()
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

            _modbusDriverService.ReceiveTransferDataFromBrokerEvent += ReceiveTransferDataFromBroker;
            _brokerDriverService.ReceiveTransferDataFromDeviceEvent += ReceiveTransferDataFromDevice;
        }

        public void ReceiveTransferDataFromDevice(object sender, DeviceTransferData deviceTransferData)
        {
            try
            {
                _modbusDriverService.SendData(deviceTransferData);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        public void ReceiveTransferDataFromBroker(object sender, BrokerTrasferData brokerTrasferData)
        {
            try
            {
                _brokerDriverService.SendData(brokerTrasferData);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                var broker = await GetBrokerSettings();
                _brokerDriverService.Start(broker);

                var device = await GetDeviceSettings();
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
