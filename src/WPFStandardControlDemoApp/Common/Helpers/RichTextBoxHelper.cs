using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPFStandardControlDemoApp.Common.Helpers
{
    public static class RichTextBoxHelper
    {
        public static readonly DependencyProperty BoundDocumentProperty =
            DependencyProperty.RegisterAttached("BoundDocument", typeof(FlowDocument), typeof(RichTextBoxHelper),
                new PropertyMetadata(null, OnBoundDocumentChanged));

        public static FlowDocument GetBoundDocument(DependencyObject obj) => (FlowDocument)obj.GetValue(BoundDocumentProperty);
        public static void SetBoundDocument(DependencyObject obj, FlowDocument value) => obj.SetValue(BoundDocumentProperty, value);

        private static void OnBoundDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RichTextBox rtb)
            {
                rtb.Document = e.NewValue as FlowDocument ?? new FlowDocument();
            }
        }
    }
}
