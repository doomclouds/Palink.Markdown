using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace PalinkBlazor.Markdown
{
    public partial class MarkdownHtmlRender
    {
        [NotNull]
        [Inject]
        public JsInterop? JsInterop { get; set; }

        [Parameter]
        public string? Markdown { get; set; }

        /// <summary>
        /// 请填写1024px等html width属性的值
        /// </summary>
        [Parameter]
        public string? MinWidth { get; set; } = "1024px";

        [Parameter]
        public EventCallback<string?> MarkdownChanged { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JsInterop.RenderMarkdown();
            }
        }
    }
}