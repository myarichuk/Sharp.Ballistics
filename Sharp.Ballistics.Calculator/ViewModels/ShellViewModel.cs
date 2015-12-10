using Caliburn.Micro;

namespace Sharp.Ballistics.Calculator.ViewModels
{

    public class ShellViewModel : Conductor<FunctionScreen>
    {
        private readonly IWindowManager windowManager;
        private FunctionScreen currentFunction;
        public ShellViewModel(IWindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        public FunctionScreen CurrentFunction
        {
            get
            {
                return currentFunction;
            }
            set
            {
                currentFunction = value;
                NotifyOfPropertyChange(() => CurrentFunction);
            }
        }
    }
}