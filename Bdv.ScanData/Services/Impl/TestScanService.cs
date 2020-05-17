using NLog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bdv.ScanData.Services.Impl
{
    public class TestScanService : IScanService
    {
        public event Action<string> DataCaptured;
        private Task _task;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly ILogger logger;

        public TestScanService(ILogger logger)
        {
            this.logger = logger;
        }

        public IEnumerable<string> GetScanPorts()
        {
            return new[]
            {
                "COM1", "COM2", "COM3"
            };
        }

        public bool StartScan(string port)
        {
            if (_cancellationTokenSource?.IsCancellationRequested ?? true)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _task = new Task(() =>
                {
                    while (!_cancellationTokenSource.IsCancellationRequested)
                    {
                        Thread.Sleep(1000);
                        var data = Guid.NewGuid().ToString();
                        logger.Debug($"Получены данные от тестового порта '{data}'");
                        DataCaptured?.Invoke(data);
                    }
                }, _cancellationTokenSource.Token);
                _task.Start();
                logger.Debug($"Открыт тестовый порт");
            }

            return true;
        }

        public void StopScan()
        {
            _cancellationTokenSource?.Cancel();
            _task?.Wait();
            logger.Debug($"Закрыт тестовый порт");
        }
    }
}
