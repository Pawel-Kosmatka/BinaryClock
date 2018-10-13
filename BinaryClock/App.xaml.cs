using BinaryClock.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BinaryClock
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Window sWindow;
            var startUpWindow = Settings.Default.StartUpWindow;
            if (startUpWindow.Equals("MainWindow.xaml"))
                sWindow = new MainWindow();
            else
                sWindow = new AltWindow();
            sWindow.Show();
        }
    }
}
