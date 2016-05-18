﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace NotifyIcon_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mutex mutex;

        private System.Windows.Forms.NotifyIcon _notifyIcon;
        private bool _isExit;
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

            MainWindow = new MainWindow();
            MainWindow.Closing += MainWindow_Closing;

            _notifyIcon = new System.Windows.Forms.NotifyIcon();
            _notifyIcon.DoubleClick += (s, args) => ShowMainWindows();
            _notifyIcon.Icon = NotifyIcon_2.Properties.Resources.MyIcon;
            _notifyIcon.Visible = true;

            CreateContextMenu();
            ShowMainWindows();
        }

        private void CreateContextMenu()
        {
            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("MainWindow...").Click += (s, e) => ShowMainWindows();
            _notifyIcon.ContextMenuStrip.Items.Add("Exit").Click += (s, e) => ExitApplication();
        }

        public void ShowMainWindows()
        {
            if (MainWindow.IsVisible)
            {
                if (MainWindow.WindowState == WindowState.Minimized)
                {
                    MainWindow.WindowState = WindowState.Normal;
                }
                MainWindow.Activate();
            }
            else
            {
                MainWindow.Show();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!_isExit)
            {
                e.Cancel = true;
                MainWindow.Hide(); // A hidden window can be shown again, a closed one not
            }
        }

        private void ExitApplication()
        {
            _isExit = true;
            MainWindow.Close();
            _notifyIcon.Dispose();
            _notifyIcon = null;
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
