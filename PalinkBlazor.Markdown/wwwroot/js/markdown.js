export var editor, markdownRef;

export async function renderEditor(theme, height, width) {
	await loadScript('_content/PalinkBlazor.Markdown/editor.md/lib/zepto.min.js');
	await loadScript('_content/PalinkBlazor.Markdown/editor.md/editormd.js');
	editor = editormd({
		id:"palink-editormd",
		width:width,
		height:height,
		theme:theme,
		editorTheme:theme === "default" ? "default" : "pastel-on-dark",
		previewTheme:theme,
		path:"_content/PalinkBlazor.Markdown/editor.md/lib/",
		codeFold:true,
		saveHTMLToTextarea:true,
		emoji:true,
		atLink:false,
		emailLink:false,
		toolbarIcons:function() {
			return ["bold", "del", "italic", "quote", "ucwords", "uppercase", "lowercase", "h1", "h2", "h3",
				"h4", "h5", "h6", "list-ul", "list-ol", "hr", "link", "image", "code", "preformatted-text",
				"code-block", "table", "datetime", "html-entities", "emoji", "watch", "preview",
				"fullscreen", "clear", "search", "theme", "||", "save"];
		},
		toolbarIconsClass:{
			save:"fa-check",
			theme:"fa fa-file-text"
		},
		toolbarIconTexts:{
			// 如果没有图标，则可以这样直接插入内容，可以是字符串或HTML标签
		
		},
		// 用于增加自定义工具栏的功能，可以直接插入HTML标签，不使用默认的元素创建图标
		toolbarCustomIcons:{
			// file:"<input type=\"file\" accept=\".md\" />",
			// faicon:"<i class=\"fa fa-star\" onclick=\"alert('faicon');\"></i>"
		
		},
		lang:{
			toolbar:{
				theme:"暗黑模式",
				save:"保存"
			}
		},
		toolbarHandlers:{
			save:function() {
				saveCallback();
			},
			theme:function() {
				if(editor.settings.editorTheme === "default") {
					editor.setTheme("dark");
					editor.setEditorTheme("pastel-on-dark");
					editor.setPreviewTheme("dark");
				} else {
					editor.setTheme("default");
					editor.setEditorTheme("default");
					editor.setPreviewTheme("default");
				}
			}
		},
		onload:function() {
			this.addKeyMap({
				"Ctrl-S":function() {
					saveCallback();
				}
			});
		}
	});
}

export async function renderMarkdown() {
	await loadScript('_content/PalinkBlazor.Markdown/editor.md/lib/zepto.min.js');
	await loadScript('_content/PalinkBlazor.Markdown/editor.md/lib/marked.min.js');
	await loadScript('_content/PalinkBlazor.Markdown/editor.md/lib/prettify.min.js');
	await loadScript('_content/PalinkBlazor.Markdown/editor.md/editormd.js');
	editormd.markdownToHTML("palink-editormd-view", {
		//htmlDecode      : true,       // 开启 HTML 标签解析，为了安全性，默认不开启
		htmlDecode:"style,script,iframe", // you can filter tags decode
		//toc             : false,
		tocm:true, // Using [TOCM]
		tocContainer:"#custom-toc-container", // 自定义 ToC 容器层
		//gfm             : false,
		//tocDropdown     : true,
		// markdownSourceCode : true, // 是否保留 Markdown 源码，即是否删除保存源码的 Textarea 标签
		emoji:true,
		taskList:true,
		tex:true // 默认不解析
	});
}

//dark,default
export function setTheme(theme) {
	editor.setTheme(theme);
}

//default,
export function setEditorTheme(theme) {
	editor.setEditorTheme(theme);
}

//dark,default
export function setPreviewTheme(theme) {
	editor.setPreviewTheme(theme);
}

export function getMarkdown() {
	return editor.getMarkdown();
}

export function getHtml() {
	return editor.getHTML();
}

export function saveCallback() {
	markdownRef.invokeMethodAsync('ShowCallback');
}

export function setObj(markdown) {
	return markdownRef = markdown;
}

async function loadScript(url) {
	const response = await window.fetch(url);
	const js = await response.text();
	eval(js);
}