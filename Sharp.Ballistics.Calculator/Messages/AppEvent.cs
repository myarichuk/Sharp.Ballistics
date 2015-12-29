using System.Collections.Generic;

namespace Sharp.Ballistics.Calculator
{
    //stuff may be added to here in the future
    public class AppEvent
    {
        public string Type { get; set; }
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
    }
}
