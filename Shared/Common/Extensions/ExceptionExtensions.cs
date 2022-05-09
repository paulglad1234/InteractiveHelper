namespace InteractiveHelper.Common.Extensions;

using InteractiveHelper.Common.Responses;
using InteractiveHelper.Common.Exceptions;
using FluentValidation;

public static class ErrorResponseExtensions
{
    public static ErrorResponse ToErrorResponse(this ValidationException data)
    {
        var res = new ErrorResponse()
        {
            Message = "",
            FieldErrors = data.Errors.Select(x =>
            {
                var elems = x.ErrorMessage.Split('&');
                var errorName = elems[0];
                var errorMessage = elems.Length > 0 ? elems[1] : errorName;
                return new ErrorResponseFieldInfo()
                {
                    FieldName = x.PropertyName,
                    Message = errorMessage,
                };
            })
        };

        return res;
    }

    public static ErrorResponse ToErrorResponse(this CommonException exception)
    {
        var res = new ErrorResponse()
        {
            ErrorCode = exception.Code,
            Message = exception.Message
        };

        return res;
    }
    public static ErrorResponse ToErrorResponse(this Exception exception)
    {
        var res = new ErrorResponse()
        {
            Message = exception.Message,
            InnerError = exception.InnerException?.ToErrorResponse()
        };

        return res;
    }
}
