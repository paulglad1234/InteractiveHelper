namespace InteractiveHelper.Common.Responses.Errors;

public class UnknownErrorResponse : ErrorResponse
{
    public UnknownErrorResponse InnerError { get; set; }
}

