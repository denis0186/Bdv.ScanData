using Bdv.ScanData.Services;
using Bdv.ScanData.ViewModel;
using System;
using System.Windows.Input;

namespace Bdv.ScanData.Commands
{
    public class FIllWindowControlsCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;
        private readonly IWindowService windowService;
        private string[] controlClasses = new[] { "EDIT" };
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning disable 67
        public FIllWindowControlsCommand(ScanDataSettingsViewModel viewModel, IWindowService windowService)
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
            ScanDataSettingsViewModel.SelectedWindowControls.Clear();

            foreach (var item in viewModel.DataParameters)
            {
                item.EditControl = null;
            }

            foreach (var controlClass in controlClasses)
            {
                var index = 0;
                foreach (var control in windowService.GetControls(viewModel.Model.DataWindowHeader, controlClass))
                {
                    ScanDataSettingsViewModel.SelectedWindowControls.Add(new EditControlViewModel { Class = controlClass, Index = index++ });
                }
            }
        }
    }
}
