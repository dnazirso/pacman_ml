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
        IPlayer player = null;
        int pacmanWidth = 0;
        int Heightlimit = 0;
        int WidthLimit = 0;
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
            Heightlimit = (int)canvasBorder.Height;
            WidthLimit = (int)canvasBorder.Width;
            pacmanWidth = (int)pacmanSprite.Width;
            player = new Player();
            player.SetPosition((int)x, (int)y);
        }

        private void WatchKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                    player.SetDirection(DirectionMapper.ToDirection(e.Key));
                    OnDirectionSet(player);
                    break;
                default:
                    break;
            }
        }

        async void OnDirectionSet(IPlayer p)
        {
            do
            {
                lastPostion = p.Position;
                LetItGo(p);
                await Task.Run(() => playGround.Refresh());
            }
            while (!lastPostion.Equals(p.Position));
        }

        private void LetItGo(IPlayer p)
        {
            if (CheckLimits(p, DirectionMapper.ToKey(p.Direction))) Move(p, DirectionMapper.ToKey(p.Direction));
        }

        private bool CheckLimits(IPlayer p, Key key)
        {
            if ((p.Position.X < 0 && key == Key.Up)
            || (p.Position.X + pacmanWidth > Heightlimit && key == Key.Down)
            || (p.Position.Y < 0 && key == Key.Left)
            || (p.Position.Y + pacmanWidth > WidthLimit && key == Key.Right))
            {
                return false;
            }
            return true;
        }

        private void Move(IPlayer p, Key key)
        {
            p.Move();
            switch (key)
            {
                case Key.Left:
                case Key.Right:
                    pacmanSprite.SetValue(LeftProperty, (double)p.Position.Y);
                    break;
                case Key.Up:
                case Key.Down:
                    pacmanSprite.SetValue(TopProperty, (double)p.Position.X);
                    break;
                default:
                    break;
            }
        }
    }
}
