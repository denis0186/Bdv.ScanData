using Bdv.ScanData.Services;
using Bdv.ScanData.ViewModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Bdv.ScanData.Commands
{
    public class ApplyScanSettingsCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;
        private readonly IModelRepository modelRepository;
        private readonly IWorkerService workerService;
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning disable 67

        public ApplyScanSettingsCommand(ScanDataSettingsViewModel viewModel, IModelRepository modelRepository, IWorkerService workerService)
        {
            this.viewModel = viewModel;
            this.modelRepository = modelRepository;
            this.workerService = workerService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Model.DataParameters.Clear();
            viewModel.Model.DataParameters.AddRange(viewModel.DataParameters.Select(x => new Model.DataParameter { Class = x.EditControl?.Class, Index = x.EditControl?.Index ?? -1, Number = x.Number }));

            if (!modelRepository.Save(viewModel.Model))
            {
                MessageBox.Show("Ошибка при сохранении параметров. Новые настройки не приняты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Параметры записаны", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                if (!workerService.Start())
                {
                    MessageBox.Show("Ошибка применения новых настроек", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
    }
}
