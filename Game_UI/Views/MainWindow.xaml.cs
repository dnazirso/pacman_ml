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
        Position lastPostion;

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
        }

        async void PacRun()
        {
            do
            {
                lastPostion = pacman.Position;
                // Do something here.
                LetItGo();
                await Task.Run(() => playGround.Refresh());
                await Task.Delay(10);
            }
            while (!lastPostion.Equals(pacman.Position));
        }

        void OnDirectionSet()
        {
            PacRun();
        }

        private void LetItGo()
        {
            CheckLimitsAndMove(pacman.Position.X, pacman.Position.Y, DirectionMapper.ToKey(pacman.Direction));
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
                    OnDirectionSet();
                    break;
                default:
                    break;
            }
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
