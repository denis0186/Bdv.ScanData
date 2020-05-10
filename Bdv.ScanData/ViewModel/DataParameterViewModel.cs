using System.ComponentModel;

namespace Bdv.ScanData.ViewModel
{
    public class DataParameterViewModel : INotifyPropertyChanged
    {
        public int Number { get; set; }
        public EditControlViewModel EditControl { get; set; }
        public string Value { get; set; }

#pragma warning disable 67
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning disable 67
    }
}
