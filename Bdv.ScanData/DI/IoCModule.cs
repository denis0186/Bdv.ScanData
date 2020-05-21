using Bdv.ScanData.Services;
using Bdv.ScanData.Services.Impl;
using Bdv.ScanData.ViewModel;
using Ninject.Modules;
using NLog;

namespace Bdv.ScanData.DI
{
    public class IoCModule : NinjectModule
    {
        public override void Load()
        {
            var appSettings = new AppSettings();

            Bind<ScanDataSettingsViewModel>().ToSelf();
            Bind<IScanService>().To<SerialPortScanService>().InSingletonScope();
            Bind<IWorkerService>().To<WorkerService>().InSingletonScope();
            Bind<IService1C>().To<HttpService1C>();
            Bind<IWindowService>().To<User32WindowService>();
            Bind<IModelRepository>().To<JsonFileModelRepository>();
            Bind<ILogger>().ToMethod(c => LogManager.GetCurrentClassLogger());
            Bind<ISerialPortSettings>().ToConstant(appSettings);
        }
    }
}
