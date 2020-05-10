﻿using Bdv.ScanData.Services;
using Bdv.ScanData.ViewModel;
using System;
using System.Windows.Input;
using Theraot.Collections;

namespace Bdv.ScanData.Commands
{
    public class RefreshPortsCommand : ICommand
    {
        private readonly ScanDataSettingsViewModel viewModel;
        private readonly IScanService scanService;

        public event EventHandler CanExecuteChanged;

        public RefreshPortsCommand(ScanDataSettingsViewModel viewModel, IScanService scanService)
        {
            this.viewModel = viewModel;
            this.scanService = scanService;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Ports.Clear();
            viewModel.Ports.AddRange(scanService.GetScanPorts());
        }
    }
}
