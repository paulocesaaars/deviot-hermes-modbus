using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseController;
using Deviot.Hermes.Modbus.Application.Interfaces;
using Deviot.Hermes.Modbus.Application.ModelViews;
using Deviot.Hermes.Modbus.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deviot.Hermes.Modbus.Api.V1.Controllers
{
    [Route("api/v{version:apiVersion}/device")]
    public class DeviceController : BaseController
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(INotifier notifier, ILogger<DeviceSettingsController> logger, IDeviceService deviceService) : base(notifier, logger)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        public ActionResult<ModbusStatusDevice> Get()
        {
            try
            {
                return CustomResponse(_deviceService.GetStatusDevice());
            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return ReturnActionResultForGenericError();
            }
        }

        [HttpGet]
        [Route("data")]
        public ActionResult<IEnumerable<DeviceDataModelView>> GetDataForInformations([FromQuery] string[] id)
        {
            try
            {
                if (id.Length > 0)
                    return CustomResponse(_deviceService.GetData(id));
                else
                    return CustomResponse(_deviceService.GetData());

            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return ReturnActionResultForGenericError();
            }
        }

        [HttpPost]
        [Route("data")]
        public ActionResult PostData(string idInformation, string data)
        {
            try
            {
                _deviceService.SendData(idInformation, data);
                return CustomResponse();
            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return ReturnActionResultForGenericError();
            }
        }

        [HttpPost]
        [Route("start")]
        public async Task<ActionResult> PostStartAsync()
        {
            try
            {
                await _deviceService.Start();
                return CustomResponse();
            }
            catch (Exception exception)
            {
                LogExceptions(exception);
                return ReturnActionResultForGenericError();
            }
        }

        [HttpPost]
        [Route("stop")]
        public ActionResult PostStop()
        {
            try
            {
                _deviceService.Stop();
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
