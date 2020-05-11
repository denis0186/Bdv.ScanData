using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Bdv.ScanData.Services.Impl
{
    public class HttpService1C : IService1C
    {
        private readonly ILogger logger;

        public HttpService1C(ILogger logger)
        {
            this.logger = logger;
        }

        public IEnumerable<string> GetParameters(string uri, params string[] parameters)
        {
            var request = WebRequest.Create($"{uri}{string.Join("~^", parameters)}");
            logger.Debug($"Отправлен запрос '{request.RequestUri.AbsoluteUri}'");
            try
            {
                var response = request.GetResponse();
                logger.Debug($"Получен ответ на запрос '{request.RequestUri.AbsoluteUri}', ContentLength = '{response.ContentLength}'");
                using (var stream = new StreamReader(response.GetResponseStream()))
                {
                    var result = stream.ReadToEnd();
                    return result.Split(new[] { "~^" }, StringSplitOptions.None);
                }
            }
            catch (Exception e)
            {
                logger.Error(e, $"Ошибка обработки запроса '{request.RequestUri.AbsoluteUri}'");
                return Enumerable.Empty<string>();
            }
        }
    }
}
