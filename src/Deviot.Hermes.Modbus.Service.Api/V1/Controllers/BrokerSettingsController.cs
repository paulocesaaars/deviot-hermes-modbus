using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseController;
using Deviot.Hermes.Modbus.Application.Interfaces;
using Deviot.Hermes.Modbus.Application.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Api.V1.Controllers
{
    [Route("api/v{version:apiVersion}/broker-settings")]
    public class BrokerSettingsController : BaseController
    {
        private readonly IBrokerSettingsService _brokerSettingsService;

        public BrokerSettingsController(INotifier notifier, ILogger<DeviceSettingsController> logger, IBrokerSettingsService brokerSettingsService) : base(notifier, logger)
        {
            _brokerSettingsService = brokerSettingsService;
        }

        [HttpGet]
        public async Task<ActionResult<ModbusDeviceViewModel>> GetAsync()
        {
            try
            {
                return CustomResponse(await _brokerSettingsService.GetAsync());
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
        public async Task<ActionResult> PutAsync([FromBody] MosquittoBrokerViewModel brokerViewModel)
        {
            try
            {
                if (brokerViewModel == null)
                    return ReturnActionResultForInvalidInformation();

                if (!ModelState.IsValid)
                    return ReturnActionResultForInvalidModelState(ModelState);

                await _brokerSettingsService.UpdateAsync(brokerViewModel);
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
