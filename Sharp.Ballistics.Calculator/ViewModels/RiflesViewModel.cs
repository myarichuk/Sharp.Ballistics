using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class RiflesViewModel : FunctionScreen
    {
        public override int Order
        {
            get
            {
                return 2;
            }
        }

        public override string IconFilename => "rifle.png";

        public bool IsBusy { get; set; }

        public bool BusyText { get; set; }

        public RiflesViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }
    }
}
