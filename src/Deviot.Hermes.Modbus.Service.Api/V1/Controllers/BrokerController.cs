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
    [Route("api/v{version:apiVersion}/broker")]
    public class BrokerController : BaseController
    {
        private readonly IBrokerService _brokerService;

        public BrokerController(INotifier notifier, ILogger<BrokerController> logger, IBrokerService brokerService) : base(notifier, logger)
        {
            _brokerService = brokerService;
        }

        [HttpGet]
        public ActionResult<MosquittoBrokerStatusViewModel> Get()
        {
            try
            {
                return CustomResponse(_brokerService.GetDeviceStatus());
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
                await _brokerService.Start();
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
                _brokerService.Stop();
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
