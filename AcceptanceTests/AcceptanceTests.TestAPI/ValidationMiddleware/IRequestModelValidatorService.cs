using System;
using System.Collections.Generic;
using FluentValidation.Results;

namespace AcceptanceTests.TestAPI.ValidationMiddleware
{
    public interface IRequestModelValidatorService
    {
        IList<ValidationFailure> Validate(Type requestModel, object modelValue);
    }
}