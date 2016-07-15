using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace ConsoleApplication14
{

    class Program
    {
        static Process server;
        static string fileName = @"start.bat";
        static void Main(string[] args)
        {
            Create_Instanse();
            Console.Write("Заупск прошел успешно");
            Console.WriteLine(server.StandardOutput.ReadToEnd());
            while (true)
                Console.ReadLine();
        }

        private static string Search_bat()
        {
            var tmp = Directory.GetFiles(Directory.GetCurrentDirectory(), fileName);
            if (tmp.Length > 1)
                throw new Exception("НЕСКОЛЬКО ФАЙЛОВ START.BAT В ДИРЕКТОРИИ С ПРОГРАММОЙ. УДАЛИТЕ ЛИШНИЕ. ДОЛЖЕН ОСТАТЬСЯ ОДИН БАТНИК");
            else
                return @tmp[0];   
        }

        private static void Create_Instanse()
        {
            try
            {
                server = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = "cmd.exe",
                        Verb = "Server_Minecraft_Process",
                        CreateNoWindow = false,
                        Arguments = "/c" + "\"" + Search_bat(),
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        RedirectStandardOutput = true
                    },
                    EnableRaisingEvents = true,
                };

                server.Exited += server_Exited;
                server.Start();

                //server.WaitForExit();
            }
            catch
            {
                throw new Exception("непредвиденная ошибка. попробуйте снова");
            }
        }

        static void server_Exited(object sender, EventArgs e)
        {
            Create_Instanse();
        }

    }
}
