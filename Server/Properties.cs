using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    [Serializable]
    public static class PropertiesServer
    {
        static bool AutoStart = true;

        public static bool autoStart
        {
            get { return AutoStart; }
            set { AutoStart = value; }
        }

        static int HeapSize = 1024;

        public static int heapSize
        {
            get { return HeapSize; }
            set { HeapSize = value; }
        }
    }
}
