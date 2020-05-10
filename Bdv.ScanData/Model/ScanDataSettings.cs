using System.Collections.Generic;
using System.ComponentModel;

namespace Bdv.ScanData.Model
{
    public class ScanDataSettings : INotifyPropertyChanged
    {
        public string Port { get; set; }
        public string Service1CUri { get; set; }
        public string DataWindowHeader { get; set; }
        public List<DataParameter> DataParameters { get; set; } = new List<DataParameter>();
#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning disable 67
    }
}
