using Bdv.ScanData.Model;
using System.Linq;

namespace Bdv.ScanData.Services.Impl
{
    public class WorkerService : IWorkerService
    {
        private readonly IScanService scanService;
        private readonly IService1C service1C;
        private readonly IModelRepository modelRepository;
        private readonly IWindowService windowService;

        public ScanDataSettings Model { get; private set; }

        public WorkerService(IScanService scanService, IService1C service1C, IModelRepository modelRepository, IWindowService windowService)
        {
            this.scanService = scanService;
            this.service1C = service1C;
            this.modelRepository = modelRepository;
            this.windowService = windowService;

            scanService.DataCaptured += ScanServiceDataCaptured;
        }

        private void ScanServiceDataCaptured(string data)
        {
            var parameters = service1C.GetParameters(Model.Service1CUri, "", data).ToList();
            var dataParameters = Model.DataParameters.Where(x => x.Index >= 0 && x.Number <= parameters.Count).OrderBy(x => x.Number);
            windowService.SetText(Model.DataWindowHeader,
                dataParameters.Select(x => x.Class).ToArray(),
                dataParameters.Select(x => x.Index).ToArray(),
                parameters.ToArray());
        }

        public bool Start()
        {
            scanService.StopScan();
            Model = modelRepository.Load();
            return scanService.StartScan(Model.Port);
        }

        public void Stop()
        {
            scanService.StopScan();
        }
    }
}
