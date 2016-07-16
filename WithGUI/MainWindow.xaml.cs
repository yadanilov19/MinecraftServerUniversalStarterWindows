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
            server = new StartServer();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            server.Start();
            server.consl.newString += consl_newString;
        }

        void consl_newString(object sender, ConsoleArgs e)
        {
            Console.Items.Add(e.currentContent);
        }
    }
}
