using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace Server
{
    public class Console
    {
        Timer timer;
        Process proc;
        double interv = 1000;
        public event EventHandler<ConsoleArgs> newString;

        string buffer;
        public Console(Process _proc)
        {
            proc = _proc;
            timer = new Timer(interv);
            timer.Elapsed += timer_Elapsed;
            timer.Enabled = true;
            timer.Start();
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            buffer = proc.StandardOutput.ReadToEnd();
            if (newString != null)
                newString(null, new ConsoleArgs() { 
                    currentContent = buffer
                });

        }
    }
}
