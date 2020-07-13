using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Deviot.Hermes.Common.BaseController
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        #region Attributes
        protected readonly INotifier _notifier;
        protected readonly Guid _transactionId;
        protected readonly ILogger _logger;
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        public BaseController(INotifier notifier, ILogger logger)
        {
            _notifier = notifier;
            _logger = logger;
            _transactionId = Guid.NewGuid();
        }
        #endregion

        #region Methods

        #region Private
        private void NotifyInvalidModelError(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors);
            foreach (var error in errors)
            {
                var errorMsg = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMsg);
            }
        }

        private void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
        #endregion

        #region Public
        #endregion

        #region Protected
        protected void LogExceptions(Exception exception)
        {
            foreach (var message in Utility.GetAllExceptionMessages(exception))
                _logger.LogError($"{_transactionId} - {message}");
        }

        protected bool ValidateRequest()
        {
            return !_notifier.HasNotification();
        }

        protected ActionResult CustomResponse(object value = null)
        {
            if (ValidateRequest())
                return Ok(new DeviotActionResult(value));

            return BadRequest(new DeviotActionResult(_notifier.GetNotifications().Select(x => x.Message)));
        }

        protected ActionResult ReturnActionResultForInvalidModelState(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
                NotifyInvalidModelError(modelState);

            return ReturnActionResultForInvalidInformation();
        }

        protected ActionResult ReturnActionResultForGenericError(string message = "Houve um erro não identificado procure o suporte técnico.")
        {
            return BadRequest(new DeviotActionResult(null, $"{_transactionId} - {message}"));
        }

        protected ActionResult ReturnActionResultForInvalidInformation(string message = "As inforções passadas na requisição, são inválidas.")
        {
            return BadRequest(new DeviotActionResult(_notifier.GetNotifications().Select(x => x.Message), message));
        }

        protected ActionResult ReturnActionResultForInvalidId(string message = "O id informado na requisição é inválido.")
        {
            NotifyError(message);
            return ReturnActionResultForInvalidInformation();
        }
        #endregion

        #endregion

        #region Events
        #endregion
    }
}
