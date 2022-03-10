using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using vTools_Session_Fetch.Logger;
namespace vTools_Session_Fetch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Copy pasta for logging :V
            Startup += new StartupEventHandler(AppEventHandler);

            DispatcherUnhandledException += LogDispatcherUnhandled;

            TaskScheduler.UnobservedTaskException += LogUnobservedTaskException;
            RenderOptions.ProcessRenderMode = RenderMode.SoftwareOnly;
        }

        private void AppEventHandler(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.FirstChanceException += LogFirstChanceException;
            AppDomain.CurrentDomain.UnhandledException += LogUnhandled;
        }

        private void LogFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            InfoLogger.Log(message: $"{e.Exception.Message}: {e.Exception.StackTrace}");
        }

        private void LogDispatcherUnhandled(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            InfoLogger.Log(message: $"{e.Exception.Message}: {e.Exception.StackTrace}");
            e.Handled = false;
        }

        private void LogUnhandled(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (e.IsTerminating)
            {
                InfoLogger.Log(message: $"{ex.Message}: {ex.StackTrace}");
            }
        }

        private void LogUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            InfoLogger.Log(message: $"{e.Exception.Message}: {e.Exception.StackTrace}");
            e.SetObserved();
        }
    }
}
