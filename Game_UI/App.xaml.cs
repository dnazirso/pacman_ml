using System.Windows;

namespace Game_UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var intro = new Views.StartWindow();
            intro.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            intro.ShowDialog();
        }
    }
}
