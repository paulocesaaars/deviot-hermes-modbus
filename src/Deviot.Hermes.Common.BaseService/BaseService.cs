using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Deviot.Hermes.Common.BaseService
{
    public abstract class BaseService
    {
        #region Attributes
        protected readonly INotifier _notifier;
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        public BaseService(INotifier notifier)
        {
            _notifier = notifier;
        }
        #endregion

        #region Methods

        #region Private
        private void Notifier(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                _notifier.Handle(new Notification(error.ErrorMessage));
        }
        #endregion

        #region Public
        #endregion

        #region Protected
        protected bool Validate<Model, ModelValidation>(Model model, ModelValidation modelValidation) where ModelValidation : AbstractValidator<Model>
        {
            var validator = modelValidation.Validate(model);

            if (validator.IsValid)
                return true;

            Notifier(validator);
            return false;
        }

        protected void NotifyMessage(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected void NotifyMessages(IEnumerable<string> messages)
        {
            foreach (var message in messages)
                NotifyMessage(message);
        }

        protected void NotifyExceptions(Exception exception)
        {
            NotifyMessages(Utility.GetAllExceptionMessages(exception));
        }
        #endregion

        #endregion

        #region Events
        #endregion
    }
}
