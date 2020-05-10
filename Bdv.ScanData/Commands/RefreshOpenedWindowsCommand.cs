﻿using Bdv.ScanData.Services;
using Bdv.ScanData.ViewModel;
using System;
using System.Windows.Input;
using Theraot.Collections;

namespace Bdv.ScanData.Commands
{
    public class RefreshOpenedWindowsCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;
        private readonly IWindowService windowService;

        public event EventHandler CanExecuteChanged;

        public RefreshOpenedWindowsCommand(ScanDataSettingsViewModel viewModel, IWindowService windowService)
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
            viewModel.OpenedWindows.Clear();
            viewModel.OpenedWindows.AddRange(windowService.GetWindows());
        }
    }
}
