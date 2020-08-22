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
    [Route("api/v{version:apiVersion}/device-settings")]
    public class DeviceSettingsController : BaseController
    {
        private readonly IDeviceSettingsService _modbusDeviceService;

        public DeviceSettingsController(INotifier notifier, ILogger<DeviceSettingsController> logger, IDeviceSettingsService modbusDeviceService) : base(notifier, logger)
        {
            _modbusDeviceService = modbusDeviceService;
        }

        [HttpGet]
        public async Task<ActionResult<ModbusDeviceModelView>> GetAsync()
        {
            try
            {
                var device = await _modbusDeviceService.GetAsync();
                return CustomResponse(await _modbusDeviceService.GetAsync());
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
                await _modbusDeviceService.ResetAsync();
                return CustomResponse();
            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return ReturnActionResultForGenericError();
            }
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync([FromBody] ModbusDeviceModelView deviceModelView)
        {
            try
            {
                if (deviceModelView == null)
                    return ReturnActionResultForInvalidInformation();

                if (!ModelState.IsValid)
                    return ReturnActionResultForInvalidModelState(ModelState);

                await _modbusDeviceService.UpdateAsync(deviceModelView);
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
