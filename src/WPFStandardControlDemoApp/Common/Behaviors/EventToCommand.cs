using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPFStandardControlDemoApp.Common.Behaviors
{
    public static class EventToCommand
    {
        /// <summary>
        /// <para>Specifies the event name to bind to the command.</para>
        /// <para>コマンドにバインドするイベント名を指定します。</para>
        /// </summary>
        /// <remarks>
        /// <para>Use this property to bind any event (e.g., Click, MouseMove, KeyDown) to a command.</para>
        /// <para>任意のイベント（Click、MouseMove、KeyDown など）をコマンドにバインドするために使用します。</para>
        /// </remarks>
        /// <value>
        /// <para>The name of the event to bind.</para>
        /// <para>バインドするイベント名。</para>
        /// </value>
        /// <example>
        /// <para>&lt;Button behaviors:EventToCommand.EventName="Click" /&gt;</para>
        /// </example>
        public static readonly DependencyProperty EventNameProperty =
            DependencyProperty.RegisterAttached(
                "EventName",
                typeof(string),
                typeof(EventToCommand),
                new PropertyMetadata(null, OnEventNameChanged));

        public static void SetEventName(DependencyObject obj, string value)
            => obj.SetValue(EventNameProperty, value);

        public static string GetEventName(DependencyObject obj)
            => (string)obj.GetValue(EventNameProperty);

        /// <summary>
        /// <para>The command to execute when the event is raised.</para>
        /// <para>イベント発生時に実行されるコマンドを指定します。</para>
        /// </summary>
        /// <remarks>
        /// <para>The command will be executed when the specified event occurs.</para>
        /// <para>指定したイベントが発生したときにコマンドが実行されます。</para>
        /// </remarks>
        /// <value>
        /// <para>The ICommand instance to execute.</para>
        /// <para>実行される ICommand。</para>
        /// </value>
        /// <example>
        /// <para>&lt;Button behaviors:EventToCommand.Command="{Binding ClickCommand}" /&gt;</para>
        /// </example>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(EventToCommand),
                new PropertyMetadata(null));

        public static void SetCommand(DependencyObject obj, ICommand value)
            => obj.SetValue(CommandProperty, value);

        public static ICommand GetCommand(DependencyObject obj)
            => (ICommand)obj.GetValue(CommandProperty);

        /// <summary>
        /// <para>The parameter passed to the command when the event is raised.</para>
        /// <para>イベント発生時にコマンドへ渡されるパラメーターを指定します。</para>
        /// </summary>
        /// <remarks>
        /// <para>This value is ignored when PassEventArgs is true.</para>
        /// <para>PassEventArgs が true の場合、この値は無視されます。</para>
        /// </remarks>
        /// <value>
        /// <para>The object passed to the command.</para>
        /// <para>コマンドに渡されるオブジェクト。</para>
        /// </value>
        /// <example>
        /// <para>&lt;Button behaviors:EventToCommand.CommandParameter="123" /&gt;</para>
        /// </example>
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(EventToCommand),
                new PropertyMetadata(null));

        public static void SetCommandParameter(DependencyObject obj, object value)
            => obj.SetValue(CommandParameterProperty, value);

        public static object GetCommandParameter(DependencyObject obj)
            => obj.GetValue(CommandParameterProperty);

        /// <summary>
        /// <para>If true, the EventArgs instance will be passed to the command instead of CommandParameter.</para>
        /// <para>true の場合、CommandParameter の代わりに EventArgs がコマンドへ渡されます。</para>
        /// </summary>
        /// <remarks>
        /// <para>Useful when you need event-specific data such as MouseEventArgs or KeyEventArgs.</para>
        /// <para>MouseEventArgs や KeyEventArgs など、イベント固有のデータが必要な場合に便利です。</para>
        /// </remarks>
        /// <value>
        /// <para>True to pass EventArgs; otherwise false.</para>
        /// <para>EventArgs を渡す場合は true、それ以外は false。</para>
        /// </value>
        /// <example>
        /// <para>&lt;Button behaviors:EventToCommand.PassEventArgs="True" /&gt;</para>
        /// </example>
        public static readonly DependencyProperty PassEventArgsProperty =
            DependencyProperty.RegisterAttached(
                "PassEventArgs",
                typeof(bool),
                typeof(EventToCommand),
                new PropertyMetadata(false));

        public static void SetPassEventArgs(DependencyObject obj, bool value)
            => obj.SetValue(PassEventArgsProperty, value);

        public static bool GetPassEventArgs(DependencyObject obj)
            => (bool)obj.GetValue(PassEventArgsProperty);

        private static readonly DependencyProperty HandlerProperty =
            DependencyProperty.RegisterAttached(
                "Handler",
                typeof(Delegate),
                typeof(EventToCommand),
                new PropertyMetadata(null));

        private static void SetHandler(DependencyObject obj, Delegate? value)
            => obj.SetValue(HandlerProperty, value);

        private static Delegate GetHandler(DependencyObject obj)
            => (Delegate)obj.GetValue(HandlerProperty);

        private static void OnEventNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element)
                return;

            if (e.OldValue is string oldName && !string.IsNullOrWhiteSpace(oldName))
            {
                var oldEvent = element.GetType().GetEvent(oldName);
                var oldHandler = GetHandler(element);

                if (oldEvent != null && oldHandler != null)
                    oldEvent.RemoveEventHandler(element, oldHandler);

                SetHandler(element, null);
            }

            if (e.NewValue is string newName && !string.IsNullOrWhiteSpace(newName))
            {
                var eventInfo = element.GetType().GetEvent(newName);
                if (eventInfo == null)
                    return;

                var method = typeof(EventToCommand).GetMethod(
                    nameof(OnEventRaised),
                    BindingFlags.Static | BindingFlags.NonPublic);

                if (method == null)
                    return;

                var handler = Delegate.CreateDelegate(eventInfo.EventHandlerType!, method);
                eventInfo.AddEventHandler(element, handler);
                SetHandler(element, handler);
            }
        }

        private static void OnEventRaised(object sender, EventArgs e)
        {
            if (sender is not DependencyObject d)
                return;

            var command = GetCommand(d);
            if (command == null)
                return;

            object parameter =
                GetPassEventArgs(d)
                ? e
                : GetCommandParameter(d);

            if (command.CanExecute(parameter))
                command.Execute(parameter);
        }
    }

}
