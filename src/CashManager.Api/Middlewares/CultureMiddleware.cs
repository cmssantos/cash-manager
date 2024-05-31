using System.Globalization;

namespace CashManager.Api.Middlewares;

public class CultureMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var cultureHeader = context.Request.Headers.AcceptLanguage.ToString();
        var culture = cultureHeader?.Split(',')
                                    .FirstOrDefault()?.Split(';')
                                    .FirstOrDefault();

        if (!string.IsNullOrEmpty(culture))
        {
            try
            {
                var cultureInfo = new CultureInfo(culture);
                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
            }
            catch (CultureNotFoundException)
            {
                var defaultCulture = new CultureInfo("en-US");
                CultureInfo.CurrentCulture = defaultCulture;
                CultureInfo.CurrentUICulture = defaultCulture;
            }
        }
        else
        {
            var defaultCulture = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = defaultCulture;
            CultureInfo.CurrentUICulture = defaultCulture;
        }

        await _next(context);
    }
}
