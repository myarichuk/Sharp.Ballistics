using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class AmmoViewModel : FunctionScreen
    {
        public override string IconFilename => "ammo.png";

        public override int Order
        {
            get
            {
                return 4;
            }
        }
    }
}
