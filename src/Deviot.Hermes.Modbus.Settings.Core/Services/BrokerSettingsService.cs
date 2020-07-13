using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseService;
using Deviot.Hermes.Modbus.Common.Entities;
using Deviot.Hermes.Modbus.Common.Validations;
using Deviot.Hermes.Modbus.Core.Contracts;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Core.Services
{
    public class BrokerSettingsService : BaseService, IBrokerSettingsService
    {
        #region Attributes
        private readonly IBrokerSettingsRepository _brokerSettingsRepository;
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        #endregion

        #region Methods

        #region Private
        private bool Validate(BrokerDevice device)
        {
            return Validate<BrokerDevice, BrokerDeviceValidation>(device, new BrokerDeviceValidation());
        }
        #endregion

        #region Public
        public BrokerSettingsService(INotifier notifier, IBrokerSettingsRepository brokerSettingsRepository) : base(notifier)
        {
            _brokerSettingsRepository = brokerSettingsRepository;
        }

        public async Task<BrokerDevice> GetAsync()
        {
            try
            {
                return await _brokerSettingsRepository.GetAsync();
            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
                return null;
            }
            catch (Exception exception)
            {
                throw new Exception("Houve um erro ao buscar o broker.", exception);
            }
        }

        public async Task UpdateAsync(BrokerDevice device)
        {
            try
            {
                if (Validate(device))
                    await _brokerSettingsRepository.UpdateAsync(device);

            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
            }
            catch (Exception exception)
            {
                throw new Exception($"Houve um erro ao atualizar o broker.", exception);
            }
        }

        public async Task ResetAsync()
        {
            try
            {
                var device = await _brokerSettingsRepository.ResetAsync();
            }
            catch (InvalidDataException exception)
            {
                NotifyExceptions(exception);
            }
            catch (Exception exception)
            {
                throw new Exception($"Houve um erro ao resetar as configurações do broker.", exception);
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
