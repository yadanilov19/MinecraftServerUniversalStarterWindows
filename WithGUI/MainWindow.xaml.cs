using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Server;
using System.Threading;

namespace WithGUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartServer server;
        public MainWindow()
        {
            InitializeComponent();
            
            server = new StartServer(new StartServer.ConsoleLog(ConsoleLogServer));
        }

        void ConsoleLogServer(string mess)
        {
            Dispatcher.BeginInvoke(new Action(() => { ConsoleR.Items.Add(mess); }));
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            server.Start();
        }
        
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            server.Stop();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server.Stop();
            this.Close();
        }
    }
}
