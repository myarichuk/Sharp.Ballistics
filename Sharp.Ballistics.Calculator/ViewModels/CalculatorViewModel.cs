using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class CalculatorViewModel : FunctionScreen
    {
        public override int Order => 1;

        public override string IconFilename => "calc.png";

        public CalculatorViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

        }
    }
}
