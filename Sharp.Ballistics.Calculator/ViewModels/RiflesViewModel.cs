using Caliburn.Micro;
using Sharp.Ballistics.Abstractions;
using Sharp.Ballistics.Calculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class RiflesViewModel : FunctionScreen
    {
        private readonly RiflesModel riflesModel;

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

        public RiflesViewModel(RiflesModel riflesModel,IEventAggregator eventAggregator) : base(eventAggregator)
        {
            DisplayName = "Rifles";
            this.riflesModel = riflesModel;
            
        }

        public IEnumerable<IRifle> Rifles => riflesModel.All();
    }
}
