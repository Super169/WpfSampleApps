using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SingleInstance_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (System.Diagnostics.Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Count() > 1)
            {
                MessageBox.Show("Another instance is already running.");
                Application.Current.Shutdown();
                return;
            }
        }
    }
}
