using Game_UI.Sprites;
using Game_UI.Tools;
using pacman_libs;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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
        PacmanSprite pacmanSprite = null;
        Obstacle obstacle = null;
        int pacmanWidth = 0;
        int Heightlimit = 0;
        int WidthLimit = 0;
        int tickCounter = 0;
        DispatcherTimer timer;
        private bool hasBegun;
        #endregion

        /// <summary>
        /// MainWindow Constructor that initialize every needs
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
            InitializePlayersSprites();
        }

        /// <summary>
        /// Initialize all needed fields and properties
        /// </summary>
        private void InitializeGame()
        {
            Heightlimit = (int)canvasBorder.Height;
            WidthLimit = (int)canvasBorder.Width;
            player = new Player();
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(LetItGo);
            timer.Interval = new TimeSpan(10000);

            obstacle = new Obstacle();
            playGround.Children.Add(obstacle);
        }

        /// <summary>
        /// Instanciate the player's sprite
        /// </summary>
        private void InitializePlayersSprites()
        {
            pacmanSprite = new PacmanSprite(player);
            pacmanWidth = 40;
            playGround.Children.Add(pacmanSprite);
            player.SetPosition(0, 0);
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
                    if (e.Key != DirectionType.ToKey(player.Direction))
                    {
                        player.SetDirection(DirectionType.ToDirection(e.Key));
                        pacmanSprite.rotate();
                    }
                    break;
                default:
                    break;
            }
            if (hasBegun == false)
            {
                hasBegun = true;
                timer.Start();
            }
        }

        /// <summary>
        /// Allow a player to move if possible
        /// </summary>
        /// <param name="p">the player</param>
        private async void LetItGo(object sender, EventArgs e)
        {
            var p = player;
            if (CheckLimits(p, DirectionType.ToKey(p.Direction)))
            {
                Move(p, DirectionType.ToKey(p.Direction));
                if (tickCounter >= 20)
                {
                    pacmanSprite.NominalAnimation();
                    tickCounter = 0;
                }
            }
            await Task.Run(() => playGround.Refresh());
            tickCounter++;
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
