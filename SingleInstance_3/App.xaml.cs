using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SingleInstance_3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mutex mutex = new Mutex(false, "{SingleInstance Mutex}");

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!mutex.WaitOne(0, true))
            {
                MessageBox.Show("Another instance is already running.");
                Application.Current.Shutdown();
                return;
            }
        }
    }
}
