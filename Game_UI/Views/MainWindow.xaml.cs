﻿using Game_UI.Sprites;
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
        PacmanSprite pacmanSprite = null;
        List<Obstacle> obstacles = null;
        int pacmanWidth = 0;
        int Heightlimit = 0;
        int WidthLimit = 0;
        int tickCounter = 0;
        DispatcherTimer timer;
        bool hasBegun;
        #endregion

        /// <summary>
        /// MainWindow Constructor that initialize every needs
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeMaze();
            InitializePlayersSprites();
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
            Heightlimit = (int)canvasBorder.Height;
            WidthLimit = (int)canvasBorder.Width;
            obstacles = new List<Obstacle>();
            obstacles.Add(new Obstacle(200, 100, 20));
            obstacles.ForEach(obstacle => playGround.Children.Add(obstacle));
        }

        /// <summary>
        /// Instanciate the player's sprite
        /// </summary>
        private void InitializePlayersSprites()
        {
            player = new Player();
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
            if (obstacles.Exists(x => x.WillCollide(player)))
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
    }
}
