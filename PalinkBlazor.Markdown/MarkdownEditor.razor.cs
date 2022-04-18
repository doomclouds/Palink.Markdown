using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PalinkBlazor.Markdown
{
    public partial class MarkdownEditor : IDisposable
    {
        public const int MinHeight = 200;
        public const double MinWidth = 0.8;

        [Parameter]
        public MarkdownTheme Theme { get; set; }

        [Parameter]
        public string? Markdown { get; set; }

        [Parameter]
        public EventCallback<string?> MarkdownChanged { get; set; }

        [Parameter]
        public string? Html { get; set; }

        [Parameter]
        public EventCallback<string?> HtmlChanged { get; set; }

        [Parameter]
        public int Height { get; set; } = MinHeight;

        [Parameter]
        public double Width { get; set; } = MinWidth;

        [NotNull]
        [Inject]
        public JsInterop? JsInterop { get; set; }

        [Parameter]
        public EventCallback SaveMarkdown { get; set; }

        private DotNetObjectReference<MarkdownEditor>? _objRef;

        /// <summary>
        /// 暴漏给JS执行
        /// </summary>
        [JSInvokable]
        public async void ShowCallback()
        {
            if (SaveMarkdown.HasDelegate)
            {
                Markdown = await JsInterop.GetMarkdown();
                if (MarkdownChanged.HasDelegate)
                    await MarkdownChanged.InvokeAsync(Markdown);

                Html = await JsInterop.GetHtml();
                if (HtmlChanged.HasDelegate)
                    await HtmlChanged.InvokeAsync(Html);

                await SaveMarkdown.InvokeAsync();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInterop.RenderEditor(Theme, Height, Width);

                _objRef = DotNetObjectReference.Create(this);
                await JsInterop.SetDotnetObject(_objRef);
            }
        }

        public void Dispose()
        {
            _objRef?.Dispose();
        }
    }
}