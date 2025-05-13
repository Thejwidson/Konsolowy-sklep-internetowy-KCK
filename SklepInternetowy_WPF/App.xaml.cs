using System.Windows;

namespace SklepInternetowy_WPF
{
    public partial class App : Application
    {
        private void ShowWindow_Click(object sender, RoutedEventArgs e)
        {
            if (Current.MainWindow != null)
            {
                Current.MainWindow.Show();
                Current.MainWindow.WindowState = WindowState.Normal;
                Current.MainWindow.Activate();
            }
        }

        private void ExitApplication_Click(object sender, RoutedEventArgs e)
        {
            if (Current.MainWindow is MainWindow mainWindow)
            {
                mainWindow.Close(); // Zamyka i sprząta tray icon
            }
            else
            {
                Shutdown();
            }
        }
    }
}
