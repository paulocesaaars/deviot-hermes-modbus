using AutoMapper;
using Deviot.Hermes.Common;
using Deviot.Hermes.Common.BaseController;
using Deviot.Hermes.Common.Entities;
using Deviot.Hermes.Modbus.Common.Entities;
using Deviot.Hermes.Modbus.Core.Contracts;
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
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;

        public DeviceController(INotifier notifier, ILogger<DeviceSettingsController> logger, IMapper mapper, IDeviceService deviceService) : base(notifier, logger)
        {
            _mapper = mapper;
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
        public ActionResult<IEnumerable<DeviceData>> GetDataForInformations([FromQuery] string[] ids)
        {
            try
            {
                if (ids.Length > 0)
                    return CustomResponse(_deviceService.GetData(ids));
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
