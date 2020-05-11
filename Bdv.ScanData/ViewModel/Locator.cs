using Bdv.ScanData.DI;
using Ninject;

namespace Bdv.ScanData.ViewModel
{
    public class Locator
    {
        private static ScanDataSettingsViewModel scanDataSettings;

        public static IKernel Kernel { get; } = new StandardKernel(new IoCModule());
        public static ScanDataSettingsViewModel ScanDataSettings => scanDataSettings ?? (scanDataSettings = Kernel.Get<ScanDataSettingsViewModel>());
    }
}
