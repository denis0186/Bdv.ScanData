using Bdv.ScanData.Model;

namespace Bdv.ScanData.Services
{
    public interface IModelRepository
    {
        ScanDataSettings Load();
        bool Save(ScanDataSettings model);
    }
}
