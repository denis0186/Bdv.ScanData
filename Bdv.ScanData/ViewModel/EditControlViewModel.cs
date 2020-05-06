using System.ComponentModel;

namespace Bdv.ScanData.ViewModel
{
    public class EditControlViewModel : INotifyPropertyChanged
    {
        private int number;
        private string name;

        public int Number
        {
            get => number; 
            set
            {
                if (number == value) return;
                number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Number)));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
