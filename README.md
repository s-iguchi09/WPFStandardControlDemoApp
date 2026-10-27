# WPFStandardControlDemoApp

Language: [English](#english) | [日本語](#japanese)

---

<a id="english"></a>
## Overview (English)

This is a demo application designed to help you explore the **configuration methods, visual styles, and behavioral outcomes** of standard WPF (Windows Presentation Foundation) controls through interactive usage.

The project is built on the latest **.NET 10.0** environment, providing a straightforward way to understand how each control functions as a UI element and interacts with code-behind events.

Detailed write-ups for every control (measured behavior, pitfalls, and XAML) are on the [project site](https://s-iguchi09.github.io/apps/wpf-standard-control-demo.html). Each control in the list below links to its page.

### Purpose of the App
* **Real-time Behavioral Testing**: Test the styles and behaviors of various controls, such as clicks, selections, and text input, all within a single interface.
* **Layout Panel Comparison**: Visually confirm the differences in positioning rules and resizing behaviors across various panel controls.
* **Code Reference**: Each section of a control page has a **Show Code** link that shows **the XAML of that section**. The XAML is taken from the page's own source, embedded in the executable at build time, so it is the markup that is actually running.

### List of Included Controls
The following controls are currently implemented in the demo:

#### Inputs
* [**TextBox**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/textbox.html)
* [**PasswordBox**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/passwordbox.html)
* [**Button**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/button.html)
* [**RepeatButton**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/repeatbutton.html)
* [**ToggleButton**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/togglebutton.html)
* [**CheckBox**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/checkbox.html)
* [**RadioButton**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/radiobutton.html)
* [**ComboBox**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/combobox.html)
* [**DatePicker**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/datepicker.html)
* [**Slider**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/slider.html)

#### Layout
* [**Grid**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/grid.html)
* [**StackPanel**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/stackpanel.html)
* [**WrapPanel**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/wrappanel.html)
* [**DockPanel**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/dockpanel.html)
* [**UniformGrid**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/uniformgrid.html)
* [**Viewbox**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/viewbox.html)
* [**ScrollViewer**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/scrollviewer.html)

#### Display
* [**TextBlock**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/textblock.html)
* [**Label**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/label.html)
* [**ProgressBar**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/progressbar.html)
* [**GroupBox**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/groupbox.html)
* [**Expander**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/expander.html)

#### List
* [**ListBox**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/listbox.html)
* [**ListView**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/listview.html)
* [**TreeView**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/treeview.html)
* [**DataGrid**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/datagrid.html)

#### Graphics
* [**Image**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/image.html)
* [**Canvas**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/canvas.html)
* [**InkCanvas**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/inkcanvas.html)

#### Selectors
* [**TabControl**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/tabcontrol.html)
* [**TabItem**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/tabitem.html)

#### Menu
* [**Menu**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/menu.html)
* [**ToolBar**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/toolbar.html)

#### Overlays
* [**ToolTip**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/tooltip.html)
* [**Popup**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/popup.html)

#### Resizer
* [**GridSplitter**](https://s-iguchi09.github.io/apps/wpf-standard-control-demo/gridsplitter.html)

### How to Run
#### 1. Quick Start (No Build Required)
If you just want to see the app in action, you can run the pre-built executable:
* Go to the **[Releases](https://github.com/s-iguchi09/WPFStandardControlDemoApp/releases)** page.
* Download the latest ZIP file.
* Extract and run the `.exe` file.

#### 2. For Developers (Build from Source)
1. Clone this repository.
2. Open the solution in **Visual Studio 2026**.
3. Build and run the project (F5).
4. Interact with each control and use the **Show Code** links to read the XAML of each section.

---

<a id="japanese"></a>
## 概要 (Japanese)

WPF（Windows Presentation Foundation）の標準コントロールの設定方法や動作結果を、実際に動かしながら確認できるデモアプリです。

最新の **.NET 10.0** 環境に対応しており、各コントロールがどのようにUIとして機能し、コード（イベント）と連携するかを直感的に把握することができます。

各コントロールの実測結果・注意点・XAML の解説は[サイトのページ](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo.html)にまとめています。下の一覧の各コントロールから、それぞれのページへ移動できます。

### アプリの目的
* **動作の即時確認**: 各コントロールのスタイルや、クリック・選択・入力時の挙動を1つの画面でテストできます。
* **レイアウト・パネルの比較**: 多彩なパネル系コントロールによる配置ルールや、リサイズ時の挙動の違いを視覚的に確認できます。
* **実際のコードを確認**: 各コントロールのページの節ごとに **Show Code** のリンクがあり、**その節の XAML** を表示します。XAML はビルド時に実行ファイルへ埋め込んだページ自身のソースから取り出すため、実際に動いているマークアップです。

### 収録コントロール一覧
現在、以下のコントロールのデモが実装されています。

#### Inputs
* [**TextBox**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/textbox.html)
* [**PasswordBox**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/passwordbox.html)
* [**Button**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/button.html)
* [**RepeatButton**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/repeatbutton.html)
* [**ToggleButton**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/togglebutton.html)
* [**CheckBox**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/checkbox.html)
* [**RadioButton**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/radiobutton.html)
* [**ComboBox**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/combobox.html)
* [**DatePicker**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/datepicker.html)
* [**Slider**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/slider.html)

#### Layout
* [**Grid**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/grid.html)
* [**StackPanel**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/stackpanel.html)
* [**WrapPanel**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/wrappanel.html)
* [**DockPanel**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/dockpanel.html)
* [**UniformGrid**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/uniformgrid.html)
* [**Viewbox**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/viewbox.html)
* [**ScrollViewer**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/scrollviewer.html)

#### Display
* [**TextBlock**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/textblock.html)
* [**Label**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/label.html)
* [**ProgressBar**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/progressbar.html)
* [**GroupBox**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/groupbox.html)
* [**Expander**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/expander.html)

#### List
* [**ListBox**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/listbox.html)
* [**ListView**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/listview.html)
* [**TreeView**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/treeview.html)
* [**DataGrid**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/datagrid.html)

#### Graphics
* [**Image**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/image.html)
* [**Canvas**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/canvas.html)
* [**InkCanvas**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/inkcanvas.html)

#### Selectors
* [**TabControl**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/tabcontrol.html)
* [**TabItem**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/tabitem.html)

#### Menu
* [**Menu**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/menu.html)
* [**ToolBar**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/toolbar.html)

#### Overlays
* [**ToolTip**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/tooltip.html)
* [**Popup**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/popup.html)

#### Resizer
* [**GridSplitter**](https://s-iguchi09.github.io/ja/apps/wpf-standard-control-demo/gridsplitter.html)

### 実行・使用方法
#### 1. すぐに動作を確認する場合（ビルド不要）
ビルド環境がない場合でも、公開済みのバイナリで動作を確認できます。
* **[Releases](https://github.com/s-iguchi09/WPFStandardControlDemoApp/releases)** ページへアクセスします。
* 最新のZIPファイルをダウンロードします。
* 展開（解凍）し、中にある `.exe` ファイルを実行してください。

#### 2. 開発・ソースコードを確認する場合
1. リポジトリをクローンします。
2. **Visual Studio 2026** でソリューションを開きます。
3. プロジェクトをビルドし、実行（F5）します。
4. 各コントロールを操作し、**Show Code** のリンクから各節の XAML を参照してください。