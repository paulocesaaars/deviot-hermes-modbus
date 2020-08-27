using AutoMapper;
using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseService;
using Deviot.Hermes.Modbus.Application.Interfaces;
using Deviot.Hermes.Modbus.Application.ModelViews;
using Deviot.Hermes.Modbus.Domain.Contracts;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Application.Services
{
    public class BrokerService : BaseService, IBrokerService
    {
        private readonly IMapper _mapper;
        private readonly IDeviceSettingsRepository _deviceSettingsRepository;
        private readonly IBrokerSettingsRepository _brokerSettingsRepository;
        private readonly IBrokerDriverService _brokerDriverService;

        public BrokerService(INotifier notifier, IMapper mapper, IBrokerSettingsRepository brokerSettingsRepository, IBrokerDriverService brokerDriverService, IDeviceSettingsRepository deviceSettingsRepository) : base(notifier)
        {
            _mapper = mapper;
            _deviceSettingsRepository = deviceSettingsRepository;
            _brokerSettingsRepository = brokerSettingsRepository;
            _brokerDriverService = brokerDriverService;
        }

        public MosquittoBrokerStatusViewModel GetDeviceStatus()
        {
            return _mapper.Map<MosquittoBrokerStatusViewModel>(_brokerDriverService.GetBrokerStatus());
        }

        public async Task Start()
        {
            var broker = await _brokerSettingsRepository.GetAsync();
            var device = await _deviceSettingsRepository.GetAsync();
            _brokerDriverService.Start(broker, device);
        }

        public void Stop()
        {
            _brokerDriverService.Stop();
        }
    }
}
