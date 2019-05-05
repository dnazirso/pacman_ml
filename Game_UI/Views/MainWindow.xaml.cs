using board_libs;
using Game_UI.Sprites;
using Game_UI.Tools;
using pacman_libs;
using System;
using System.Collections.Generic;
using System.Linq;
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
        Board board = null;
        PacmanSprite pacmanSprite = null;
        List<IBlock> obstacles = null;
        int pacmanWidth = 0;
        int Heightlimit = 0;
        int WidthLimit = 0;
        int tickCounter = 0;
        DispatcherTimer timer;
        bool hasBegun;
        #endregion
        #region init
        /// <summary>
        /// MainWindow Constructor that initialize every needs
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeMaze();

        }

        /// <summary>
        /// Initialize all needed fields and properties
        /// </summary>
        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(LetItGo);
            timer.Interval = new TimeSpan(10000);
        }

        /// <summary>
        /// Initialize maze properties, such as obstacles border limits and so forth.
        /// </summary>
        private void InitializeMaze()
        {

            board = new Board("D:\\repos\\perso\\pacman_ml\\board_libs\\maze0.txt");
            obstacles = new List<IBlock>();
            player = new Player();

            pacmanSprite = new PacmanSprite(player);
            pacmanWidth = 40;
            playGround.Children.Add(pacmanSprite);

            var top = 0;
            var left = 0;
            foreach (List<char> line in board.Grid)
            {
                left = 0;
                foreach (char letter in line)
                {
                    IBlock block = null;
                    switch (letter)
                    {
                        case 'c':
                            player.SetPosition(top - 50, left + 20);
                            pacmanSprite.SetValue(TopProperty, (double)top - 50);
                            pacmanSprite.SetValue(LeftProperty, (double)left + 20);
                            block = new Blank(top, left, 20, false);
                            break;
                        case '#':
                            block = new Obstacle(top, left, 20, true);
                            break;
                        default:
                            block = new Blank(top, left, 20, false);
                            break;
                    }
                    obstacles.Add(block);
                    left += 20;
                }
                top += 20;
            }

            Heightlimit = top;
            WidthLimit = left;
            this.SetValue(HeightProperty, (double)top);
            this.SetValue(WidthProperty, (double)left);

            //playGround.SetValue(HeightProperty, (double)top);
            //playGround.SetValue(WidthProperty, (double)left);
            //playGround.SetValue(TopProperty, (double)top + 20);
            //playGround.SetValue(LeftProperty, (double)left + 20);

            canvasBorder.SetValue(HeightProperty, (double)top);
            canvasBorder.SetValue(WidthProperty, (double)left);
            canvasBorder.SetValue(TopProperty, (double)top - 20);
            canvasBorder.SetValue(LeftProperty, (double)left - 20);

            obstacles.ForEach(obstacle => playGround.Children.Add((UIElement)obstacle));
        }
        #endregion
        #region gamedesign
        /// <summary>
        /// Keyboard event handler
        /// </summary>
        /// <param name="sender">object that sends the event</param>
        /// <param name="e">the event iteself</param>
        private void WatchKeys(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.F5))
            {
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }

            if (DirectionType.ExistsWhitin(e.Key) && e.Key != DirectionType.ToKey(player.Direction))
            {
                player.SetDirection(DirectionType.ToDirection(e.Key));
                pacmanSprite.rotate();
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
            if ((p.Position.X < 0 && key.Equals(DirectionType.Up.Key))
            || (p.Position.X + pacmanWidth > Heightlimit && key.Equals(DirectionType.Down.Key))
            || (p.Position.Y < 0 && key.Equals(DirectionType.Left.Key))
            || (p.Position.Y + pacmanWidth > WidthLimit && key.Equals(DirectionType.Right.Key)))
            {
                return false;
            }
            return !obstacles.Exists(x => x.WillCollide(player));
        }

        /// <summary>
        /// Update the position of a player
        /// </summary>
        /// <param name="p">the player</param>
        /// <param name="key">the pressed key</param>
        private void Move(IPlayer p, Key key)
        {
            p.Move();

            if (key.Equals(DirectionType.Left.Key) || key.Equals(DirectionType.Right.Key))
            {
                pacmanSprite.SetValue(LeftProperty, (double)p.Position.Y);
            }
            if (key.Equals(DirectionType.Up.Key) || key.Equals(DirectionType.Down.Key))
            {
                pacmanSprite.SetValue(TopProperty, (double)p.Position.X);
            }
            debbug.Text = $"X : {player.Position.X} \nY : {player.Position.Y}";
        }
        #endregion
    }
}
