using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public static class InkCanvasBehavior
    {
        // ペンの太さを同期
        public static readonly DependencyProperty PenSizeProperty =
            DependencyProperty.RegisterAttached("PenSize", typeof(double), typeof(InkCanvasBehavior), new PropertyMetadata(2.0, OnPenAttributeChanged));

        public static void SetPenSize(DependencyObject element, double value) => element.SetValue(PenSizeProperty, value);
        public static double GetPenSize(DependencyObject element) => (double)element.GetValue(PenSizeProperty);

        // ペンの色を同期
        public static readonly DependencyProperty PenColorProperty =
            DependencyProperty.RegisterAttached("PenColor", typeof(Color), typeof(InkCanvasBehavior), new PropertyMetadata(Colors.Black, OnPenAttributeChanged));

        public static void SetPenColor(DependencyObject element, Color value) => element.SetValue(PenColorProperty, value);
        public static Color GetPenColor(DependencyObject element) => (Color)element.GetValue(PenColorProperty);

        private static void OnPenAttributeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is InkCanvas inkCanvas)
            {
                // 値が変更されたら、内部の DrawingAttributes を直接更新する
                inkCanvas.DefaultDrawingAttributes.Width = GetPenSize(inkCanvas);
                inkCanvas.DefaultDrawingAttributes.Height = GetPenSize(inkCanvas);
                inkCanvas.DefaultDrawingAttributes.Color = GetPenColor(inkCanvas);
            }
        }
    }
}