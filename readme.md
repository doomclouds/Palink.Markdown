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

暗黑模式

![Snipaste_2022-04-18_18-52-55](https://s2.loli.net/2022/04/18/8m9pV5ORDg63ElY.png)

明亮模式

![Snipaste_2022-04-18_18-53-06](https://s2.loli.net/2022/04/18/MRgJ4W7fF89j1a5.png)

Html渲染效果

![Snipaste_2022-04-18_18-53-15](https://s2.loli.net/2022/04/18/aiMm8c9rGQn7pwU.png)
