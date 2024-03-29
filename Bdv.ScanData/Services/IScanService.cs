﻿using Bdv.ScanData.Model;
using System;
using System.Collections.Generic;

namespace Bdv.ScanData.Services
{
    public interface IScanService
    {
        event Action<string> DataCaptured;
        IEnumerable<string> GetScanPorts();
        bool StartScan(string port);
        void StopScan();
    }
}
