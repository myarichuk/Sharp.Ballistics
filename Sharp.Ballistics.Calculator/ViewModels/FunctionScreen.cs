using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public abstract class FunctionScreen : Screen
    {
        public abstract string IconFilename { get; }       
        
        public abstract int Order { get; } 
    }
}
