﻿using board_libs;
using board_libs.Models;
using Game_UI.Sprites;
using Game_UI.Tools;
using pacman_libs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using utils_libs.Abstractions;
using utils_libs.Tools;

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
        IDirection _wantedDirection;
        List<IBlock> _obstacles;
        List<IBlock> _dots;
        DispatcherTimer _timer;
        DebbugPac _debbug;
        bool _hasBegun;
        int _tickMoveCounter;
        int _tickRotateCounter;
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
            _debbug = new DebbugPac();
            playGround.Children.Add(_debbug);
        }

        /// <summary>
        /// Initialize all needed fields and properties
        /// </summary>
        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(LetItGo);
            _timer.Interval = new TimeSpan(10000);
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
            _dots = new List<IBlock>();
            _player = new pacman_libs.Player();

            _pacmanSprite = new PacmanSprite(_player);
            playGround.Children.Add(_pacmanSprite);

            foreach (Area block in _board.Maze)
            {
                var placedBlock = Placeblock(block);
                if (block.Shape.Equals('c'))
                {
                    _player.SetPosition(block.Min.X + 10, block.Min.Y + 20);
                    _pacmanSprite.SetValue(TopProperty, (double)_player.Position.X);
                    _pacmanSprite.SetValue(LeftProperty, (double)_player.Position.Y);
                }
                if (block.Shape.Equals('·'))
                {
                    _dots.Add(placedBlock);
                }
                _obstacles.Add(placedBlock);
            }
            this.SetValue(HeightProperty, (double)_board.Limits.X + 40);
            this.SetValue(WidthProperty, (double)_board.Limits.Y + 40);

            canvasBorder.SetValue(HeightProperty, (double)_board.Limits.X);
            canvasBorder.SetValue(WidthProperty, (double)_board.Limits.Y);

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
            if (!DirectionType.ExistsWhitin(e.Key)) return;
            if (!_hasBegun
                && (DirectionType.ToDirection(e.Key).Equals(DirectionType.Left.Direction)
                || DirectionType.ToDirection(e.Key).Equals(DirectionType.Right.Direction)))
            {
                _hasBegun = true;
                _timer.Start();
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
            IPlayer testPlayer = new board_libs.Models.Player
            {
                Direction = direction,
                Position = new board_libs.Models.Position
                {
                    X = _player.Position.X,
                    Y = _player.Position.Y
                }
            };
            if (!_obstacles.Exists(x => x.WillCollide(testPlayer)))
            {
                _player.SetDirection(direction);
                _pacmanSprite.rotate();
                _wantedDirection = DirectionType.StandStill.Direction;
                _tickRotateCounter = 0;
            }
            else
            {
                _wantedDirection = direction;
            }
        }

        /// <summary>
        /// Allow a player to move if possible
        /// </summary>
        /// <param name="p">the pressed key</param>
        private async void LetItGo(object sender, EventArgs e)
        {
            var p = _player;

            if (_tickRotateCounter < 20 && !_wantedDirection.Equals(DirectionType.StandStill.Direction))
            {
                _tickRotateCounter++;
                SetDirection(DirectionType.ToDirection(DirectionType.ToKey(_wantedDirection)));
            }
            if (CheckLimits(p, DirectionType.ToKey(p.Direction)))
            {
                Move(p);
                if (_tickMoveCounter >= 20)
                {
                    _pacmanSprite.NominalAnimation();
                    _tickMoveCounter = 0;
                }
            }
            await Task.Run(() => playGround.Refresh());
            _tickMoveCounter++;
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
            || (p.Position.X > _board.Limits.X && key.Equals(DirectionType.Down.Key))
            || (p.Position.Y < 0 && key.Equals(DirectionType.Left.Key))
            || (p.Position.Y > _board.Limits.Y && key.Equals(DirectionType.Right.Key)))
            {
                return false;
            }
            return !_obstacles.Exists(x =>
            {
                var wlcld = x.WillCollide(p);
                return wlcld;
            });
        }

        /// <summary>
        /// Update the position of a player
        /// </summary>
        /// <param name="p">the player</param>
        /// <param name="key">the pressed key</param>
        private void Move(IPlayer p)
        {
            p.Move();
            DoesWarp(p);

            _pacmanSprite.SetValue(LeftProperty, (double)p.Position.Y);
            _pacmanSprite.SetValue(TopProperty, (double)p.Position.X);

            if (_debbug != null)
            {
                _debbug.debbug.Text = $"X : {p.Position.X} \nY : {p.Position.Y}";
            }
        }

        /// <summary>
        /// When reaching an edge, teleport to the other side
        /// </summary>
        /// <param name="p"></param>
        private void DoesWarp(IPlayer p)
        {
            if (p.Position.Y > _board.Limits.Y)
            {
                p.SetPosition(p.Position.X, 0);
            }
            if (p.Position.Y < 0)
            {
                p.SetPosition(p.Position.X, _board.Limits.Y);
            }
        }
        #endregion
    }
}
