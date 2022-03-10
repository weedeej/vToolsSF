using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using vTools_Session_Fetch.Constants;
using vTools_Session_Fetch.Listener;
using vTools_Session_Fetch.Objects;

namespace vTools_Session_Fetch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Output_Panel_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Strings.LogFile)) File.Move(Strings.LogFile, Strings.LogFile + ".old.txt", true);
            var outputPanel = (StackPanel)sender;
            Logger.InfoLogger.Log(outputPanel, "Waiting for session");
            var settings = await Observer.ReadSettingsYAML(sender);
            var Port = HTTPListener.GetPort();
            this.Title += $" | PORT: {Port}";
            Logger.InfoLogger.Log(outputPanel, $"YAML File Deserialized. Starting Listener at port {Port}");

            HTTPListener.Listen(outputPanel, settings.Serialize(), Port);
            Logger.InfoLogger.Log(outputPanel, $"HTTP Listener started. Auth using this Port Number: {Port}. Click to copy.");
        }

        private void ScrollView_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0)
            {
                ScrollView.ScrollToEnd();
            }
        }
    }
}
