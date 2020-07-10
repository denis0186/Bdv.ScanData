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
                logger.Debug($"Получены данные '{data}'");
                DataCaptured?.Invoke(data);
            }
        }

        public IEnumerable<string> GetScanPorts()
        {
            return SerialPort.GetPortNames();
        }

        public bool StartScan(string port)
        {
            StopScan();
            
            try
            {
                serialPort.PortName = port;
                serialPort.BaudRate = serialPortSettings.SerialPortBaudRate;
                serialPort.Parity = serialPortSettings.SerialPortParity;
                serialPort.DataBits = serialPortSettings.SerialPortDataBits;
                serialPort.StopBits = serialPort.StopBits;
                serialPort.Open();
                logger.Debug($"Открыт порт '{port}', BaudRate '{serialPort.BaudRate}', Parity '{serialPort.Parity}', DataBits '{serialPort.DataBits}' StopBits '{serialPort.StopBits}'");
            }
            catch (Exception e)
            {
                logger.Error(e, $@"Ошибка при открытии COM порта '{port}', BaudRate = '{serialPortSettings.SerialPortBaudRate}', 
                        Parity = '{serialPortSettings.SerialPortParity}', DataBits = '{serialPortSettings.SerialPortDataBits}', StopBits = '{serialPort.StopBits}'");
                return false;
            }

            return true;
        }

        public void StopScan()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
                logger.Debug($"Закрыт порт '{serialPort.PortName}'");
            }
        }
    }

    public interface ISerialPortSettings
    {
        int SerialPortBaudRate { get; }
        Parity SerialPortParity { get; }
        int SerialPortDataBits { get; }
        StopBits SerialPortStopBits { get; }
    }
}
