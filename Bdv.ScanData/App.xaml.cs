using Bdv.ScanData.Services;
using Bdv.ScanData.ViewModel;
using Microsoft.Shell;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Bdv.ScanData
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {

        private const string Unique = "BDV-SCAN-DATA-05222020";

        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();

                // Allow single instance code to perform cleanup operations
                SingleInstance<App>.Cleanup();
            }
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            
            if (this.MainWindow.WindowState == WindowState.Minimized)
            {
                this.MainWindow.WindowState = WindowState.Normal;
            }

            this.MainWindow.Show();
            this.MainWindow.Activate();

            return true;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var worker = Locator.Kernel.Get<IWorkerService>();
            var logger = Locator.Kernel.Get<ILogger>();

            if (!worker.Start())
            {
                logger.Error("Ошибка старта основного сервиса");
            }
        }
    }
}
