using AutoMapper;
using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseService;
using Deviot.Hermes.Modbus.Application.Interfaces;
using Deviot.Hermes.Modbus.Application.ModelViews;
using Deviot.Hermes.Modbus.Domain.Contracts;
using Deviot.Hermes.Modbus.Domain.Entities;
using Deviot.Hermes.Modbus.Domain.Validations;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Services
{
    public class DeviceSettingsService : BaseService, IDeviceSettingsService
    {
        #region Attributes
        private readonly IMapper _mapper;
        private readonly IDeviceSettingsRepository _deviceSettingsRepository;
        private readonly IDeviceDriverService _deviceDriverService;
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        #endregion

        #region Methods

        #region Private
        private bool Validate(ModbusDevice device)
        {
            return Validate<ModbusDevice, ModbusDeviceValidation>(device, new ModbusDeviceValidation());
        }
        #endregion

        #region Public
        public DeviceSettingsService(INotifier notifier, IMapper mapper, IDeviceSettingsRepository deviceSettingsRepository, IDeviceDriverService deviceDriverService) : base(notifier)
        {
            _mapper = mapper;
            _deviceSettingsRepository = deviceSettingsRepository;
            _deviceDriverService = deviceDriverService;
        }

        public async Task<ModbusDeviceModelView> GetAsync()
        {
            try
            {
                return _mapper.Map<ModbusDeviceModelView>(await _deviceSettingsRepository.GetAsync());
            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception("Houve um erro ao buscar os dispositivos.", exception);
            }
        }

        public async Task UpdateAsync(ModbusDeviceModelView modbusDeviceModelView)
        {
            try
            {
                var device = _mapper.Map<ModbusDevice>(modbusDeviceModelView);
                if (Validate(device))
                {
                    await _deviceSettingsRepository.UpdateAsync(device);
                    _deviceDriverService.UpdateDevice(device);
                }
            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
            }
            catch (Exception exception)
            {
                throw new Exception($"Houve um erro ao atualizar o dispositivo modbus.", exception);
            }
        }

        public async Task ResetAsync()
        {
            try
            {
                var device = await _deviceSettingsRepository.ResetAsync();
                _deviceDriverService.UpdateDevice(device);
            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
            }
            catch (Exception exception)
            {
                throw new Exception($"Houve um erro ao resetar as configurações de dispositivo.", exception);
            }
        }
        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}
