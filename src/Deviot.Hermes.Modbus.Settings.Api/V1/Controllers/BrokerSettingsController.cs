using AutoMapper;
using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseController;
using Deviot.Hermes.Modbus.Common.Entities;
using Deviot.Hermes.Modbus.Common.ModelViews;
using Deviot.Hermes.Modbus.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Api.V1.Controllers
{
    [Route("api/v{version:apiVersion}/broker-settings")]
    public class BrokerSettingsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IBrokerSettingsService _brokerSettingsService;

        public BrokerSettingsController(INotifier notifier, ILogger<BrokerSettingsController> logger, IMapper mapper, IBrokerSettingsService brokerSettingsService) : base(notifier, logger)
        {
            _mapper = mapper;
            _brokerSettingsService = brokerSettingsService;
        }

        [HttpGet]
        public async Task<ActionResult<BrokerDeviceModelView>> GetAsync()
        {
            try
            {
                var broker = await _brokerSettingsService.GetAsync();
                return CustomResponse(_mapper.Map<BrokerDeviceModelView>(broker));
            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return ReturnActionResultForGenericError();
            }
        }

        [HttpPost]
        [Route("reset")]
        public async Task<ActionResult> PostAsync()
        {
            try
            {
                await _brokerSettingsService.ResetAsync();
                return CustomResponse();
            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return ReturnActionResultForGenericError();
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] BrokerDeviceModelView brokerDeviceModelView)
        {
            try
            {
                if (brokerDeviceModelView == null)
                    return ReturnActionResultForInvalidInformation();

                if (!ModelState.IsValid)
                    return ReturnActionResultForInvalidModelState(ModelState);

                await _brokerSettingsService.UpdateAsync(_mapper.Map<BrokerDevice>(brokerDeviceModelView));
                return CustomResponse();
            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return ReturnActionResultForGenericError();
            }
        }
    }
}
