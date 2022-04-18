using Microsoft.Extensions.DependencyInjection;

namespace PalinkBlazor.Markdown;

public static class MarkdownExtensions
{
    public static IServiceCollection AddMarkdown(this IServiceCollection service)
    {
        return service.AddScoped<JsInterop>();
    }
}