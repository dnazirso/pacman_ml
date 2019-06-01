﻿using board_libs;
using board_libs.Models;
using Game_UI.Sprites;
using Game_UI.Tools;
using ghost_libs;
using pacman_libs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using utils_libs;
using utils_libs.Abstractions;
using Position = board_libs.Models.Position;

namespace Game_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private fields
        Engine engine;
        Board board;
        PacmanSprite pacman;
        GhostSprite blinky;
        DebbugPac debbug;
        #endregion

        #region init
        /// <summary>
        /// MainWindow Constructor that initialize every needs
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeMaze();
            InitializeEngine();
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
        /// Initialize game engine
        /// </summary>
        private void InitializeEngine()
        {
            engine = new Engine();
            engine.SubscribeEvent((sender, e) => LetItGo(pacman));
            engine.SubscribeEvent((sender, e) => LetItGo(blinky));
        }

        /// <summary>
        /// Initialize maze properties and elements such as 
        /// players, obstacles, border, limits and so forth.
        /// </summary>
        private void InitializeMaze()
        {
            List<IBlock> obstacles = new List<IBlock>();
            List<Dot> dots = new List<Dot>();
            board = new Board(".\\maze1.txt");

            pacman = new PacmanSprite(new Pacman(), dots);
            blinky = new GhostSprite(new Ghost(pacman.Player, board.Grid));

            playGround.Children.Add(pacman);
            playGround.Children.Add(blinky);

            foreach (List<IBlock> line in board.Maze)
            {
                foreach (Area block in line)
                {
                    var placedBlock = Placeblock(block);
                    if (block.Shape.Equals('c'))
                    {
                        pacman.Player.SetPosition(block.Min.X + 10, block.Min.Y + 20);
                        pacman.Player.Coord = block.Coord;
                    }
                    if (block.Shape.Equals('b'))
                    {
                        blinky.Player.SetPosition(block.Min.X + 10, block.Min.Y + 20);
                        blinky.Player.Coord = block.Coord;
                    }
                    if (block.Shape.Equals('·'))
                    {
                        dots.Add((Dot)placedBlock);
                    }
                    obstacles.Add(placedBlock);
                }
            }
            this.SetValue(HeightProperty, (double)board.Limits.X + 40);
            this.SetValue(WidthProperty, (double)board.Limits.Y + 40);

            canvasBorder.SetValue(HeightProperty, (double)board.Limits.X);
            canvasBorder.SetValue(WidthProperty, (double)board.Limits.Y);

            pacman.UpdatePosition();
            blinky.UpdatePosition();

            obstacles.ForEach(obstacle => playGround.Children.Add((UIElement)obstacle));
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

            if (!engine.HasBegun)
            {
                engine.HasBegun = true;
                engine.Launch();
                board.FirstEntry(pacman.Player, e.Key);
            }

            board.KeyPressedEvents(pacman.Player, e.Key);
        }

        /// <summary>
        /// Compute move for a player then render
        /// </summary>
        /// <param name="p">the pressed key</param>
        private async void LetItGo(IUIPlayer p)
        {
            board.ComputeMoves(p.Player);
            await Render(p);
        }

        /// <summary>
        /// Update the position of a player
        /// </summary>
        /// <param name="p">the player</param>
        /// <param name="key">the pressed key</param>
        private async Task Render(IUIPlayer p)
        {
            if (p.Player.Position.X != p.LastPosition.X || p.Player.Position.Y != p.LastPosition.Y)
            {
                p.UpdatePosition();
            }

            if (debbug != null)
            {
                debbug.debbug.Text = $"X : {pacman.Player.Position.X} \nY : {pacman.Player.Position.Y} \nLeft:{board.DotsLeft}|Eaten:{((IPacman)pacman.Player).DotsEaten}";
            }

            await Task.Run(() => playGround.Refresh());
        }
        #endregion
    }
}
