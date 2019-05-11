using System.Windows;
using System.Windows.Input;

namespace Game_UI.Views
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        /// <summary>
        /// Constructor of the introduction
        /// </summary>
        public StartWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Swich view in order to begin game
        /// </summary>
        private void LaunchGame()
        {
            Hide();
            var maze = new MainWindow();
            maze.ShowDialog();
            Close();
        }

        /// <summary>
        /// Begin game event handler by click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaunchGame(object sender, RoutedEventArgs e) => LaunchGame();

        /// <summary>
        /// Begin game event handler by pressing Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LaunchGame(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) LaunchGame();
        }
    }
}
