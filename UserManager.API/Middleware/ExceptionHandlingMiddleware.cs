namespace UserManager.API.Middleware;

using Microsoft.EntityFrameworkCore;
using UserManager.API.Models;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DbUpdateException dbEx)
        {
            logger.LogError(dbEx, dbEx.Message);

            var errorMessage = dbEx.InnerException?.Message ?? dbEx.Message;

            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var response = new Response
            {
                Status = 400,
                Message = errorMessage,
                Data = default!
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;

            var response = new Response
            {
                Status = 500,
                Message = "An unexpected error occurred.",
                Data = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}

