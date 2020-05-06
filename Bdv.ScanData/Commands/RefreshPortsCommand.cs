using Bdv.ScanData.ViewModel;
using System;
using System.IO.Ports;
using System.Windows.Input;

namespace Bdv.ScanData.Commands
{
    public class RefreshPortsCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        public RefreshPortsCommand(ScanDataSettingsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Ports.Clear();
            foreach (var port in SerialPort.GetPortNames())
            {
                viewModel.Ports.Add(port);
            }
        }
    }
}
