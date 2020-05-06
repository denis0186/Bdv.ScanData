using Bdv.ScanData.ViewModel;
using System;
using System.Windows.Input;

namespace Bdv.ScanData.Commands
{
    public class ApplyControlsCountCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        public ApplyControlsCountCommand(ScanDataSettingsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var count = viewModel.EditControls.Count;
            for(var i = count - 1; i >= viewModel.ParametersCount; i--)
            {
                viewModel.EditControls.RemoveAt(i);
            }

            for (var i = count; i < viewModel.ParametersCount; i++)
            {
                viewModel.EditControls.Add(new EditControlViewModel { Number = i + 1 });
            }
        }
    }
}
