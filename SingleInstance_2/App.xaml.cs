using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SingleInstance_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mutex mutex;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            bool createdNew;
            mutex = new Mutex(true, "{SingleInstance Mutex}", out createdNew);
            if (!createdNew)
            {
                mutex = null;
                MessageBox.Show("Another instance is already running.");
                Application.Current.Shutdown();
                return;
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            if (mutex != null)
            {
                mutex.ReleaseMutex();
                mutex.Dispose();
            }
        }
    }
}
