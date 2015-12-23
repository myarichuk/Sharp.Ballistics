using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class ScopesViewModel : FunctionScreen
    {
        public override int Order
        {
            get
            {
                return 3;
            }
        }

        public override string IconFilename => "scope.png";

        public ScopesViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }
    }
}
