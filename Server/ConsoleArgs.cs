using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    public class ConsoleArgs: EventArgs
    {
        public string currentContent { set; get; }
    }
}
