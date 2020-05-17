using Bdv.ScanData.Services.Impl;
using Bdv.ScanData.ViewModel;
using NLog;
using System.Windows;

namespace Bdv.ScanData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _closingFromMenu;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TaskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.Show();
            this.Activate();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!_closingFromMenu)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Hide(); 
            }
        }

        private void MenuItem_ClickExit(object sender, RoutedEventArgs e)
        {
            _closingFromMenu = true;
            Close();
            _closingFromMenu = false;
        }
    }
}
