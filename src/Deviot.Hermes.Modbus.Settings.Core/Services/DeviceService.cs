using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseService;
using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;
using Deviot.Hermes.Modbus.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Core.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        private readonly IDeviceSettingsRepository _deviceSettingsRepository;
        private readonly IDeviceDriverService _deviceDriverService;

        public DeviceService(INotifier notifier, IDeviceSettingsRepository deviceSettingsRepository, IDeviceDriverService deviceDriverService) : base(notifier)
        {
            _deviceSettingsRepository = deviceSettingsRepository;
            _deviceDriverService = deviceDriverService;
        }

        public IEnumerable<DeviceData> GetData()
        {
            return _deviceDriverService.GetData();
        }

        public IEnumerable<DeviceData> GetData(IEnumerable<string> idInformations)
        {
            return _deviceDriverService.GetData(idInformations);
        }

        public ModbusStatusDevice GetStatusDevice()
        {
            return _deviceDriverService.GetStatusDevice();
        }

        public void SendData(string idInformation, string data)
        {
            try
            {
                _deviceDriverService.SendData(idInformation, data);
            }
            catch(InvalidOperationException exception)
            {
                NotifyExceptions(exception);
            }
            catch(Exception exception)
            {
                throw new Exception($"Não foi possível escrever o dado {data}, na informação de id {idInformation}.", exception);
            }
        }

        public async Task Start()
        {
            var device = await _deviceSettingsRepository.GetAsync();
            _deviceDriverService.Start(device);
        }

        public void Stop()
        {
            _deviceDriverService.Stop();
        }

        public void UpdateDevice(ModbusDevice modbusDevice)
        {
            _deviceDriverService.UpdateDevice(modbusDevice);
        }
    }
}
