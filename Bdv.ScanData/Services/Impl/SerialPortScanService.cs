using System.Collections.Generic;
using System.IO.Ports;

namespace Bdv.ScanData.Services.Impl
{
    public class SerialPortScanService : IScanService
    {
        public IEnumerable<string> GetScanPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
