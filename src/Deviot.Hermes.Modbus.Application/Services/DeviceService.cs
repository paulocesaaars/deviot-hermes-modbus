using AutoMapper;
using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseService;
using Deviot.Hermes.Modbus.Application.Interfaces;
using Deviot.Hermes.Modbus.Application.ModelViews;
using Deviot.Hermes.Modbus.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        private readonly IMapper _mapper;
        private readonly IDeviceSettingsRepository _deviceSettingsRepository;
        private readonly IDeviceDriverService _deviceDriverService;

        public DeviceService(INotifier notifier, IMapper mapper, IDeviceSettingsRepository deviceSettingsRepository, IDeviceDriverService deviceDriverService) : base(notifier)
        {
            _mapper = mapper;
            _deviceSettingsRepository = deviceSettingsRepository;
            _deviceDriverService = deviceDriverService;
        }

        public IEnumerable<DeviceDataModelView> GetData()
        {
            return _mapper.Map<IEnumerable<DeviceDataModelView>>(_deviceDriverService.GetData());
        }

        public IEnumerable<DeviceDataModelView> GetData(IEnumerable<string> idInformations)
        {
            return _mapper.Map<IEnumerable<DeviceDataModelView>>(_deviceDriverService.GetData(idInformations));
        }

        public ModbusStatusDeviceModelView GetStatusDevice()
        {
            return _mapper.Map<ModbusStatusDeviceModelView>(_deviceDriverService.GetStatusDevice());
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
    }
}
