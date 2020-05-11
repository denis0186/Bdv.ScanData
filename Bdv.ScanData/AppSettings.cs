using Bdv.ScanData.Services.Impl;
using System.Configuration;
using System.IO.Ports;

namespace Bdv.ScanData
{
    public class AppSettings : ISerialPortSettings
    {
        public int SerialPortBaudRate { get; } = 9600;

        public Parity SerialPortParity { get; } = Parity.None;

        public int SerialPortDataBits { get; } = 8;

        public AppSettings()
        {
            if (int.TryParse(ConfigurationManager.AppSettings["SerialPortBaudRate"], out var res))
            {
                SerialPortBaudRate = res;
            }
            if (int.TryParse(ConfigurationManager.AppSettings["SerialPortParity"], out res))
            {
                SerialPortParity = (Parity)res;
            }
            if (int.TryParse(ConfigurationManager.AppSettings["SerialPortDataBits"], out res))
            {
                SerialPortDataBits = res;
            }
        }
    }
}
