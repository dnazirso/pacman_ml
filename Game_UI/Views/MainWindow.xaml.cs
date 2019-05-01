using Game_UI.Tools;
using pacman_libs;
using System;
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
        #region Private fields and properties
        IPlayer player = null;
        int pacmanWidth = 0;
        int Heightlimit = 0;
        int WidthLimit = 0;
        private bool hasBegun;
        #endregion

        /// <summary>
        /// MainWindow Constructor that initialize every needs
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        /// <summary>
        /// Initialize all needed fields and properties
        /// </summary>
        private void InitializeGame()
        {
            var x = (double)pacmanSprite.GetValue(Canvas.LeftProperty);
            var y = (double)pacmanSprite.GetValue(Canvas.TopProperty);
            Heightlimit = (int)canvasBorder.Height;
            WidthLimit = (int)canvasBorder.Width;
            pacmanWidth = (int)pacmanSprite.Width;
            player = new Player();
            player.SetPosition((int)x, (int)y);
        }

        /// <summary>
        /// Keyboard event handler
        /// </summary>
        /// <param name="sender">object that sends the event</param>
        /// <param name="e">the event iteself</param>
        private void WatchKeys(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                case Key.Up:
                case Key.Right:
                case Key.Down:
                    if (e.Key != DirectionMapper.ToKey(player.Direction))
                    {
                        player.SetDirection(DirectionMapper.ToDirection(e.Key));
                    }
                    break;
                default:
                    break;
            }
            if (hasBegun == false)
            {
                hasBegun = true;
                LetItGo(player);
            }
        }

        /// <summary>
        /// Allow a player to move if possible
        /// </summary>
        /// <param name="p">the player</param>
        private async void LetItGo(IPlayer p)
        {
            do
            {
                if (CheckLimits(p, DirectionMapper.ToKey(p.Direction)))
                {
                    Move(p, DirectionMapper.ToKey(p.Direction));
                }
                await Task.Run(() => playGround.Refresh(20));
            }
            while (true);
        }

        /// <summary>
        /// Check if the next step is possible depending on the direction taken
        /// </summary>
        /// <param name="p">the player</param>
        /// <param name="key">the pressed key</param>
        /// <returns>a boolean</returns>
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

        /// <summary>
        /// Update the position of a player
        /// </summary>
        /// <param name="p">the player</param>
        /// <param name="key">the pressed key</param>
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
