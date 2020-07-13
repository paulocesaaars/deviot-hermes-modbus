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
    [Route("api/v{version:apiVersion}/device-settings")]
    public class DeviceSettingsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IDeviceSettingsService _modbusDeviceService;

        public DeviceSettingsController(INotifier notifier, ILogger<DeviceSettingsController> logger, IMapper mapper, IDeviceSettingsService modbusDeviceService) : base(notifier, logger)
        {
            _mapper = mapper;
            _modbusDeviceService = modbusDeviceService;
        }

        [HttpGet]
        public async Task<ActionResult<ModbusDeviceModelView>> GetAsync()
        {
            try
            {
                var device = await _modbusDeviceService.GetAsync();
                return CustomResponse(_mapper.Map<ModbusDeviceModelView>(device));
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

                await _modbusDeviceService.UpdateAsync(_mapper.Map<ModbusDevice>(deviceModelView));
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
