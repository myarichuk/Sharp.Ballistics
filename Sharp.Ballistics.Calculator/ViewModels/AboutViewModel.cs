using Caliburn.Micro;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class AboutViewModel : FunctionScreen
    {
        public override string IconFilename => "about.png";

        public override int Order => int.MaxValue;

        public AboutViewModel(IEventAggregator eventsAggregator) : base(eventsAggregator)
        {
            DisplayName = "About Sharp.Ballistics.Calculator";
        }
    }
}
