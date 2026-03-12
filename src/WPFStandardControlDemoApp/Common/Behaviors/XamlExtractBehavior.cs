using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Xml.Linq;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public class XamlExtractBehavior : DependencyObject
    {
        public static readonly DependencyProperty TargetNamesProperty =
            DependencyProperty.RegisterAttached(
                "TargetNames",
                typeof(string),
                typeof(XamlExtractBehavior),
                new PropertyMetadata(null, OnTargetNamesChanged));

        public static void SetTargetNames(DependencyObject obj, string value)
            => obj.SetValue(TargetNamesProperty, value);

        public static string GetTargetNames(DependencyObject obj)
            => (string)obj.GetValue(TargetNamesProperty);

        public static readonly DependencyProperty RawXamlFileNameProperty =
            DependencyProperty.RegisterAttached(
                "RawXamlFileName",
                typeof(string),
                typeof(XamlExtractBehavior),
                new PropertyMetadata(null));

        public static void SetRawXamlFileName(DependencyObject obj, string value)
            => obj.SetValue(RawXamlFileNameProperty, value);

        public static string GetRawXamlFileName(DependencyObject obj)
            => (string)obj.GetValue(RawXamlFileNameProperty);

        public static readonly DependencyProperty ExtractedXamlProperty =
            DependencyProperty.RegisterAttached(
                "ExtractedXaml",
                typeof(string),
                typeof(XamlExtractBehavior),
                new PropertyMetadata(null));

        public static void SetExtractedXaml(DependencyObject obj, string value)
            => obj.SetValue(ExtractedXamlProperty, value);

        public static string GetExtractedXaml(DependencyObject obj)
            => (string)obj.GetValue(ExtractedXamlProperty);

        private static void OnTargetNamesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement fe) return;

            WeakEventManager<FrameworkElement, RoutedEventArgs>.RemoveHandler(fe, nameof(FrameworkElement.Loaded), OnLoaded);

            if (e.NewValue is string names && !string.IsNullOrEmpty(names))
            {
                WeakEventManager<FrameworkElement, RoutedEventArgs>.AddHandler(fe, nameof(FrameworkElement.Loaded), OnLoaded);
            }
        }

        private static void OnLoaded(object? sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement fe) return;

            WeakEventManager<FrameworkElement, RoutedEventArgs>.RemoveHandler(fe, nameof(FrameworkElement.Loaded), OnLoaded);

            try
            {
                string rawFile = GetRawXamlFileName(fe);

                if (string.IsNullOrEmpty(rawFile))
                    throw new InvalidOperationException("RawXamlFileName が指定されていません。");

                string xamlText = LoadRawXaml(rawFile);

                var xdoc = XDocument.Parse(xamlText);
                XNamespace xNs = "http://schemas.microsoft.com/winfx/2006/xaml";

                var names = GetTargetNames(fe).Split(',')
                                       .Select(n => n.Trim())
                                       .Where(n => !string.IsNullOrEmpty(n));

                var extracted = names.Select(name =>
                {
                    var node = xdoc.Descendants()
                                   .FirstOrDefault(e => (string?)e.Attribute(xNs + "Name") == name);

                    if (node == null)
                        throw new InvalidOperationException($"x:Name='{name}' が XAML 内に見つかりません。");

                    var xml = node.ToString();

                    return Regex.Replace(xml, @"\s+xmlns(:\w+)?=""[^""]+""", "");

                });

                SetExtractedXaml(fe, string.Join("\n", extracted));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("XAML 抽出中にエラーが発生しました。", ex);
            }
        }

        private static string LoadRawXaml(string fileName)
        {
            var asm = Assembly.GetExecutingAssembly();
            var resourceName = asm.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
                throw new InvalidOperationException($"{fileName} がアセンブリ内に見つかりません。");

            using var stream = asm.GetManifestResourceStream(resourceName);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
}
