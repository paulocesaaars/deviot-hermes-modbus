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
    public class BrokerSettingsService : BaseService, IBrokerSettingsService
    {
        #region Attributes
        private readonly IMapper _mapper;
        private readonly IDeviceSettingsRepository _deviceSettingsRepository;
        private readonly IBrokerSettingsRepository _brokerSettingsRepository;
        private readonly IBrokerDriverService _brokerDriverService;
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        #endregion

        #region Methods

        #region Private
        private bool Validate(MosquittoBroker broker)
        {
            return Validate<MosquittoBroker, MosquittoBrokerValidation>(broker, new MosquittoBrokerValidation());
        }
        #endregion

        #region Public
        public BrokerSettingsService(INotifier notifier, IMapper mapper, IBrokerSettingsRepository brokerSettingsRepository, IBrokerDriverService brokerDriverService, IDeviceSettingsRepository deviceSettingsRepository) : base(notifier)
        {
            _mapper = mapper;
            _deviceSettingsRepository = deviceSettingsRepository;
            _brokerSettingsRepository = brokerSettingsRepository;
            _brokerDriverService = brokerDriverService;
        }

        public async Task<MosquittoBrokerViewModel> GetAsync()
        {
            try
            {
                return _mapper.Map<MosquittoBrokerViewModel>(await _brokerSettingsRepository.GetAsync());
            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception("Houve um erro ao buscar a configuração do broker Mosquitto.", exception);
            }
        }

        public async Task UpdateAsync(MosquittoBrokerViewModel mosquittoBroker)
        {
            try
            {
                var broker = _mapper.Map<MosquittoBroker>(mosquittoBroker);
                if (Validate(broker))
                {
                    await _brokerSettingsRepository.UpdateAsync(broker);
                    var device = await _deviceSettingsRepository.GetAsync();
                    _brokerDriverService.UpdateDevice(broker, device);
                }
            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
            }
            catch (Exception exception)
            {
                throw new Exception($"Houve um erro ao atualizar o broker Mosquitto.", exception);
            }
        }

        public async Task ResetAsync()
        {
            try
            {
                var broker = await _brokerSettingsRepository.ResetAsync();
                var device = await _deviceSettingsRepository.ResetAsync();
                _brokerDriverService.UpdateDevice(broker, device);
            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
            }
            catch (Exception exception)
            {
                throw new Exception($"Houve um erro ao resetar as configurações do broker Mosquitto.", exception);
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
