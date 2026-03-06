using System.Windows;

namespace WPFStandardControlDemoApp.Common.Behaviors.WeakEventManagers
{
    internal class DataObjectPastingWeakEventManager : WeakEventManager
    {
        private DataObjectPastingWeakEventManager() { }

        public static void AddHandler(DependencyObject source, EventHandler handler)
        {
            CurrentManager.ProtectedAddHandler(source, handler);
        }

        public static void RemoveHandler(DependencyObject source, EventHandler handler)
        {
            CurrentManager.ProtectedRemoveHandler(source, handler);
        }

        private static DataObjectPastingWeakEventManager CurrentManager
        {
            get
            {
                var managerType = typeof(DataObjectPastingWeakEventManager);
                var manager = (DataObjectPastingWeakEventManager)GetCurrentManager(managerType);
                if (manager == null)
                {
                    manager = new DataObjectPastingWeakEventManager();
                    SetCurrentManager(managerType, manager);
                }
                return manager;
            }
        }

        protected override void StartListening(object source)
        {
            if (source is DependencyObject target)
            {
                DataObject.AddPastingHandler(target, OnPastingInternal);
            }
        }

        protected override void StopListening(object source)
        {
            if (source is DependencyObject target)
            {
                DataObject.RemovePastingHandler(target, OnPastingInternal);
            }
        }

        private void OnPastingInternal(object sender, EventArgs e)
        {
            DeliverEvent(sender, e);
        }
    }
}
