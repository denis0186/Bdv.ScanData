using System.Collections.Generic;

namespace Bdv.ScanData.Services
{
    public interface IService1C
    {
        IEnumerable<string> GetParameters(string uri, params string[] parameters);
    }
}
