using System.Windows;

namespace MouseKeyboardWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ViewModel viewModel = new();
            MainWindow mainWindow = new() { DataContext = viewModel };
            mainWindow.Show();
        }
    }
}
