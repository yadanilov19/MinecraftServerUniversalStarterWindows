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
            ConsoleTextField.Text = WithGUI.Properties.Resources.StringDefault;
            server = new StartServer(new StartServer.ConsoleLog(ConsoleLogServer));
        }

        void ConsoleLogServer(string mess)
        {
            Dispatcher.BeginInvoke(new Action(() => {
                if (ConsoleR.Items.Count > 1000)
                    for (int i = 0; i < 800; ++i)
                        ConsoleR.Items.RemoveAt(0);

                        ConsoleR.Items.Add(mess);
                ConsoleR.ScrollIntoView(mess);
            }));
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

        private void ConsoleTextField_GotFocus(object sender, RoutedEventArgs e)
        {

            if ((sender as TextBox).Text == WithGUI.Properties.Resources.StringDefault)
            {
                (sender as TextBox).Text = "";
                return;
            }
        }

        private void ConsoleTextField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ConsoleR.Items.Add("{Command from This Host Process}[]: " + ConsoleTextField.Text);
                server.SendCommand(ConsoleTextField.Text);
            }
        }

    }
}
