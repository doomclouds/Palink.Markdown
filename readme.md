editor.md的Blazor封装，包含Markdown编辑器和Markdown Html渲染器

启用Markdown

```c#
builder.Services.AddMarkdown();
```

Markdown编辑器与Markdown渲染器

```c#
@inherits LayoutComponentBase
@inject IJSRuntime _js;
<div>
    <h1>Markdown</h1>
    <MarkdownEditor @bind-Html="@Html" @bind-Markdown="@Markdown"
                    Height="700" SaveMarkdown="Save" Theme="@MarkdownTheme.DarkTheme"
                    Width="0.8">
    </MarkdownEditor>
    <MarkdownHtmlRender @bind-Markdown="Markdown" MinWidth="1024px"></MarkdownHtmlRender>
    <div>
        @Body
    </div>
</div>

@code {
    public string? Markdown { get; set; }
    public string? Html { get; set; }

    private async void Save() {
        await _js.InvokeVoidAsync("alert", "\n💥💢好像漏了点什么吧💢💥");
    }

    protected override void OnInitialized() {
        var txt = File.ReadAllText(@"C:\Users\Palink\Desktop\markdown.txt");
        Markdown = txt;
    }

}
```

编辑器效果

