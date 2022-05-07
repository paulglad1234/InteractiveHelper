namespace InteractiveHelper.Common.Exceptions;

using System;

/// <summary>
/// Base exception 
/// </summary>
public class CommonException : Exception
{
    /// <summary>
    ///Error code
    /// </summary>
    public int Code { get; } = -1;

    /// <summary>
    /// Error name
    /// </summary>
    public string Name { get; } = string.Empty;

    #region Constructors

    public CommonException()
    {
    }

    public CommonException(string message) : base(message)
    {
    }

    public CommonException(Exception inner) : base(inner.Message, inner)
    {
    }

    public CommonException(string message, Exception inner) : base(message, inner)
    {
    }

    public CommonException(int code, string message) : base(message)
    {
        Code = code;
    }

    public CommonException(int code, string message, Exception inner) : base(message, inner)
    {
        Code = code;
    }

    #endregion

    public static void ThrowIf(Func<bool> predicate, string message)
    {
        if (predicate.Invoke())
            throw new CommonException(message);
    }
}
