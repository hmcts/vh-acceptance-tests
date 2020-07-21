using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;

namespace AcceptanceTests.TestAPI.ValidationMiddleware
{
    public class RequestModelValidatorService : IRequestModelValidatorService
    {
        private readonly IValidatorFactory _validatorFactory;

        public RequestModelValidatorService(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public IList<ValidationFailure> Validate(Type requestModel, object modelValue)
        {
            var validator = _validatorFactory.GetValidator(requestModel);
            var result = validator.Validate(modelValue as IValidationContext);
            return result.Errors;
        }
    }
}