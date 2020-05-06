using Bdv.ScanData.ViewModel;
using System.Windows;

namespace Bdv.ScanData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ScanDataSettingsViewModel(new Model.ScanDataSettings());
        }
    }
}
