using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace Server
{
    public class StartServer
    {
        Process server;
        string fileName = @"minecraft*.jar";
        public delegate void ConsoleLog(string message);
        ConsoleLog log;
        public StartServer(ConsoleLog _log)
        {
            log = _log;
        }
        private string Search_server_jar()
        {
            //return "java -Xmx1024M -Xms1024M -jar minecraft_server.jar nogui";
            var tmp = Directory.GetFiles(Directory.GetCurrentDirectory(), fileName);
            if (tmp.Length != 1)
                throw new Exception("НЕСКОЛЬКО/НИ ОДНОГО ФАЙЛОВ/А minecraft.jar В ДИРЕКТОРИИ С ПРОГРАММОЙ. УДАЛИТЕ ЛИШНИЕ. ДОЛЖЕН ОСТАТЬСЯ ОДИН jar file");
            else{
                string tmpps = "java -Xmx1024M -Xms1024M -jar " + @tmp[0].Replace(Directory.GetCurrentDirectory(), "").Replace("\\", "") + " nogui ";
                return tmpps;
                }
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
                server.Start();


                server.BeginOutputReadLine();
                StreamWriter sr = server.StandardInput;


                sr.AutoFlush = true;
                sr.WriteLine("cd " + Directory.GetCurrentDirectory());
                sr.WriteLine(Search_server_jar());
                sr.WriteLine("cls");


                server.OutputDataReceived += server_OutputDataReceived;
                server.ErrorDataReceived += server_OutputDataReceived;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "   ;   " + e.InnerException.Message);
            }
        }

        void server_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data.Length > 0 && e.Data[0] == '[')
                    log(e.Data);
            }
            catch (Exception f)
            {

            }
        }

        public void Stop()
        {
            if (server == null)
                return;

            server.Exited -= server_Exited;
            server.Dispose();
            server.Kill();
            GC.WaitForFullGCComplete();
            GC.Collect();
        }

        void server_Exited(object sender, EventArgs e)
        {
            Stop();
            Start();
        }

        public void SendCommand(string p)
        {
            server.StandardInput.WriteLine(p);
        }
    }
}
