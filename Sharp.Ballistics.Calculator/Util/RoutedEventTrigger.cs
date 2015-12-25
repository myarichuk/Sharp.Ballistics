using System;
using System.Windows;
using System.Windows.Interactivity;

namespace Sharp.Ballistics.Calculator
{
    //credit : http://stackoverflow.com/a/8471269
    public class RoutedEventTrigger : EventTriggerBase<DependencyObject>
    {
        public RoutedEvent RoutedEvent { get; set; }

        protected override void OnAttached()
        {
            var behavior = base.AssociatedObject as Behavior;
            var associatedElement = base.AssociatedObject as FrameworkElement;
            if (behavior != null)
            {
                associatedElement = ((IAttachedObject)behavior).AssociatedObject as FrameworkElement;
            }
            if (associatedElement == null)
            {
                throw new ArgumentException("Routed Event trigger can only be associated to framework elements");
            }
            if (RoutedEvent != null)
            { associatedElement.AddHandler(RoutedEvent, new RoutedEventHandler(this.OnRoutedEvent)); }
        }
        void OnRoutedEvent(object sender, RoutedEventArgs args)
        {
            base.OnEvent(args);
        }
        protected override string GetEventName() { return RoutedEvent.Name; }
    }
}
