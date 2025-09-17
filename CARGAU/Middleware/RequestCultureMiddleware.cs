using System.Globalization;

namespace API.Middleware;

public class RequestCultureMiddleware
{
    private readonly RequestDelegate _next;

    public RequestCultureMiddleware(RequestDelegate next) // через DI можно внедрить что угодно
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var cultureQuery = context.Request.Query["culture"];
        if (!string.IsNullOrWhiteSpace(cultureQuery))
        {
            var culture = new CultureInfo(cultureQuery!);

            CultureInfo.CurrentCulture = culture; // влияет на форматирование чисел, дат, валют
            CultureInfo.CurrentUICulture = culture; // влияет на язык ресурсов (какие переводы строк использовать)
        }

        // Call the next delegate/middleware in the pipeline.
        await _next(context);
    }
}

public static class RequestCultureMiddlewareExtensions
{
	public static IApplicationBuilder RequestCulture(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<RequestCultureMiddleware>();
	}
}