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
        private ICommand testService1CCommand;
        private ICommand refreshOpenedWindowsCommand;
        private ICommand applyControlsCountCommand;
        private ICommand fillWindowControlsCommand;
        private readonly IWindowService windowService;
        private readonly IScanService scanService;
        private ICommand testParametersCommand;

        public ScanDataSettings Model { get; }

        public ObservableCollection<string> OpenedWindows { get; } = new ObservableCollection<string>();
        public ObservableCollection<DataParameterViewModel> DataParameters { get; } = new ObservableCollection<DataParameterViewModel>();
        public ObservableCollection<string> Ports { get; } = new ObservableCollection<string>();
        public static ObservableCollection<EditControlViewModel> SelectedWindowControls { get; } = new ObservableCollection<EditControlViewModel>();
        public int ParametersCount { get; set; }

        public ICommand RefreshPortsCommand => refreshPortsCommand ?? (refreshPortsCommand = new RefreshPortsCommand(this, scanService));
        public ICommand TestService1CCommand => testService1CCommand;
        public ICommand RefreshOpenedWindowsCommand => refreshOpenedWindowsCommand ?? (refreshOpenedWindowsCommand = new RefreshOpenedWindowsCommand(this, windowService));
        public ICommand ApplyControlsCountCommand => applyControlsCountCommand ?? (applyControlsCountCommand = new ApplyControlsCountCommand(this));
        public ICommand FillWindowControlsCommand => fillWindowControlsCommand ?? (fillWindowControlsCommand = new FIllWindowControlsCommand(this, windowService));
        public ICommand TestParametersCommand => testParametersCommand ?? (testParametersCommand = new TestParametersCommand(this, windowService));
        public ScanDataSettingsViewModel(ScanDataSettings model, IWindowService windowService, IScanService scanService)
        {
            Model = model;
            this.windowService = windowService;
            this.scanService = scanService;
            Model.PropertyChanged += ModelPropertyChanged;
            RefreshPortsCommand.Execute(null);
            RefreshOpenedWindowsCommand.Execute(null);
            ParametersCount = model.DataParameters.Count;
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ScanDataSettings.DataWindowHeader))
            {
                FillWindowControlsCommand.Execute(null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
