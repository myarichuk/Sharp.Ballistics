using Caliburn.Micro;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class AboutViewModel : FunctionScreen
    {
        public override string IconFilename => "about.png";

        public override int Order => int.MaxValue;

        public AboutViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            DisplayName = "About Sharp.Ballistics.Calculator";
        }
    }
}
