using Bdv.ScanData.DI;
using Bdv.ScanData.Services;
using Bdv.ScanData.ViewModel;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Bdv.ScanData
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mutex = new Mutex(true, "Bdv.ScanData", out var onlyOne);
            if (!onlyOne)
            {
                MessageBox.Show("Другой экземпляр приложения уже запущен", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown();
                return;
            }

            var worker = Locator.Kernel.Get<IWorkerService>();
            var logger = Locator.Kernel.Get<ILogger>();

            if (!worker.Start())
            {
                logger.Error("Ошибка старта основного сервиса");
            }
        }
    }
}
