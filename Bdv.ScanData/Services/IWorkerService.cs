using Bdv.ScanData.Model;
using System.Threading.Tasks;

namespace Bdv.ScanData.Services
{
    public interface IWorkerService
    {
        bool Start();
        void Stop();
    }
}
