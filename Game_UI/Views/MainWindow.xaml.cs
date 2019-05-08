using board_libs;
using Game_UI.Sprites;
using Game_UI.Tools;
using pacman_libs;
using System;
using System.Collections.Generic;
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
        #region Private fields
        IPlayer player;
        Board board;
        PacmanSprite pacmanSprite;
        Position limits;
        IDirection wantedDirection;
        List<IBlock> obstacles;
        List<Dot> dots;
        DispatcherTimer timer;
        DebbugPac debbug;
        bool hasBegun;
        int tickCounter;
        int tickRotateCounter;
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
            InitializeDebbugMode();
        }

        /// <summary>
        /// Add debbug content
        /// </summary>
        private void InitializeDebbugMode()
        {
            debbug = new DebbugPac();
            playGround.Children.Add(debbug);
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
        /// Initialize maze properties and elements such as 
        /// players, obstacles, border, limits and so forth.
        /// </summary>
        private void InitializeMaze()
        {
            var resourceName = ".\\maze1.txt";
            board = new Board(resourceName);
            obstacles = new List<IBlock>();
            dots = new List<Dot>();
            player = new Player();

            pacmanSprite = new PacmanSprite(player);
            playGround.Children.Add(pacmanSprite);

            var top = 0;
            var left = 0;
            foreach (List<char> line in board.Grid)
            {
                left = 0;
                foreach (char letter in line)
                {
                    var block = Placeblock(top, left, letter);
                    if (letter.Equals('·'))
                    {
                        dots.Add((Dot)block);
                        board.DotsLeft++;
                    }
                    obstacles.Add(block);
                    left += 20;
                }
                top += 20;
            }
            limits = new Position { X = top, Y = left };
            this.SetValue(HeightProperty, (double)top + 150);
            this.SetValue(WidthProperty, (double)left + 150);

            canvasBorder.SetValue(HeightProperty, (double)top);
            canvasBorder.SetValue(WidthProperty, (double)left);

            obstacles.ForEach(obstacle => playGround.Children.Add((UIElement)obstacle));
        }

        /// <summary>
        /// Puts in place each block
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="letter"></param>
        private IBlock Placeblock(int top, int left, char letter)
        {
            switch (letter)
            {
                case 'c':
                    player.SetPosition(top + 10, left + 20);
                    pacmanSprite.SetValue(TopProperty, (double)top + 10);
                    pacmanSprite.SetValue(LeftProperty, (double)left + 20);
                    return new Blank(top, left, 20, false);
                case '╔': return new PipeAngle(top, left, 20, true, 0);
                case '╗': return new PipeAngle(top, left, 20, true, 90);
                case '╝': return new PipeAngle(top, left, 20, true, 180);
                case '╚': return new PipeAngle(top, left, 20, true, 270);
                case '#': return new Obstacle(top, left, 20, true);
                case '║': return new PipeStraight(top, left, 20, true, 0);
                case '═': return new PipeStraight(top, left, 20, true, 90);
                case '-': return new Blank(top, left, 20, true);
                case '·': return new Dot(top, left, 20, false, board);
                default: return new Blank(top, left, 20, false);
            }
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
            if (!DirectionType.ExistsWhitin(e.Key)) return;
            if (!hasBegun
                && (DirectionType.ToDirection(e.Key).Equals(DirectionType.Left.Direction)
                || DirectionType.ToDirection(e.Key).Equals(DirectionType.Right.Direction)))
            {
                hasBegun = true;
                timer.Start();
                SetDirection(DirectionType.ToDirection(e.Key));
            }

            SetDirection(DirectionType.ToDirection(e.Key));
        }

        /// <summary>
        /// Set the direction to the player
        /// </summary>
        /// <param name="direction">the wanted </param>
        private void SetDirection(IDirection direction)
        {
            IPlayer testPlayer = new Player
            {
                Direction = direction,
                Position = new Position
                {
                    X = player.Position.X,
                    Y = player.Position.Y
                }
            };
            if (!obstacles.Exists(x => x.WillCollide(testPlayer)))
            {
                player.SetDirection(direction);
                pacmanSprite.rotate();
                wantedDirection = DirectionType.StandStill.Direction;
                tickRotateCounter = 0;
            }
            else
            {
                wantedDirection = direction;
            }
        }

        /// <summary>
        /// Allow a player to move if possible
        /// </summary>
        /// <param name="p">the pressed key</param>
        private async void LetItGo(object sender, EventArgs e)
        {
            var p = player;

            if (tickRotateCounter < 20 && !wantedDirection.Equals(DirectionType.StandStill.Direction))
            {
                tickRotateCounter++;
                SetDirection(DirectionType.ToDirection(DirectionType.ToKey(wantedDirection)));
            }
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
            || (p.Position.X > limits.X && key.Equals(DirectionType.Down.Key))
            || (p.Position.Y < 0 && key.Equals(DirectionType.Left.Key))
            || (p.Position.Y > limits.Y && key.Equals(DirectionType.Right.Key)))
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
            DoesWarp(p);

            pacmanSprite.SetValue(LeftProperty, (double)p.Position.Y);
            pacmanSprite.SetValue(TopProperty, (double)p.Position.X);

            if (debbug != null)
            {
                debbug.debbug.Text = $"X : {player.Position.X} \nY : {player.Position.Y}";
            }
        }

        /// <summary>
        /// When reaching an edge, teleport to the other side
        /// </summary>
        /// <param name="p"></param>
        private void DoesWarp(IPlayer p)
        {
            if (p.Position.Y > limits.Y)
            {
                p.SetPosition(p.Position.X, 0);
            }
            if (p.Position.Y < 0)
            {
                p.SetPosition(p.Position.X, limits.Y);
            }
        }
        #endregion
    }
}
