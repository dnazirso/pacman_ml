using Game_UI.Tools;
using pacman_libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Pacman pacman = null;
        int pacmanWidth = 0;
        // Step 6 (event ending part)
        Position lastPostion;

        //Step 1
        event EventHandler PacRunEvent;
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        void InitializeGame()
        {
            var x = (double)pacmanSprite.GetValue(Canvas.LeftProperty);
            var y = (double)pacmanSprite.GetValue(Canvas.TopProperty);
            pacmanWidth = (int)pacmanSprite.Width;
            pacman = new Pacman();
            pacman.SetPosition((int)x, (int)y);

            // Step3
            PacRunEvent += PacRun;
        }

        // Step 2
        void PacRun(object sender, EventArgs e)
        {
            do
            {
                lastPostion = pacman.Position;
                // Do something here.
                LetItGo();
                playGround.Refresh();
            }
            while (!lastPostion.Equals(pacman.Position));
        }

        // Step 4
        void OnDirectionSet(EventArgs e)
        {
            PacRunEvent?.Invoke(this, e);
        }

        private void WatchKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                    pacman.SetDirection(DirectionMapper.ToDirection(e.Key));
                    // Step 5
                    OnDirectionSet(EventArgs.Empty);
                    break;
                default:
                    break;
            }
        }

        private void LetItGo()
        {
            CheckLimitsAndMove(pacman.Position.X, pacman.Position.Y, DirectionMapper.ToKey(pacman.Direction));
        }

        private void CheckLimitsAndMove(double top, double left, Key key)
        {
            var Heightlimit = canvasBorder.Height;
            var WidthLimit = canvasBorder.Width;

            if ((top < 0 && key == Key.Up)
            || (top + pacmanWidth > Heightlimit && key == Key.Down)
            || (left < 0 && key == Key.Left)
            || (left + pacmanWidth > WidthLimit && key == Key.Right))
            {
                return;
            }
            ActMove(key);
        }

        private void ActMove(Key key)
        {
            pacman.Move();
            switch (key)
            {
                case Key.Left:
                case Key.Right:
                    pacmanSprite.SetValue(LeftProperty, (double)pacman.Position.Y);
                    break;
                case Key.Up:
                case Key.Down:
                    pacmanSprite.SetValue(TopProperty, (double)pacman.Position.X);
                    break;
                default:
                    break;
            }
        }
    }
}
