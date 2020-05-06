using Bdv.ScanData.ViewModel;
using System;
using System.Text;
using System.Windows.Input;
using static Vanara.PInvoke.User32;

namespace Bdv.ScanData.Commands
{
    public class RefreshOpenedWindowsCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;

        public event EventHandler CanExecuteChanged;

        public RefreshOpenedWindowsCommand(ScanDataSettingsViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.OpenedWindows.Clear();
            EnumWindows((hwnd, param) =>
            {
                try
                {
                    var length = GetWindowTextLength(hwnd);
                    if (length > 0)
                    {
                        var sb = new StringBuilder(length + 1);
                        GetWindowText(hwnd, sb, length + 1);
                        if (sb.Length > 0)
                        {
                            viewModel.OpenedWindows.Add(sb.ToString());
                        }
                    }
                }
                catch
                {

                }
                return true;
            }, new IntPtr(0));
        }
    }
}
