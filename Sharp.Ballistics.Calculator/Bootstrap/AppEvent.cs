using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp.Ballistics.Calculator
{
    //stuff may be added to here in the future
    public class AppEvent
    {
        public string MessageType { get; set; }
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
    }
}
