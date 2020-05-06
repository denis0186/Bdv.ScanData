using Bdv.ScanData.Commands;
using Bdv.ScanData.Model;
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

        public ScanDataSettings Model { get; }

        public ObservableCollection<string> OpenedWindows { get; } = new ObservableCollection<string>();
        public ObservableCollection<EditControlViewModel> EditControls { get; } = new ObservableCollection<EditControlViewModel>();
        public ObservableCollection<string> Ports { get; } = new ObservableCollection<string>();
        public int ParametersCount { get; set; }

        public ICommand RefreshPortsCommand  => refreshPortsCommand ?? (refreshPortsCommand = new RefreshPortsCommand(this));
        public ICommand TestService1CCommand => testService1CCommand;
        public ICommand RefreshOpenedWindowsCommand => refreshOpenedWindowsCommand ?? (refreshOpenedWindowsCommand = new RefreshOpenedWindowsCommand(this));
        public ICommand ApplyControlsCountCommand => applyControlsCountCommand ?? (applyControlsCountCommand = new ApplyControlsCountCommand(this));

        public ScanDataSettingsViewModel(ScanDataSettings model)
        {
            Model = model;
            RefreshPortsCommand.Execute(null);
            RefreshOpenedWindowsCommand.Execute(null);
            ParametersCount = model.DataParameters.Count;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
