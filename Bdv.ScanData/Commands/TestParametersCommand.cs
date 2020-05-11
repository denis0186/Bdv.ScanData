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
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning disable 67
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
            if (parameter.Equals("write"))
            {
                foreach (var item in viewModel.DataParameters.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    windowService.SetText(viewModel.Model.DataWindowHeader, item.EditControl.Class, item.EditControl.Index, item.Value);
                }
            }
            if (parameter.Equals("read"))
            {
                foreach (var item in viewModel.DataParameters)
                {
                    item.Value = windowService.GetText(viewModel.Model.DataWindowHeader, item.EditControl.Class, item.EditControl.Index);
                }
            }
        }
    }
}
