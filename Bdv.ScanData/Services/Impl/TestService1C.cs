﻿using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Bdv.ScanData.Services.Impl
{
    public class TestService1C : IService1C
    {
        private readonly ILogger logger;

        public TestService1C(ILogger logger)
        {
            this.logger = logger;
        }

        public IEnumerable<string> GetParameters(string uri, params string[] parameters)
        {
            logger.Debug($"Имитация отправки запроса на uri '{uri}' с параметрами '{string.Join(",", parameters)}'");
            Thread.Sleep(500);
            var response = Enumerable.Range(1, 12).Select(x => new Random(x + DateTime.Now.Millisecond).Next(9999).ToString("D0"));
            logger.Debug($"Получен ответ '{string.Join(",", response)}' от сервиса '{uri}'");
            return response;
        }
    }
}
