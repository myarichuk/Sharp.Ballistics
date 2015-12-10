using Caliburn.Micro;

namespace Sharp.Ballistics.Calculator.ViewModels
{

    public class ShellViewModel : Conductor<FunctionScreen>
    {
        private readonly IWindowManager windowManager;

        public ShellViewModel(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
        }
    }
}