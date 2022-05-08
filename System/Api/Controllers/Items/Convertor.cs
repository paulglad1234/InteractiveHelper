using InteractiveHelper.Common.Exceptions;

namespace InteractiveHelper.Api.Controllers.Items;

/// <summary>
/// Contains methods to convert one object to another
/// </summary>
public static class Convertor
{
    /// <summary>
    /// Write contents of a file to byte array
    /// </summary>
    /// <param name="formFile">File</param>
    /// <returns>Byte array with file content</returns>
    /// <exception cref="CommonException">Something went wrong with the file</exception>
    public static byte[] IFormFileToByteArray(this IFormFile formFile)
    {
        using var stream = new MemoryStream();
        if (formFile == null)
            throw new CommonException("No file provided");

        if (formFile.Length > 2048)
            throw new CommonException("The file was too large");

        formFile.CopyTo(stream);
        return stream.ToArray();
    }
}
