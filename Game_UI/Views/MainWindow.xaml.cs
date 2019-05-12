using board_libs;
using board_libs.Models;
using Game_UI.Sprites;
using Game_UI.Tools;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using utils_libs.Abstractions;

namespace Game_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private fields
        IPlayer _player;
        Board _board;
        PacmanSprite _pacmanSprite;
        List<IBlock> _obstacles;
        DebbugPac _debbug;
        #endregion

        #region init
        /// <summary>
        /// MainWindow Constructor that initialize every needs
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeMaze();
            InitializeDebbugMode();
        }

        /// <summary>
        /// Add debbug content
        /// </summary>
        private void InitializeDebbugMode()
        {
            _debbug = new DebbugPac();
            playGround.Children.Add(_debbug);
        }

        /// <summary>
        /// Initialize maze properties and elements such as 
        /// players, obstacles, border, limits and so forth.
        /// </summary>
        private void InitializeMaze()
        {
            var resourceName = ".\\maze1.txt";
            _board = new Board(resourceName);
            _obstacles = new List<IBlock>();
            _player = new pacman_libs.Player();
            _board.InitializeTimer((sender, e) => LetItGo(_player));

            _pacmanSprite = new PacmanSprite(_player);
            playGround.Children.Add(_pacmanSprite);

            foreach (Area block in _board.Maze)
            {
                var placedBlock = Placeblock(block);
                if (block.Shape.Equals('c'))
                {
                    _player.SetPosition(block.Min.X + 10, block.Min.Y + 20);
                }
                _obstacles.Add(placedBlock);
            }
            this.SetValue(HeightProperty, (double)_board.Limits.X + 40);
            this.SetValue(WidthProperty, (double)_board.Limits.Y + 40);

            canvasBorder.SetValue(HeightProperty, (double)_board.Limits.X);
            canvasBorder.SetValue(WidthProperty, (double)_board.Limits.Y);

            _pacmanSprite.UpdatePosition();

            _obstacles.ForEach(obstacle => playGround.Children.Add((UIElement)obstacle));
        }

        /// <summary>
        /// Puts in place each block
        /// </summary>
        /// <param name="top"></param>
        /// <param name="left"></param>
        /// <param name="letter"></param>
        private IBlock Placeblock(Area block)
        {
            switch (block.Shape)
            {
                case 'c': return new Blank(block);

                case '╔':
                case '╗':
                case '╝':
                case '╚': return new PipeAngle(block);

                case '#': return new Obstacle(block);

                case '║':
                case '═': return new PipeStraight(block);

                case '-': return new Blank(block);

                case '·': return new Dot(block);

                default: return new Blank(block);
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
            _board.KeyPressedEvents(_player, e.Key);
        }

        /// <summary>
        /// Allow a player to move if possible
        /// </summary>
        /// <param name="p">the pressed key</param>
        private async void LetItGo(IPlayer p)
        {
            _board.RetrySetDirectionAndMove(p);
            await Render(p);
        }

        /// <summary>
        /// Update the position of a player
        /// </summary>
        /// <param name="p">the player</param>
        /// <param name="key">the pressed key</param>
        private async Task Render(IPlayer p)
        {
            if (p.Position.X != _pacmanSprite.lastPosition.X || p.Position.Y != _pacmanSprite.lastPosition.Y)
            {
                _pacmanSprite.UpdatePosition();
            }

            if (_debbug != null)
            {
                _debbug.debbug.Text = $"X : {p.Position.X} \nY : {p.Position.Y}";
            }

            await Task.Run(() => playGround.Refresh());
        }
        #endregion
    }
}
