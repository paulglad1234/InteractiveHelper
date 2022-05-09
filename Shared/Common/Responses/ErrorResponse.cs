namespace InteractiveHelper.Common.Responses;

public class ErrorResponse
{
    public int ErrorCode { get; set; }

    public string Message { get; set; }
    public ErrorResponse InnerError { get; set; }
    public IEnumerable<ErrorResponseFieldInfo> FieldErrors { get; set; }
}

public class ErrorResponseFieldInfo
{
    public string FieldName { get; set; }

    public string Message { get; set; }
}

