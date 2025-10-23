using ByteTech.Application.Common;
using ByteTech.Application.Exceptions;
using FluentValidation;

namespace ByteTech.Presentation.Middlewares;

public class HandleExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await OnHandleExceptionAsync(ex, context);
        }
    }

    public async Task OnHandleExceptionAsync(Exception ex, HttpContext context)
    {
        var response = new BaseResponse
        {
            Success = false,
            Message = ex.Message,
            StatusCode = ex switch
            {
                NotFoundException => System.Net.HttpStatusCode.NotFound,
                UnauthorizedException => System.Net.HttpStatusCode.Unauthorized,
                ValidationException => System.Net.HttpStatusCode.UnprocessableContent,
                AppException or ArgumentOutOfRangeException or ArgumentException => System.Net.HttpStatusCode.BadRequest,
                _ => System.Net.HttpStatusCode.InternalServerError
            },
        };

        await context.Response.WriteAsJsonAsync(response);
    }
}