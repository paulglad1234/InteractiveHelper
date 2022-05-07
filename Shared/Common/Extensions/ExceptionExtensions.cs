namespace InteractiveHelper.Common.Extensions;

using InteractiveHelper.Common.Responses;
using InteractiveHelper.Common.Exceptions;
using FluentValidation;

public static class ErrorResponseExtensions
{
    /// <summary>
    /// Make error response from ValidationResult
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Convert process exception to ErrorResponse
    /// </summary>
    /// <param name="exception">Process exception</param>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this CommonException exception)
    {
        var res = new ErrorResponse()
        {
            ErrorCode = exception.Code,
            Message = exception.Message
        };

        return res;
    }

    /// <summary>
    /// Convert exception to ErrorResponse
    /// </summary>
    /// <param name="exception">Exception</param>
    /// <returns></returns>
    public static ErrorResponse ToErrorResponse(this Exception exception)
    {
        var res = new ErrorResponse()
        {
            Message = exception.Message
        };

        return res;
    }
}
