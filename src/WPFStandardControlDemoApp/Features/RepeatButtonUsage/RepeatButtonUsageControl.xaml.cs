using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFStandardControlDemoApp.Features.RepeatButtonUsage
{
    /// <summary>
    /// RepeatButtonUsageControl.xaml の相互作用ロジック
    /// </summary>
    public partial class RepeatButtonUsageControl : UserControl
    {
        public RepeatButtonUsageControl()
        {
            InitializeComponent();
        }

        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            ClickDateTime.Text = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff");
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !new System.Text.RegularExpressions.Regex("[0-9]").IsMatch(e.Text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                if (!new System.Text.RegularExpressions.Regex("[0-9]").IsMatch(e.DataObject.GetData(DataFormats.Text).ToString()))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
