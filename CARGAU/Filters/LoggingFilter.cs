using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class LoggingFilter : Attribute, IActionFilter
{
    private readonly string _message;
    private readonly ILogger<LoggingFilter> _logger; // указываем дженерик тип для удобного использования логгера 
    // (чтобы в логах по категории понять откуда сам лог)

    public LoggingFilter(ILogger<LoggingFilter> logger, string message) =>
        (_logger, _message) = (logger, message);

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation("Лог до вызова: {Message}", _message);
        //context.
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation("Лог после вызова: {Message}", _message);
        //context.HttpContext.Response.Headers.Add("Filter-Logging", "Logging");
    }
}
