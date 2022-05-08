namespace InteractiveHelper.Common.Responses.Errors;

public class ValidationErrorResponse : ErrorResponse
{
    public IEnumerable<ValidationErrorResponseFieldInfo> FieldErrors { get; set; }
}

public class ValidationErrorResponseFieldInfo
{
    public string FieldName { get; set; }

    public string Message { get; set; }
}

