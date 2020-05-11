using Bdv.ScanData.ViewModel;
using System;
using System.Windows.Input;

namespace Bdv.ScanData.Commands
{
    public class ApplyControlsCountCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning disable 67
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
            var count = viewModel.DataParameters.Count;
            for(var i = count - 1; i >= viewModel.ParametersCount; i--)
            {
                viewModel.DataParameters.RemoveAt(i);
            }

            for (var i = count; i < viewModel.ParametersCount; i++)
            {
                viewModel.DataParameters.Add(new DataParameterViewModel { Number = i + 1 });
            }
        }
    }
}
