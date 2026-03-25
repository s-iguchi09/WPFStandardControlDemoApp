using System.Windows.Controls;
using System.Windows.Input;

namespace WPFStandardControlDemoApp.Common.Commands
{
    public class InkCanvasStrokesClearCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => parameter is InkCanvas;

        public void Execute(object? parameter)
        {
            if (parameter is not InkCanvas inkCanvas)
            {
                return;
            }

            inkCanvas.Strokes.Clear();
        }
    }
}
