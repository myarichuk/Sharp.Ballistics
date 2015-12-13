using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator.ViewModels
{
    public class ConfigurationViewModel : FunctionScreen
    {
        public override int Order
        {
            get
            {
                return int.MaxValue;
            }
        }

        public override string IconFilename
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
