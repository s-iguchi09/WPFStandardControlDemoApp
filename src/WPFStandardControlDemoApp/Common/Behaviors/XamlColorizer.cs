using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data; // 追加
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public static class XamlColorizer
    {
        // 入力：抽出されたXAML文字列
        public static readonly DependencyProperty SourceTextProperty =
            DependencyProperty.RegisterAttached("SourceText", typeof(string), typeof(XamlColorizer),
                new PropertyMetadata(null, OnSourceTextChanged));

        // 出力：色付けされたFlowDocument
        public static readonly DependencyProperty TargetDocumentProperty =
            DependencyProperty.RegisterAttached("TargetDocument", typeof(FlowDocument), typeof(XamlColorizer),
                new PropertyMetadata(null));

        public static void SetSourceText(DependencyObject obj, string value) => obj.SetValue(SourceTextProperty, value);
        public static string GetSourceText(DependencyObject obj) => (string)obj.GetValue(SourceTextProperty);

        public static void SetTargetDocument(DependencyObject obj, FlowDocument value) => obj.SetValue(TargetDocumentProperty, value);
        public static FlowDocument GetTargetDocument(DependencyObject obj) => (FlowDocument)obj.GetValue(TargetDocumentProperty);

        private static void OnSourceTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement fe && e.NewValue is string xamlText)
            {
                // 前回作成した XmlReader ベースのハイライトロジックを呼び出す
                var doc = XamlSyntaxHighlighter.Generate(xamlText);

                Binding widthBinding = new Binding("ActualWidth") { Source = fe };
                BindingOperations.SetBinding(doc, FlowDocument.PageWidthProperty, widthBinding);

                SetTargetDocument(fe, doc);
            }
        }

        public static class XamlSyntaxHighlighter
        {
            // Visual Studio ライトテーマ (XAMLエディタ) 配色定義
            private static readonly SolidColorBrush SymbolBrush = new SolidColorBrush(Color.FromRgb(0, 0, 255));      // 青: < > = { } ,
            private static readonly SolidColorBrush TagNameBrush = new SolidColorBrush(Color.FromRgb(163, 21, 21));   // 茶: 要素名 (Grid等), Binding等
            private static readonly SolidColorBrush AttrNameBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));    // 赤: 属性名 (x:Name等), Path, ElementName等
            private static readonly SolidColorBrush AttrValueBrush = new SolidColorBrush(Color.FromRgb(0, 0, 255));   // 青: 属性値, ElementNameの対象
            private static readonly SolidColorBrush CommentBrush = new SolidColorBrush(Color.FromRgb(0, 128, 0));     // 緑: コメント
            private static readonly SolidColorBrush TextBrush = Brushes.Black;                                        // 黒: コンテンツ・空白

            public static FlowDocument Generate(string xamlText)
            {
                var doc = new FlowDocument
                {
                    PagePadding = new Thickness(0),
                    ColumnWidth = double.PositiveInfinity
                };

                var paragraph = new Paragraph();
                doc.Blocks.Add(paragraph);

                if (string.IsNullOrWhiteSpace(xamlText)) return doc;

                try
                {
                    // 名前空間の解決 (カスタム接頭辞エラー回避)
                    NameTable nt = new NameTable();
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
                    nsmgr.AddNamespace(string.Empty, "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
                    nsmgr.AddNamespace("x", "http://schemas.microsoft.com/winfx/2006/xaml");
                    nsmgr.AddNamespace("d", "http://schemas.microsoft.com/expression/blend/2008");
                    nsmgr.AddNamespace("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");

                    var prefixMatches = Regex.Matches(xamlText, @"\b(\w+):[a-zA-Z]");
                    foreach (Match match in prefixMatches)
                    {
                        string p = match.Groups[1].Value;
                        if (nsmgr.LookupNamespace(p) == null) nsmgr.AddNamespace(p, $"http://dummy/{p}");
                    }

                    XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
                    var settings = new XmlReaderSettings { IgnoreWhitespace = true, ConformanceLevel = ConformanceLevel.Fragment };

                    using var reader = XmlReader.Create(new StringReader(xamlText), settings, context);

                    while (reader.Read())
                    {
                        if(paragraph.Inlines.Any())
                        {
                            paragraph.Inlines.Add(new LineBreak());
                        }

                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                AddIndent(paragraph, reader.Depth);
                                AddRun(paragraph, "<", SymbolBrush);
                                AddRun(paragraph, reader.Name, TagNameBrush);

                                if (reader.HasAttributes)
                                {
                                    while (reader.MoveToNextAttribute())
                                    {
                                        if (reader.Prefix == "xmlns" || reader.Name == "xmlns") continue;

                                        AddRun(paragraph, " ", TextBrush);
                                        AddRun(paragraph, reader.Name, AttrNameBrush);
                                        AddRun(paragraph, "=\"", SymbolBrush);

                                        // 属性値の解析
                                        ParseAttributeValue(paragraph, reader.Value);

                                        AddRun(paragraph, "\"", SymbolBrush);
                                    }
                                    reader.MoveToElement();
                                }

                                AddRun(paragraph, reader.IsEmptyElement ? " />" : ">", SymbolBrush);
                                break;

                            case XmlNodeType.EndElement:
                                AddIndent(paragraph, reader.Depth);
                                AddRun(paragraph, "</", SymbolBrush);
                                AddRun(paragraph, reader.Name, TagNameBrush);
                                AddRun(paragraph, ">", SymbolBrush);
                                break;

                            case XmlNodeType.Text:
                                if (!string.IsNullOrWhiteSpace(reader.Value))
                                {
                                    AddIndent(paragraph, reader.Depth);
                                    AddRun(paragraph, reader.Value.Trim(), TextBrush);
                                }
                                break;

                            case XmlNodeType.Comment:
                                AddIndent(paragraph, reader.Depth);
                                AddRun(paragraph, "", CommentBrush);
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    paragraph.Inlines.Clear();
                    AddRun(paragraph, $"[Error]: {ex.Message}", Brushes.Red);
                    paragraph.Inlines.Add(new LineBreak());
                    AddRun(paragraph, xamlText, TextBrush);
                }

                return doc;
            }

            private static void ParseAttributeValue(Paragraph p, string value)
            {
                if (value.StartsWith("{") && value.EndsWith("}"))
                {
                    var tokens = Regex.Split(value, @"([{} = ,])");

                    bool isNextMarkupName = false;
                    bool isNextValue = false;

                    foreach (var token in tokens)
                    {
                        if (string.IsNullOrEmpty(token)) continue;

                        if (string.IsNullOrWhiteSpace(token))
                        {
                            AddRun(p, token, TextBrush);
                            continue;
                        }

                        if (Regex.IsMatch(token, @"^[{} = ,]$"))
                        {
                            AddRun(p, token, SymbolBrush);
                            if (token == "{") isNextMarkupName = true;
                            if (token == "=") isNextValue = true;
                            if (token == ",") isNextValue = false;
                            continue;
                        }

                        if (isNextMarkupName)
                        {
                            // {Binding など -> 茶色
                            AddRun(p, token, TagNameBrush);
                            isNextMarkupName = false;
                        }
                        else if (isNextValue)
                        {
                            // = の直後 (ElementNameのターゲット等) -> 青色に変更
                            AddRun(p, token, AttrValueBrush);
                            isNextValue = false;
                        }
                        else
                        {
                            // プロパティ名やパス -> 赤色
                            AddRun(p, token, AttrNameBrush);
                        }
                    }
                }
                else
                {
                    // 通常の引用符内の値 -> 青色
                    AddRun(p, value, AttrValueBrush);
                }
            }

            private static void AddRun(Paragraph p, string text, Brush color)
            {
                p.Inlines.Add(new Run(text) { Foreground = color });
            }

            private static void AddIndent(Paragraph p, int depth)
            {
                if (depth > 0) p.Inlines.Add(new Run(new string(' ', depth * 4)));
            }
        }
    }
}