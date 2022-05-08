namespace InteractiveHelper.Api.Middleware;

using InteractiveHelper.Common.Extensions;
using InteractiveHelper.Common.Responses.Errors;
using System.Text.Json;
using InteractiveHelper.Common.Exceptions;
using FluentValidation;

/// <summary>
/// Global exception catcher
/// </summary>
public class ExceptionsMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionsMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        ErrorResponse response = null;
        try
        {
            await next.Invoke(context);
        }
        catch (ValidationException e)
        {
            response = e.ToErrorResponse();
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (CommonException e)
        {
            response = e.ToErrorResponse();
            if (e.Code == 404)
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            else
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
        catch (Exception e)
        {
            response = e.ToErrorResponse();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        finally
        {
            if (!(response is null))
            {
                //context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                await context.Response.StartAsync();
                await context.Response.CompleteAsync();
            }
        }
    }
}
