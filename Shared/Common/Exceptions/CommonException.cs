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

    public static void ThrowIf(Func<bool> predicate, string message, int code = -1)
    {
        if (predicate.Invoke())
            throw new CommonException(code, message);
    }

    public static void ThrowIfNull(object obj, string message, int code = -1)
    {
        if (obj is null)
            throw new CommonException(code, message);
    }
}
