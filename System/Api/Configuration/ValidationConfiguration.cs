using InteractiveHelper.Common.Helpers;
using InteractiveHelper.Common.Responses.Errors;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using InteractiveHelper.Common.Validator;

namespace InteractiveHelper.Api.Configuration;

/// <summary>
/// Validation configuration class
/// </summary>
public static class ValidationConfiguration
{
    /// <summary>
    /// Configures validation and adds to the service collection
    /// </summary>
    public static IMvcBuilder AddValidator(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var fieldErrors = new List<ValidationErrorResponseFieldInfo>();
                foreach (var item in context.ModelState)
                {
                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                        fieldErrors.Add(new ValidationErrorResponseFieldInfo()
                        {
                            FieldName = item.Key,
                            Message = string.Join(", ", item.Value.Errors.Select(x => x.ErrorMessage))
                        });
                }

                var result = new BadRequestObjectResult(new ValidationErrorResponse()
                {
                    Message = "One or more validation errors occurred.",
                    FieldErrors = fieldErrors
                });

                return result;
            };
        });

        builder.AddFluentValidation(fv =>
        {
            fv.DisableDataAnnotationsValidation = true;
            fv.ImplicitlyValidateChildProperties = true;
            fv.AutomaticValidationEnabled = true;
        });

        ValidatorsRegisterHelper.Register(builder.Services);

        builder.Services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return builder;
    }
}
