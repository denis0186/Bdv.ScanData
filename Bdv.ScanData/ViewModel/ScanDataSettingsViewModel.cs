using Bdv.ScanData.Commands;
using Bdv.ScanData.Model;
using Bdv.ScanData.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Bdv.ScanData.ViewModel
{
    public class ScanDataSettingsViewModel : INotifyPropertyChanged
    {
        private ICommand refreshPortsCommand;
        private ICommand refreshOpenedWindowsCommand;
        private ICommand applyControlsCountCommand;
        private ICommand fillWindowControlsCommand;
        private readonly IWindowService windowService;
        private readonly IScanService scanService;
        private readonly IModelRepository modelRepository;
        private readonly IWorkerService workerService;
        private ICommand applyScanSettingsCommand;
        private ICommand loadScanSettingsCommand;
        private ICommand testParametersCommand;

        public ScanDataSettings Model { get; set; }

        public ObservableCollection<string> OpenedWindows { get; } = new ObservableCollection<string>();
        public ObservableCollection<DataParameterViewModel> DataParameters { get; } = new ObservableCollection<DataParameterViewModel>();
        public ObservableCollection<string> Ports { get; } = new ObservableCollection<string>();
        public static ObservableCollection<EditControlViewModel> SelectedWindowControls { get; } = new ObservableCollection<EditControlViewModel>();
        public int ParametersCount { get; set; }

        public ICommand RefreshPortsCommand => refreshPortsCommand ?? (refreshPortsCommand = new RefreshPortsCommand(this, scanService));
        public ICommand RefreshOpenedWindowsCommand => refreshOpenedWindowsCommand ?? (refreshOpenedWindowsCommand = new RefreshOpenedWindowsCommand(this, windowService));
        public ICommand ApplyControlsCountCommand => applyControlsCountCommand ?? (applyControlsCountCommand = new ApplyControlsCountCommand(this));
        public ICommand FillWindowControlsCommand => fillWindowControlsCommand ?? (fillWindowControlsCommand = new FIllWindowControlsCommand(this, windowService));
        public ICommand TestParametersCommand => testParametersCommand ?? (testParametersCommand = new TestParametersCommand(this, windowService));
        public ICommand LoadScanSettingsCommand => loadScanSettingsCommand ?? (loadScanSettingsCommand = new LoadScanSettingsCommand(this, modelRepository));
        public ICommand ApplyScanSettingsCommand => applyScanSettingsCommand ?? (applyScanSettingsCommand = new ApplyScanSettingsCommand(this, modelRepository, workerService));

        public ScanDataSettingsViewModel(IWindowService windowService, IScanService scanService, IModelRepository modelRepository, IWorkerService workerService)
        {
            this.windowService = windowService;
            this.scanService = scanService;
            this.modelRepository = modelRepository;
            this.workerService = workerService;
            PropertyChanged += ScanDataSettingsViewModelPropertyChanged;
            LoadScanSettingsCommand.Execute(null);
        }

        private void ScanDataSettingsViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model))
            {
                Model.PropertyChanged -= ModelPropertyChanged;
                Model.PropertyChanged += ModelPropertyChanged;
            }
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ScanDataSettings.DataWindowHeader))
            {
                FillWindowControlsCommand.Execute(null);
            }
        }
#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning disable 67
    }
}
