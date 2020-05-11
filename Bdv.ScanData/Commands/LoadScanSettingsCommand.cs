using Bdv.ScanData.Services;
using Bdv.ScanData.ViewModel;
using System;
using System.Linq;
using System.Windows.Input;
using Theraot.Collections;

namespace Bdv.ScanData.Commands
{
    public class LoadScanSettingsCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;
        private readonly IModelRepository modelRepository;
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning disable 67
        public LoadScanSettingsCommand(ScanDataSettingsViewModel viewModel, IModelRepository modelRepository)
        {
            this.viewModel = viewModel;
            this.modelRepository = modelRepository;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Ports.Clear();
            viewModel.OpenedWindows.Clear();
            viewModel.DataParameters.Clear();
            ScanDataSettingsViewModel.SelectedWindowControls.Clear();

            var model = modelRepository.Load();

            viewModel.Model = model;
            viewModel.ParametersCount = model.DataParameters.Count;

            viewModel.OpenedWindows.Add(model.DataWindowHeader);
            viewModel.Ports.Add(model.Port);
            
            ScanDataSettingsViewModel.SelectedWindowControls.AddRange(model.DataParameters.Where(x => x.Index >= 0).Select(x => new EditControlViewModel { Class = x.Class, Index = x.Index }));

            foreach (var item in model.DataParameters.OrderBy(x => x.Number))
            {
                viewModel.DataParameters.Add(new DataParameterViewModel
                { Number = item.Number, EditControl = ScanDataSettingsViewModel.SelectedWindowControls.FirstOrDefault(x => x.Class == item.Class && x.Index == item.Index) });
            }
        }
    }
}
