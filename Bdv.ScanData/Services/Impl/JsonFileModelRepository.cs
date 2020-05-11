using Bdv.ScanData.Model;
using Newtonsoft.Json;
using NLog;
using System;
using System.IO;

namespace Bdv.ScanData.Services.Impl
{
    public class JsonFileModelRepository : IModelRepository
    {
        private const string filename = "scandata.settings.json";
        private const string directory = "Bdv.ScanData";
        private readonly ILogger logger;

        public string DirectoryName { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), directory);
        public string FileName => Path.Combine(DirectoryName, filename);

        public JsonFileModelRepository(ILogger logger)
        {
            this.logger = logger;
        }

        private ScanDataSettings LoadDefault()
        {
            return new ScanDataSettings
            {
                DataWindowHeader = "Sample window title",
                Port = "",
                Service1CUri = "http://172.16.255.32/?GetProbeParams~^",
                DataParameters = new System.Collections.Generic.List<DataParameter>
                {
                    new DataParameter {Class = "EDIT", Index = 0, Number = 1},
                    new DataParameter {Class = "EDIT", Index = 1, Number = 2},
                    new DataParameter {Class = "EDIT", Index = 2, Number = 3},
                    new DataParameter {Class = "EDIT", Index = 3, Number = 4},
                    new DataParameter {Class = "EDIT", Index = 4, Number = 5},
                    new DataParameter {Class = "EDIT", Index = 5, Number = 6},
                    new DataParameter {Class = "EDIT", Index = 6, Number = 7},
                    new DataParameter {Class = "EDIT", Index = 7, Number = 8},
                    new DataParameter {Class = "EDIT", Index = 8, Number = 9},
                    new DataParameter {Class = "EDIT", Index = 9, Number = 10},
                    new DataParameter {Class = "EDIT", Index = 10, Number = 11},
                    new DataParameter {Class = "EDIT", Index = 11, Number = 12},
                }
            };
        }

        public ScanDataSettings Load()
        {
            if (File.Exists(FileName))
            {
                var content = File.ReadAllText(FileName);
                try
                {
                    return JsonConvert.DeserializeObject<ScanDataSettings>(content);
                }
                catch (Exception e)
                {
                    logger.Error(e, $"Файл настроек '{FileName}' поврежден или имеет не верный формат. Будут загружены настройки по-умолчанию");
                }
            }

            return LoadDefault();
        }

        public bool Save(ScanDataSettings model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            try
            {
                if (!Directory.Exists(DirectoryName))
                {
                    Directory.CreateDirectory(DirectoryName);
                }

                File.WriteAllText(FileName, JsonConvert.SerializeObject(model));
            }
            catch (Exception e)
            {
                logger.Error(e, $"Ошибка записи настроек в файл '{FileName}'");
                return false;
            }

            return true;
        }
    }
}
