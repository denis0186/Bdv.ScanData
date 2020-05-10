using System.Collections.Generic;

namespace Bdv.ScanData.Services
{
    public interface IScanService
    {
        IEnumerable<string> GetScanPorts();
    }
}
