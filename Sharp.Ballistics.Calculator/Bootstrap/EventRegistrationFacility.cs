using Caliburn.Micro;
using Castle.MicroKernel.Facilities;

namespace Sharp.Ballistics.Calculator.Bootstrap
{
    class EventRegistrationFacility : AbstractFacility
    {
        private IEventAggregator _eventAggregator;

        protected override void Init()
        {
            Kernel.ComponentCreated += ComponentCreated;
            Kernel.ComponentDestroyed += ComponentDestroyed;
        }

        void ComponentCreated(Castle.Core.ComponentModel model, object instance)
        {
            if (!(instance is IHandle)) return;
            if (_eventAggregator == null) _eventAggregator = Kernel.Resolve<IEventAggregator>();
            _eventAggregator.Subscribe(instance);
        }

        void ComponentDestroyed(Castle.Core.ComponentModel model, object instance)
        {
            if (!(instance is IHandle)) return;
            if (_eventAggregator == null) return;
            _eventAggregator.Unsubscribe(instance);
        }

    }
}
