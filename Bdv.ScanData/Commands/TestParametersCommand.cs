using Bdv.ScanData.Services;
using Bdv.ScanData.ViewModel;
using System;
using System.Linq;
using System.Windows.Input;

namespace Bdv.ScanData.Commands
{
    public class TestParametersCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;
        private readonly IWindowService windowService;

        public event EventHandler CanExecuteChanged;

        public TestParametersCommand(ScanDataSettingsViewModel viewModel, IWindowService windowService)
        {
            this.viewModel = viewModel;
            this.windowService = windowService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            foreach (var item in viewModel.DataParameters.Where(x => !string.IsNullOrEmpty(x.Value)))
            {
                windowService.SetText(viewModel.Model.DataWindowHeader, item.EditControl.Class, item.EditControl.Index, item.Value);
            }
        }
    }
}
