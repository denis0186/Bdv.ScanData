using System;
using System.Windows.Input;

namespace Bdv.ScanData.Commands
{
    public class ApplyScanSettingsCommand : ICommand
    {
#pragma warning disable 67
        public event EventHandler CanExecuteChanged;
#pragma warning disable 67

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            return;
        }
    }
}
