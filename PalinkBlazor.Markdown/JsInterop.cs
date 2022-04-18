using Microsoft.JSInterop;

namespace PalinkBlazor.Markdown;

// This class provides an example of how JavaScript functionality can be wrapped
// in a .NET class for easy consumption. The associated JavaScript module is
// loaded on demand when first needed.
//
// This class can be registered as scoped DI service and then injected into Blazor
// components for use.
public class JsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public JsInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>("import",
                "./_content/PalinkBlazor.Markdown/js/markdown.js").AsTask());
    }

    public async ValueTask<string> GetMarkdown()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string>("getMarkdown");
    }

    public async ValueTask<string> GetHtml()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string>("getHtml");
    }

    public async ValueTask RenderEditor(string? theme, int height, double width)
    {
        if (width is > 1 or < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(width), "宽度是百分比，值在0-1之间");
        }

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("renderEditor",
            theme == "dark" ? MarkdownTheme.DarkTheme
                : MarkdownTheme.LightTheme, height, $"{(int)(width * 100)}%");
    }

    public async ValueTask SetDotnetObject(DotNetObjectReference<MarkdownEditor> objRef)
    {
        var module = await _moduleTask.Value;
        await module.InvokeAsync<object>("setObj", objRef);
    }

    public async ValueTask RenderMarkdown()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("renderMarkdown");
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}