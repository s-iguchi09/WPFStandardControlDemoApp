# WPFStandardControlDemoApp

Language: [English](#english) | [日本語](#japanese)

---

<a id="english"></a>
## Overview (English)

This is a demo application designed to help you explore the **configuration methods, visual styles, and behavioral outcomes** of standard WPF (Windows Presentation Foundation) controls through interactive usage.

The project is built on the latest **.NET 10.0** environment, providing a straightforward way to understand how each control functions as a UI element and interacts with code-behind events.

### Purpose of the App
* **Real-time Behavioral Testing**: Test the styles and behaviors of various controls, such as clicks, selections, and text input, all within a single interface.
* **Layout Panel Comparison**: Visually confirm the differences in positioning rules and resizing behaviors across various panel controls.
* **Code Reference**: Each control in the app includes a button to **view the actual source code (XAML/C#)**, making it easy to learn implementation details.

### List of Included Controls
The following controls are currently implemented in the demo:

#### Inputs
* **TextBox**
* **PasswordBox**
* **Button**
* **RepeatButton**
* **ToggleButton**
* **CheckBox**
* **RadioButton**
* **ComboBox**
* **DatePicker**
* **Slider**

#### Layout
* **Grid**
* **StackPanel**
* **WrapPanel**
* **DockPanel**
* **UniformGrid**
* **Viewbox**
* **ScrollViewer**

#### Display
* **TextBlock**
* **Label**
* **ProgressBar**
* **GroupBox**
* **Expander**

#### List
* **ListBox**
* **ListView**
* **TreeView**
* **DataGrid**

#### Graphics
* **Image**
* **Canvas**
* **InkCanvas**

#### Selectors
* **TabControl**
* **TabItem**

#### Menu
* **Menu**
* **ToolBar**

#### Overlays
* **ToolTip**
* **Popup**

#### Resizer
* **GridSplitter**

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
4. Interact with each control and use the **built-in code view buttons** to learn the implementation methods by referring to the actual source code.

---

<a id="japanese"></a>
## 概要 (Japanese)

WPF（Windows Presentation Foundation）の標準コントロールの設定方法や動作結果を、実際に動かしながら確認できるデモアプリです。

最新の **.NET 10.0** 環境に対応しており、各コントロールがどのようにUIとして機能し、コード（イベント）と連携するかを直感的に把握することができます。

### アプリの目的
* **動作の即時確認**: 各コントロールのスタイルや、クリック・選択・入力時の挙動を1つの画面でテストできます。
* **レイアウト・パネルの比較**: 多彩なパネル系コントロールによる配置ルールや、リサイズ時の挙動の違いを視覚的に確認できます。
* **実際のコードを確認**: 各コントロールには**実際のソースコード（XAML/C#）を確認できるボタン**が用意されており、実装方法をその場ですぐに学ぶことができます。

### 収録コントロール一覧
現在、以下のコントロールのデモが実装されています。

#### Inputs
* **TextBox**
* **PasswordBox**
* **Button**
* **RepeatButton**
* **ToggleButton**
* **CheckBox**
* **RadioButton**
* **ComboBox**
* **DatePicker**
* **Slider**

#### Layout
* **Grid**
* **StackPanel**
* **WrapPanel**
* **DockPanel**
* **UniformGrid**
* **Viewbox**
* **ScrollViewer**

#### Display
* **TextBlock**
* **Label**
* **ProgressBar**
* **GroupBox**
* **Expander**

#### List
* **ListBox**
* **ListView**
* **TreeView**
* **DataGrid**

#### Graphics
* **Image**
* **Canvas**
* **InkCanvas**

#### Selectors
* **TabControl**
* **TabItem**

#### Menu
* **Menu**
* **ToolBar**

#### Overlays
* **ToolTip**
* **Popup**

#### Resizer
* **GridSplitter**

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
4. 各コントロールを操作し、備え付けのボタンから実際のコードを参照して実装方法を学習してください。