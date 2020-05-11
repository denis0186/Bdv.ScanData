using Bdv.ScanData.Model;
using NLog;
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace Bdv.ScanData.Services.Impl
{
    public class SerialPortScanService : IScanService
    {
        private readonly SerialPort serialPort = new SerialPort();
        private readonly ISerialPortSettings serialPortSettings;
        private readonly ILogger logger;

        public event Action<string> DataCaptured;

        public SerialPortScanService(ISerialPortSettings serialPortSettings, ILogger logger)
        {
            this.serialPortSettings = serialPortSettings;
            this.logger = logger;

            serialPort.DataReceived += SerialPortDataReceived;
        }

        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var data = serialPort.ReadExisting();
            if (!string.IsNullOrEmpty(data))
            {
                DataCaptured?.Invoke(data);
            }
        }

        public IEnumerable<string> GetScanPorts()
        {
            return SerialPort.GetPortNames();
        }

        public void StartScan(ScanDataSettings scanDataSettings)
        {
            StopScan();

            serialPort.PortName = scanDataSettings.Port;
            serialPort.BaudRate = serialPortSettings.SerialPortBaudRate;
            serialPort.Parity = serialPortSettings.SerialPortParity;
            serialPort.DataBits = serialPortSettings.SerialPortDataBits;

            try
            {
                serialPort.Open();
            }
            catch (Exception e)
            {
                logger.Error(e, $@"Ошибка при открытии COM порта '{scanDataSettings.Port}', BaudRate = '{serialPortSettings.SerialPortBaudRate}', 
                        Parity = '{serialPortSettings.SerialPortParity}', DataBits = '{serialPortSettings.SerialPortDataBits}'");
            }
        }

        public void StopScan()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }
    }

    public interface ISerialPortSettings
    {
        int SerialPortBaudRate { get; }
        Parity SerialPortParity { get; }
        int SerialPortDataBits { get; }
    }
}
