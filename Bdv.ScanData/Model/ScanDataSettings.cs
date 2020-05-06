using System.Collections.Generic;

namespace Bdv.ScanData.Model
{
    public class ScanDataSettings
    {
        public string Port { get; set; }
        public string Service1CUri { get; set; }
        public string DataWindowHeader { get; set; }
        public List<string> DataParameters { get; set; } = new List<string>();
    }
}
