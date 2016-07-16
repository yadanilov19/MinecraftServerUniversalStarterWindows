using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Server
{
    public class StartServer
    {
        Process server;
        public Console consl;
        string fileName = @"start.bat";
        private string Search_bat()
        {
            //return "java -Xmx1024M -Xms1024M -jar minecraft_server.jar nogui";
            var tmp = Directory.GetFiles(Directory.GetCurrentDirectory(), fileName);
            if (tmp.Length != 1)
                throw new Exception("НЕСКОЛЬКО/НИ ОДНОГО ФАЙЛОВ/А START.BAT В ДИРЕКТОРИИ С ПРОГРАММОЙ. УДАЛИТЕ ЛИШНИЕ. ДОЛЖЕН ОСТАТЬСЯ ОДИН БАТНИК");
            else
                return @tmp[0];
        }

        public void Start()
        {
            try
            {
                var a = Thread.CurrentThread.Name;
                server = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = "cmd.exe",
                        Verb = "Server_Minecraft_Process",
                        CreateNoWindow = false,
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        WorkingDirectory = Directory.GetCurrentDirectory()
                    },
                    EnableRaisingEvents = true,
                };

                server.Exited += server_Exited;
                consl = new Console(server);
                server.Start();

                StreamWriter sr = server.StandardInput;
                sr.WriteLine(Directory.GetCurrentDirectory());
                sr.WriteLine(fileName);
                sr.Close();
                //server.WaitForExit();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "   ;   " + e.InnerException.Message);
            }
        }

        public void Stop()
        {
            if (server == null)
                return;

            server.Exited -= server_Exited;
            server.WaitForExit();
            server.Close();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        void server_Exited(object sender, EventArgs e)
        {
            Stop();
            Start();
        }
    }
}
